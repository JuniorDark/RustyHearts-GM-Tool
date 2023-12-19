using RHGMTool.Data;
using System.Data;
using System.Text;
using static RHGMTool.Helper.EnumMapper;

namespace RHGMTool
{
    public partial class ItemForm : Form
    {
        private readonly MailForm? mailForm;
        private int activeItemIndex;
        private MailTemplateData? templateData;
        private readonly DataTable? itemDataTable;
        private readonly bool isDatabase;

        public ItemForm(MailForm? mailForm, int activeItemIndex, bool database = false)
        {
            InitializeComponent();
            isDatabase = database;
            this.mailForm = mailForm;
            this.activeItemIndex = activeItemIndex;
            IsMdiContainer = true;
            ConfigureMdiForm(this);
            templateData = new MailTemplateData();
            itemDataTable = ItemDataTable.CachedDataTable;
            InitializeOptionComboboxes();
            cmbItemType.SelectedIndex = 1;
            cmbItemBranch.SelectedIndex = 0;
            cmbJobClass.SelectedIndex = 0;
            cbSocketColor1.SelectedIndex = 0;
            cbSocketColor2.SelectedIndex = 0;
            cbSocketColor3.SelectedIndex = 0;

        }

        #region Form Configuration

        private void ItemForm_Load(object sender, EventArgs e)
        {
            if (isDatabase)
            {
                Text = "Rusty Hearts Item Database";
                btnAddItem.Visible = false;
                numAmount.Visible = false;
                pbItemIcon.Visible = false;
                labelAmount.Visible = false;
                lbSelectedItem.Visible = false;
            }
            else
            {
                Text = $"Attach Item (Slot {activeItemIndex})";
            }

            ConfigureDataGridView();
        }

        private void ItemForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is MailForm mailForm)
            {
                dataGridView.CellValueNeeded -= DataGridView_CellValueNeeded;
                dataGridView.CellFormatting -= DataGridView_CellFormatting;
                templateData = null;
                itemDataTable?.Clear();
                iconImageCache.Clear();
                Dispose();
            }

        }

        private void InitializeOptionComboboxes()
        {
            // Populate option code comboboxes with ItemOption Table data
            DBQuery.PopulateOptionCodeComboBox(cbOptionCode1);
            DBQuery.PopulateOptionCodeComboBox(cbOptionCode2);
            DBQuery.PopulateOptionCodeComboBox(cbOptionCode3);

            // Populate socket code comboboxes with ItemOption Table data
            DBQuery.PopulateOptionCodeComboBox(cbSocketCode1);
            DBQuery.PopulateOptionCodeComboBox(cbSocketCode2);
            DBQuery.PopulateOptionCodeComboBox(cbSocketCode3);
        }

        private static void ConfigureMdiForm(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control is Label label)
                {
                    label.BackColor = ColorTranslator.FromHtml("#1a1917");
                    label.ForeColor = Color.White;
                }
                else if (control is GroupBox groupBox)
                {
                    groupBox.BackColor = ColorTranslator.FromHtml("#2c2b29");
                    groupBox.ForeColor = Color.White;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.BackColor = ColorTranslator.FromHtml("#1a1917");
                    comboBox.ForeColor = Color.Gold;
                }
                else if (control is MdiClient midClient)
                {
                    midClient.BackColor = ColorTranslator.FromHtml("#1a1917");
                }
                else if (control is Button button)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.BackColor = ColorTranslator.FromHtml("#1a1917");

                    button.MouseEnter += (sender, e) =>
                    {
                        button.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#1a1917");
                        button.FlatAppearance.BorderSize = 1;
                    };

                    button.MouseLeave += (sender, e) =>
                    {
                        button.FlatAppearance.BorderSize = 0;
                    };
                }
            }
        }

        #endregion

        #region Template Configuration
        public void SetTemplateData(MailTemplateData data, int itemIndex)
        {
            templateData = data;
            activeItemIndex = itemIndex;
            LoadTemplateData();
            SetIconSlot();
        }

        public void LoadTemplateData()
        {
            try
            {
                if (templateData == null)
                {
                    MessageBox.Show($"Error loading template data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (InvokeRequired)
                {
                    BeginInvoke(new Action(LoadTemplateData));
                    return;
                }

                int activeIndex = activeItemIndex - 1;
                string templateItemType = templateData?.ItemTypes?[activeIndex] ?? string.Empty;
                int templateItemID = templateData?.ItemIDs?[activeIndex] ?? 0;

                SetComboBoxSelectedIndex(cmbItemType, templateItemType);

                DataGridViewRow? selectedRow = GetSelectedRowByItemID(dataGridView, templateItemID);

                if (selectedRow != null)
                {
                    dataGridView.ClearSelection();
                    selectedRow.Selected = true;
                    dataGridView.CurrentCell = selectedRow.Cells["ID"];
                }
                numAmount.Value = templateData?.ItemAmounts?[activeIndex] ?? 0;
                numDurability.Value = templateData?.Durabilities?[activeIndex] ?? 0;
                numMaxDurability.Value = templateData?.DurabilityMaxValues?[activeIndex] ?? 0;
                numEnchantLevel.Value = templateData?.EnchantLevels?[activeIndex] ?? 0;
                numRank.Value = templateData?.Ranks?[activeIndex] ?? 0;
                numReconstructionMax.Value = templateData?.ReconNums?[activeIndex] ?? 0;
                tbReconNum.Text = (templateData?.ReconStates?[activeIndex] ?? 0).ToString();
                numWeight.Value = templateData?.WeightValues?[activeIndex] ?? 0;


                SetComboBoxSelectedIndexByOptionCode(cbOptionCode1, templateData?.OptionCodes1?[activeIndex]);
                SetComboBoxSelectedIndexByOptionCode(cbOptionCode2, templateData?.OptionCodes2?[activeIndex]);
                SetComboBoxSelectedIndexByOptionCode(cbOptionCode3, templateData?.OptionCodes3?[activeIndex]);

                SetComboBoxSelectedIndexBySocketColor(cbSocketColor1, templateData?.SocketColors1?[activeIndex]);
                SetComboBoxSelectedIndexBySocketColor(cbSocketColor2, templateData?.SocketColors2?[activeIndex]);
                SetComboBoxSelectedIndexBySocketColor(cbSocketColor3, templateData?.SocketColors3?[activeIndex]);

                SetComboBoxSelectedIndexByOptionCode(cbSocketCode1, templateData?.SocketCodes1?[activeIndex]);
                SetComboBoxSelectedIndexByOptionCode(cbSocketCode2, templateData?.SocketCodes2?[activeIndex]);
                SetComboBoxSelectedIndexByOptionCode(cbSocketCode3, templateData?.SocketCodes3?[activeIndex]);

                SetOptionValues(numOptionValue1, templateData?.OptionCodes1?[activeIndex], templateData?.OptionValues1?[activeIndex]);
                SetOptionValues(numOptionValue2, templateData?.OptionCodes2?[activeIndex], templateData?.OptionValues2?[activeIndex]);
                SetOptionValues(numOptionValue3, templateData?.OptionCodes3?[activeIndex], templateData?.OptionValues3?[activeIndex]);

                SetOptionValues(numSocketValue1, templateData?.SocketCodes1?[activeIndex], templateData?.SocketValues1?[activeIndex]);
                SetOptionValues(numSocketValue2, templateData?.SocketCodes2?[activeIndex], templateData?.SocketValues2?[activeIndex]);
                SetOptionValues(numSocketValue3, templateData?.SocketCodes3?[activeIndex], templateData?.SocketValues3?[activeIndex]);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading template data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void SetComboBoxSelectedIndex(ComboBox comboBox, string value)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString() == value)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        private static DataGridViewRow? GetSelectedRowByItemID(DataGridView dataGridView, int? itemID)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (Convert.ToInt32(row.Cells["ID"].Value) == itemID)
                {
                    return row;
                }
            }

            return null;
        }

        private static void SetComboBoxSelectedIndexByOptionCode(ComboBox comboBox, int? optionCode)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                ItemOption itemOption = (ItemOption)comboBox.Items[i];
                if (itemOption.ID == optionCode)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        private static void SetComboBoxSelectedIndexBySocketColor(ComboBox comboBox, int? socketColor)
        {
            if (socketColor.HasValue && socketColorIdMap.ContainsValue(socketColor.Value))
            {
                string socketColorString = socketColorIdMap.FirstOrDefault(x => x.Value == socketColor.Value).Key;
                comboBox.SelectedIndex = comboBox.Items.IndexOf(socketColorString);
            }
        }

        private static void SetOptionValues(NumericUpDown numericUpDown, int? optionCode, int? optionValue)
        {
            (int maxValue, int minValue) = DBQuery.GetOptionValues(optionCode);
            numericUpDown.Maximum = maxValue;
            numericUpDown.Minimum = minValue;
            numericUpDown.Value = optionValue ?? 0;
        }
        #endregion

        #region Datagrid Configuration

        private void FilterDataGridView()
        {
            try
            {
                // Get selected values from cmbItemCategory, cmbSubCategory, cmbItemBranch, and cmbJobClass
                int selectedCategoryID = ((ItemOption)cmbItemCategory.SelectedItem)?.ID ?? 0;
                int selectedSubCategoryID = ((ItemOption)cmbSubCategory.SelectedItem)?.ID ?? 0;
                int selectedBranchIndex = cmbItemBranch.SelectedIndex;
                int selectedJobClass = cmbJobClass.SelectedItem != null ? GetJobClassValue(cmbJobClass.SelectedItem.ToString()) : 0;

                // Map selectedBranchIndex to nBranch values
                IEnumerable<int> nBranchValues = MapBranchIndexToValues(selectedBranchIndex);

                // Use a DataView to filter the DataTable based on the selected category, subcategory, branch, and job class
                DataView dataView = new(ItemDataTable.CachedDataTable);

                StringBuilder filterExpressionBuilder = new();
                filterExpressionBuilder.Append($"(nCategory = {selectedCategoryID} OR {selectedCategoryID} = 0) ");
                filterExpressionBuilder.Append($"AND (nSubCategory = {selectedSubCategoryID} OR {selectedSubCategoryID} = 0) ");

                // Conditionally include nBranch filter
                if (selectedBranchIndex != 0)
                {
                    string nBranchFilter = $"(nBranch IN ({string.Join(",", nBranchValues)}))";
                    filterExpressionBuilder.Append($"AND {nBranchFilter} ");
                }

                // Conditionally include nJobClass filter
                if (selectedJobClass != 0)
                {
                    filterExpressionBuilder.Append($"AND (nJobClass = {selectedJobClass}) ");
                }

                filterExpressionBuilder.Append($"AND (ItemType = {(int)selectedItemType} OR '{selectedItemType}' = 'All')");

                dataView.RowFilter = filterExpressionBuilder.ToString();
                DataTable filteredDataTable = dataView.ToTable();

                // Update the DataGridView with the filtered DataTable
                UpdateDataGridView(filteredDataTable);
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred in category filter: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbItemCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDataGridView();
        }

        private void CmbSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDataGridView();
        }

        private void CmbItemBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDataGridView();
        }

        private void CmbJobClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDataGridView();
        }

        DataTable? filteredDataTable;

        private ItemType selectedItemType;
        private void CmbItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string itemTypeString = cmbItemType.Text;

                if (Enum.TryParse(itemTypeString, out ItemType itemType))
                {
                    selectedItemType = itemType;

                    filteredDataTable = itemDataTable;

                    // Check if filtering is required
                    if (itemType != ItemType.All)
                    {
                        // Use a DataView to filter the DataTable based on the ItemType
                        DataView dataView = new(itemDataTable)
                        {
                            RowFilter = $"ItemType = {(int)itemType}"
                        };
                        filteredDataTable = dataView.ToTable();
                    }

                    if (filteredDataTable != null && filteredDataTable.Rows.Count > 0)
                    {
                        // Bind the filteredDataTable to the DataGridView
                        UpdateDataGridView(filteredDataTable);

                        ConfigureDataGridView();

                        // Set the initial selected row in the DataGridView
                        if (dataGridView.Rows.Count > 0)
                        {
                            dataGridView.Rows[0].Selected = true;
                            DataGridViewRow selectedRow = dataGridView.SelectedRows[0];

                            string? rowSelectedItemType = selectedRow.Cells["ItemType"].Value?.ToString();
                            if (Enum.TryParse(rowSelectedItemType, out ItemType selectedItemType))
                            {
                                SetControlValues(selectedRow, selectedItemType);
                            }
                            else
                            {
                                MessageBox.Show($"Invalid item type: {itemTypeString}", "Invalid Item Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        // Populate cmbItemCategory with categories
                        DBQuery.PopulateCategoryComboBox(cmbItemCategory, selectedItemType);

                        // Populate cmbSubCategory with subcategories
                        DBQuery.PopulateSubCategoryComboBox(cmbSubCategory, selectedItemType);

                        UpdateControls(itemType);
                    }
                    else
                    {
                        dataGridView.DataSource = null;
                        MessageBox.Show($"No data found for the selected item type: {itemType}", "Invalid Item Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show($"Invalid item type: {itemTypeString}", "Invalid Item Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred in Item Type: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateControls(ItemType itemType)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateControls(itemType)));
                return;
            }

            switch (itemType)
            {
                case ItemType.All:
                case ItemType.Item:
                    numEnchantLevel.Value = 0;
                    gbGearStats.Enabled = false;
                    gbRandom.Enabled = false;
                    gbSocket.Enabled = false;
                    if (cbOptionCode1.Items.Count > 0) cbOptionCode1.SelectedIndex = 0;
                    if (cbOptionCode2.Items.Count > 0) cbOptionCode2.SelectedIndex = 0;
                    if (cbOptionCode3.Items.Count > 0) cbOptionCode3.SelectedIndex = 0;
                    numOptionValue1.Value = 0;
                    numOptionValue2.Value = 0;
                    numOptionValue3.Value = 0;
                    if (cbSocketCode1.Items.Count > 0) cbSocketCode1.SelectedIndex = 0;
                    if (cbSocketCode2.Items.Count > 0) cbSocketCode2.SelectedIndex = 0;
                    if (cbSocketCode3.Items.Count > 0) cbSocketCode3.SelectedIndex = 0;
                    numSocketValue1.Value = 0;
                    numSocketValue2.Value = 0;
                    numSocketValue3.Value = 0;
                    break;
                case ItemType.Costume:
                    numEnchantLevel.Value = 0;
                    numAmount.Value = 1;
                    gbGearStats.Enabled = false;
                    gbRandom.Enabled = true;
                    gbSocket.Enabled = false;
                    if (cbSocketCode1.Items.Count > 0) cbSocketCode1.SelectedIndex = 0;
                    if (cbSocketCode2.Items.Count > 0) cbSocketCode2.SelectedIndex = 0;
                    if (cbSocketCode3.Items.Count > 0) cbSocketCode3.SelectedIndex = 0;
                    numSocketValue1.Value = 0;
                    numSocketValue2.Value = 0;
                    numSocketValue3.Value = 0;
                    break;
                case ItemType.Armor:
                case ItemType.Weapon:
                    numAmount.Value = 1;
                    gbGearStats.Enabled = true;
                    gbRandom.Enabled = true;
                    break;
                default:
                    MessageBox.Show("Invalid item type.", "Invalid Item Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void UpdateDataGridView(DataTable itemDataTable)
        {
            if (dataGridView.InvokeRequired)
            {
                dataGridView.Invoke(new Action(() => UpdateDataGridView(itemDataTable)));
                return;
            }

            dataGridView.Columns.Clear();

            if (dataGridView.DataSource == null || dataGridView.DataSource != itemDataTable)
            {
                if (dataGridView.Columns.Count == 0)
                {
                    // Bind the DataView to the DataGridView
                    dataGridView.DataSource = itemDataTable;

                    DataGridViewTextBoxColumn idColumn = new()
                    {
                        Name = "ID",
                        DataPropertyName = "nID",
                        HeaderText = "ID",
                    };
                    dataGridView.Columns.Add(idColumn);

                    DataGridViewTextBoxColumn nameColumn = new()
                    {
                        Name = "Name",
                        DataPropertyName = "wszDesc",
                        HeaderText = "Name",
                    };
                    dataGridView.Columns.Add(nameColumn);

                    DataGridViewImageColumn iconColumn = new()
                    {
                        Name = "Icon",
                        DataPropertyName = "szIconName",
                        HeaderText = "Icon",
                        ImageLayout = DataGridViewImageCellLayout.Zoom
                    };
                    dataGridView.Columns.Add(iconColumn);

                }

                // Set the initial selected row in the DataGridView
                if (dataGridView.Rows.Count > 0)
                {
                    string totalRows = dataGridView.RowCount.ToString();
                    lbTotal.Text = $"Showing: {totalRows} items";
                    dataGridView.Rows[0].Selected = true;
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];

                    // Assuming "ItemType" is a column in your DataGridView
                    string? rowSelectedItemType = selectedRow.Cells["ItemType"].Value?.ToString();
                    if (Enum.TryParse(rowSelectedItemType, out ItemType selectedItemType))
                    {
                        SetControlValues(selectedRow, selectedItemType);
                    }
                    else
                    {
                        MessageBox.Show($"Invalid item type: {rowSelectedItemType}", "Invalid Item Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

        }

        private void ConfigureDataGridView()
        {
            if (dataGridView.InvokeRequired)
            {
                dataGridView.Invoke(new Action(ConfigureDataGridView));
                return;
            }

            dataGridView.VirtualMode = true;
            dataGridView.CellValueNeeded += DataGridView_CellValueNeeded;
            dataGridView.CellFormatting += DataGridView_CellFormatting;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.Blue;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Gold;

            // Set the row style
            DataGridViewCellStyle rowStyle = new()
            {
                BackColor = ColorTranslator.FromHtml("#1a1917"),
                ForeColor = Color.Gold
            };
            dataGridView.RowsDefaultCellStyle = rowStyle;

            // Set the column header style
            DataGridViewCellStyle headerStyle = new()
            {
                BackColor = ColorTranslator.FromHtml("#1a1917"),
                ForeColor = Color.Gold
            };

            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle = headerStyle;

            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.Columns["Icon"].Width = 36;

            dataGridView.RowTemplate.Height = 36;
            dataGridView.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["Name"].FillWeight = 100;

            SetColumnVisibility("ItemType", false);
            SetColumnVisibility("nID", false);
            SetColumnVisibility("wszDesc", false);
            SetColumnVisibility("nWeaponID00", false);
            SetColumnVisibility("nSocketCountMin", false);
            SetColumnVisibility("nSocketCountMax", false);
            SetColumnVisibility("nJobClass", false);
            SetColumnVisibility("nDefense", false);
            SetColumnVisibility("nMagicDefense", false);
            SetColumnVisibility("nOptionCountMin", false);
            SetColumnVisibility("nOptionCountMax", false);
            SetColumnVisibility("nSetId", false);
            SetColumnVisibility("nFixOption00", false);
            SetColumnVisibility("nFixOptionValue00", false);
            SetColumnVisibility("nFixOption01", false);
            SetColumnVisibility("nFixOptionValue01", false);
            SetColumnVisibility("nSocketCountMin", false);
            SetColumnVisibility("wszItemDescription", false);
            SetColumnVisibility("nBranch", false);
            SetColumnVisibility("nSocketCountMax", false);
            SetColumnVisibility("nReconstructionMax", false);
            SetColumnVisibility("nLevelLimit", false);
            SetColumnVisibility("nItemTrade", false);
            SetColumnVisibility("nOverlapCnt", false);
            SetColumnVisibility("nDurability", false);
            SetColumnVisibility("nWeight", false);
            SetColumnVisibility("nSellPrice", false);
            SetColumnVisibility("szIconName", false);
            SetColumnVisibility("nPetEatGroup", false);
            SetColumnVisibility("nCategory", false);
            SetColumnVisibility("nSubCategory", false);
        }

        private void DataGridView_CellValueNeeded(object? sender, DataGridViewCellValueEventArgs e)
        {
            if (itemDataTable != null)
            {
                if (e.RowIndex < itemDataTable.Rows.Count && e.ColumnIndex < itemDataTable.Columns.Count)
                {
                    e.Value = itemDataTable.Rows[e.RowIndex][e.ColumnIndex];
                }
            }
        }

        private void DataGridView_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dataGridView.Columns[e.ColumnIndex].Name == "Icon" && e.Value is string iconName)
                {
                    // Load the icon image and set it to the cell
                    Image iconImage = LoadIconImage(iconName);
                    e.Value = iconImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred Formatting DataGridView: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];

                    string? rowSelectedItemType = selectedRow.Cells["ItemType"].Value?.ToString();
                    if (Enum.TryParse(rowSelectedItemType, out ItemType selectedItemType))
                    {
                        SetControlValues(selectedRow, selectedItemType);
                    }
                    else
                    {
                        MessageBox.Show($"Invalid item type: {rowSelectedItemType}", "Invalid Item Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred in DataGridView: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetColumnVisibility(string columnName, bool isVisible)
        {
            if (dataGridView.Columns.Contains(columnName))
            {
                dataGridView.Columns[columnName].Visible = isVisible;
            }
        }

        public int itemID;

        private void SetControlValues(DataGridViewRow row, ItemType selectedItemType)
        {
            try
            {
                if (row == null)
                    return;

                if (InvokeRequired)
                {
                    Invoke(new Action(() => SetControlValues(row, selectedItemType)));
                    return;
                }

                if (selectedItemType != ItemType.Item)
                {
                    OpenGearFrame();
                    UpdateGearFrameValues(row);
                }
                else
                {
                    OpenItemFrame();
                    UpdateItemFrameValues(row);
                }

                UpdateControls(selectedItemType);

                // Retrieve item information from the selected DataGridViewRow
                int id = row.Cells["nID"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["nID"].Value) : 0;
                string? iconName = row.Cells["szIconName"].Value != DBNull.Value ? row.Cells["szIconName"].Value.ToString() : "";
                int socketCountMax = row.Cells["nSocketCountMax"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["nSocketCountMax"].Value) : 0;
                int reconstructionMax = row.Cells["nReconstructionMax"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["nReconstructionMax"].Value) : 0;
                int durability = row.Cells["nDurability"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["nDurability"].Value) : 0;
                int weight = row.Cells["nWeight"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["nWeight"].Value) : 0;
                int maxItemStack = row.Cells["nOverlapCnt"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["nOverlapCnt"].Value) : 0;
                int optionCountMax = row.Cells["nOptionCountMax"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["nOptionCountMax"].Value) : 0;
                itemID = id;

                numDurability.Minimum = 0;
                numDurability.Maximum = 100000;
                numMaxDurability.Minimum = 0;
                numMaxDurability.Maximum = 100000;
                numDurability.Value = Math.Min(durability, numDurability.Maximum);
                numMaxDurability.Value = Math.Min(durability, numMaxDurability.Maximum);
                numReconstructionMax.Maximum = reconstructionMax;
                numReconstructionMax.Enabled = reconstructionMax > 0;
                numReconstructionMax.Value = Math.Min(reconstructionMax, numReconstructionMax.Maximum);
                tbReconNum.Text = reconstructionMax.ToString();
                numSocketCount.Minimum = 0;
                numSocketCount.Maximum = socketCountMax;
                tbSocketCount.Text = socketCountMax.ToString();
                numSocketCount.Value = socketCountMax;
                numWeight.Value = weight;
                numAmount.Minimum = 1;
                if (maxItemStack == 0)
                {
                    numAmount.Maximum = 1;
                }
                else
                {
                    numAmount.Maximum = maxItemStack;
                }
                numAmount.Value = 1;

                gbSocket.Enabled = socketCountMax > 0;
                UpdateOptionControls(optionCountMax);

                OptionComboBox_SelectedIndexChanged(cbOptionCode1, EventArgs.Empty);
                OptionComboBox_SelectedIndexChanged(cbOptionCode2, EventArgs.Empty);
                OptionComboBox_SelectedIndexChanged(cbOptionCode3, EventArgs.Empty);

                OptionComboBox_SelectedIndexChanged(cbSocketCode1, EventArgs.Empty);
                OptionComboBox_SelectedIndexChanged(cbSocketCode2, EventArgs.Empty);
                OptionComboBox_SelectedIndexChanged(cbSocketCode3, EventArgs.Empty);

                pbItemIcon.Image = LoadIconImage(iconName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred in updating control values: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateOptionControls(int optionCountMax)
        {
            List<ComboBox> optionCodeComboBoxes = new List<ComboBox>
            {
                cbOptionCode1, cbOptionCode2, cbOptionCode3
            };

            List<NumericUpDown> optionValueNumericUpDowns = new List<NumericUpDown>
            {
                numOptionValue1, numOptionValue2, numOptionValue3
            };

            for (int i = 0; i < optionCodeComboBoxes.Count; i++)
            {
                bool isEnabled = i < optionCountMax;

                optionCodeComboBoxes[i].Enabled = isEnabled;
                optionValueNumericUpDowns[i].Enabled = isEnabled;

                // Adjust the Minimum property based on whether the control is enabled or disabled
                optionValueNumericUpDowns[i].Minimum = isEnabled ? 1 : 0;

                if (!isEnabled)
                {
                    // When disabled, set numOptionValue to 0 and cbOptionCode selected item to 0
                    optionValueNumericUpDowns[i].Value = 0;
                    optionCodeComboBoxes[i].SelectedIndex = 0;
                }
            }
        }



        private ItemFrame? itemFrame = null;

        private void OpenItemFrame()
        {
            try
            {
                if (itemFrame == null || itemFrame.IsDisposed)
                {
                    itemFrame = new ItemFrame
                    {
                        MdiParent = this
                    };

                    itemFrame.FormClosed += (sender, e) => itemFrame = null;
                }

                gearFrame?.Hide();

                if (!itemFrame.Visible)
                {
                    itemFrame.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static ItemData ExtractItemFromRow(DataGridViewRow row)
        {
            ItemData item = new()
            {
                Name = row.Cells["wszDesc"].Value as string ?? string.Empty,
                Description = row.Cells["wszItemDescription"].Value as string ?? string.Empty,
                Category = Convert.ToInt32(row.Cells["nCategory"].Value != DBNull.Value ? row.Cells["nCategory"].Value : 0),
                SubCategory = Convert.ToInt32(row.Cells["nSubCategory"].Value != DBNull.Value ? row.Cells["nSubCategory"].Value : 0),
                Weight = Convert.ToInt32(row.Cells["nWeight"].Value != DBNull.Value ? row.Cells["nWeight"].Value : 0),
                LevelLimit = Convert.ToInt32(row.Cells["nLevelLimit"].Value != DBNull.Value ? row.Cells["nLevelLimit"].Value : 0),
                ItemTrade = Convert.ToInt32(row.Cells["nItemTrade"].Value != DBNull.Value ? row.Cells["nItemTrade"].Value : 0),
                Branch = Convert.ToInt32(row.Cells["nBranch"].Value != DBNull.Value ? row.Cells["nBranch"].Value : 0),
                SellPrice = Convert.ToInt32(row.Cells["nSellPrice"].Value != DBNull.Value ? row.Cells["nSellPrice"].Value : 0),
                PetFood = Convert.ToInt32(row.Cells["nPetEatGroup"].Value != DBNull.Value ? row.Cells["nPetEatGroup"].Value : 0),
                JobClass = Convert.ToInt32(row.Cells["nJobClass"].Value != DBNull.Value ? row.Cells["nJobClass"].Value : 0),
            };

            return item;
        }

        private void UpdateItemFrameValues(DataGridViewRow row)
        {
            if (itemFrame != null && !itemFrame.IsDisposed)
            {
                ItemData item = ExtractItemFromRow(row);
                itemFrame.SetItemData(item);
            }
        }

        private GearFrame? gearFrame = null;
        private void OpenGearFrame()
        {
            try
            {
                if (gearFrame == null || gearFrame.IsDisposed)
                {
                    gearFrame = new GearFrame
                    {
                        MdiParent = this
                    };

                    gearFrame.FormClosed += (sender, e) => gearFrame = null;
                }

                itemFrame?.Hide();

                if (!gearFrame.Visible)
                {
                    gearFrame.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static ItemData ExtractGearItemFromRow(DataGridViewRow row)
        {
            ItemData gearItem = new()
            {
                Name = row.Cells["wszDesc"].Value as string ?? string.Empty,
                Description = row.Cells["wszItemDescription"].Value as string ?? string.Empty,
                Type = row.Cells["ItemType"].Value as string ?? string.Empty,
                Category = Convert.ToInt32(row.Cells["nCategory"].Value != DBNull.Value ? row.Cells["nCategory"].Value : 0),
                WeaponID00 = Convert.ToInt32(row.Cells["nWeaponID00"].Value != DBNull.Value ? row.Cells["nWeaponID00"].Value : 0),
                SubCategory = Convert.ToInt32(row.Cells["nSubCategory"].Value != DBNull.Value ? row.Cells["nSubCategory"].Value : 0),
                Weight = Convert.ToInt32(row.Cells["nWeight"].Value != DBNull.Value ? row.Cells["nWeight"].Value : 0),
                LevelLimit = Convert.ToInt32(row.Cells["nLevelLimit"].Value != DBNull.Value ? row.Cells["nLevelLimit"].Value : 0),
                ItemTrade = Convert.ToInt32(row.Cells["nItemTrade"].Value != DBNull.Value ? row.Cells["nItemTrade"].Value : 0),
                Durability = Convert.ToInt32(row.Cells["nDurability"].Value != DBNull.Value ? row.Cells["nDurability"].Value : 0),
                Defense = Convert.ToInt32(row.Cells["nDefense"].Value != DBNull.Value ? row.Cells["nDefense"].Value : 0),
                MagicDefense = Convert.ToInt32(row.Cells["nMagicDefense"].Value != DBNull.Value ? row.Cells["nMagicDefense"].Value : 0),
                Branch = Convert.ToInt32(row.Cells["nBranch"].Value != DBNull.Value ? row.Cells["nBranch"].Value : 0),
                SocketCountMin = Convert.ToInt32(row.Cells["nSocketCountMin"].Value != DBNull.Value ? row.Cells["nSocketCountMin"].Value : 0),
                SocketCountMax = Convert.ToInt32(row.Cells["nSocketCountMax"].Value != DBNull.Value ? row.Cells["nSocketCountMax"].Value : 0),
                ReconstructionMax = Convert.ToInt32(row.Cells["nReconstructionMax"].Value != DBNull.Value ? row.Cells["nReconstructionMax"].Value : 0),
                SellPrice = Convert.ToInt32(row.Cells["nSellPrice"].Value != DBNull.Value ? row.Cells["nSellPrice"].Value : 0),
                PetFood = Convert.ToInt32(row.Cells["nPetEatGroup"].Value != DBNull.Value ? row.Cells["nPetEatGroup"].Value : 0),
                JobClass = Convert.ToInt32(row.Cells["nJobClass"].Value != DBNull.Value ? row.Cells["nJobClass"].Value : 0),
                OptionCountMin = Convert.ToInt32(row.Cells["nOptionCountMin"].Value != DBNull.Value ? row.Cells["nOptionCountMin"].Value : 0),
                OptionCountMax = Convert.ToInt32(row.Cells["nOptionCountMax"].Value != DBNull.Value ? row.Cells["nOptionCountMax"].Value : 0),
                SetId = Convert.ToInt32(row.Cells["nSetId"].Value != DBNull.Value ? row.Cells["nSetId"].Value : 0),
                FixOption00 = Convert.ToInt32(row.Cells["nFixOption00"].Value != DBNull.Value ? row.Cells["nFixOption00"].Value : 0),
                FixOptionValue00 = Convert.ToInt32(row.Cells["nFixOptionValue00"].Value != DBNull.Value ? row.Cells["nFixOptionValue00"].Value : 0),
                FixOption01 = Convert.ToInt32(row.Cells["nFixOption01"].Value != DBNull.Value ? row.Cells["nFixOption01"].Value : 0),
                FixOptionValue01 = Convert.ToInt32(row.Cells["nFixOptionValue01"].Value != DBNull.Value ? row.Cells["nFixOptionValue01"].Value : 0)
            };

            return gearItem;
        }

        private void UpdateGearFrameValues(DataGridViewRow row)
        {
            if (gearFrame != null && !gearFrame.IsDisposed)
            {
                ItemData gearItem = ExtractGearItemFromRow(row);
                gearFrame.SetItemData(gearItem);
            }
        }

        private readonly Dictionary<string, Image> iconImageCache = new();

        private Image LoadIconImage(string? iconName)
        {
            try
            {
                if (iconName == null)
                {
                    return Properties.Resources.question_icon;
                }

                if (iconImageCache.TryGetValue(iconName, out var cachedImage))
                {
                    return cachedImage;
                }

                string[] subfolders = Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources"));

                foreach (string subfolder in subfolders)
                {
                    string[] imageFiles = Directory.GetFiles(subfolder, "*.png", SearchOption.AllDirectories);
                    string lowercaseIconName = iconName.ToLower();

                    foreach (string imageFilePath in imageFiles)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(imageFilePath);

                        if (string.Equals(fileName, lowercaseIconName, StringComparison.OrdinalIgnoreCase))
                        {
                            Image loadedImage = Image.FromFile(imageFilePath);
                            iconImageCache[iconName] = loadedImage; // Cache the loaded image
                            return loadedImage;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Properties.Resources.question_icon;

        }

        #endregion

        #region Add Item
        private void ButtonAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<MailData> mailDataList = mailForm!.MailDataList;

                int selectedItemID = itemID;
                string selectedItemType = cmbItemType.Text;

                int selectedOptionCode1ID = ((ItemOption)cbOptionCode1.SelectedItem).ID;
                int selectedOptionCode2ID = ((ItemOption)cbOptionCode2.SelectedItem).ID;
                int selectedOptionCode3ID = ((ItemOption)cbOptionCode3.SelectedItem).ID;

                int selectedSocketCode1ID = ((ItemOption)cbSocketCode1.SelectedItem).ID;
                int selectedSocketCode2ID = ((ItemOption)cbSocketCode2.SelectedItem).ID;
                int selectedSocketCode3ID = ((ItemOption)cbSocketCode3.SelectedItem).ID;

                string? selectedSocketColor1 = cbSocketColor1.SelectedItem?.ToString();
                string? selectedSocketColor2 = cbSocketColor2.SelectedItem?.ToString();
                string? selectedSocketColor3 = cbSocketColor3.SelectedItem?.ToString();

                int selectedSocketColorId1 = GetSocketColorIdOrDefault(selectedSocketColor1);
                int selectedSocketColorId2 = GetSocketColorIdOrDefault(selectedSocketColor2);
                int selectedSocketColorId3 = GetSocketColorIdOrDefault(selectedSocketColor3);

                MailData? existingMailData = mailDataList.FirstOrDefault(data => data.SlotIndex == activeItemIndex - 1);

                if (existingMailData != null)
                {
                    existingMailData.ItemType = selectedItemType;
                    existingMailData.ItemID = selectedItemID;
                    existingMailData.Amount = (int)numAmount.Value;
                    existingMailData.Durability = (int)numDurability.Value;
                    existingMailData.EnchantLevel = (int)numEnchantLevel.Value;
                    existingMailData.Rank = (int)numRank.Value;
                    existingMailData.ReconNum = (int)numReconstructionMax.Value;
                    existingMailData.ReconCount = int.Parse(tbReconNum.Text);
                    existingMailData.SocketCount = (int)numSocketCount.Value;
                    existingMailData.OptionCode1 = selectedOptionCode1ID;
                    existingMailData.OptionCode2 = selectedOptionCode2ID;
                    existingMailData.OptionCode3 = selectedOptionCode3ID;
                    existingMailData.OptionValue1 = (int)numOptionValue1.Value;
                    existingMailData.OptionValue2 = (int)numOptionValue2.Value;
                    existingMailData.OptionValue3 = (int)numOptionValue3.Value;
                    existingMailData.SocketColor1 = selectedSocketColorId1;
                    existingMailData.SocketColor2 = selectedSocketColorId2;
                    existingMailData.SocketColor3 = selectedSocketColorId3;
                    existingMailData.SocketCode1 = selectedSocketCode1ID;
                    existingMailData.SocketCode2 = selectedSocketCode2ID;
                    existingMailData.SocketCode3 = selectedSocketCode3ID;
                    existingMailData.SocketValue1 = (int)numSocketValue1.Value;
                    existingMailData.SocketValue2 = (int)numSocketValue2.Value;
                    existingMailData.SocketValue3 = (int)numSocketValue3.Value;
                    existingMailData.DurabilityMax = (int)numMaxDurability.Value;
                    existingMailData.Weight = (int)numWeight.Value;
                }
                else
                {
                    MailData mailData = new()
                    {
                        SlotIndex = activeItemIndex - 1,
                        ItemType = selectedItemType,
                        ItemID = selectedItemID,
                        Amount = (int)numAmount.Value,
                        Durability = (int)numDurability.Value,
                        EnchantLevel = (int)numEnchantLevel.Value,
                        Rank = (int)numRank.Value,
                        ReconNum = (int)numReconstructionMax.Value,
                        ReconCount = int.Parse(tbReconNum.Text),
                        SocketCount = (int)numSocketCount.Value,
                        OptionCode1 = selectedOptionCode1ID,
                        OptionCode2 = selectedOptionCode2ID,
                        OptionCode3 = selectedOptionCode3ID,
                        OptionValue1 = (int)numOptionValue1.Value,
                        OptionValue2 = (int)numOptionValue2.Value,
                        OptionValue3 = (int)numOptionValue3.Value,
                        SocketColor1 = selectedSocketColorId1,
                        SocketColor2 = selectedSocketColorId2,
                        SocketColor3 = selectedSocketColorId3,
                        SocketCode1 = selectedSocketCode1ID,
                        SocketCode2 = selectedSocketCode2ID,
                        SocketCode3 = selectedSocketCode3ID,
                        SocketValue1 = (int)numSocketValue1.Value,
                        SocketValue2 = (int)numSocketValue2.Value,
                        SocketValue3 = (int)numSocketValue3.Value,
                        DurabilityMax = (int)numMaxDurability.Value,
                        Weight = (int)numWeight.Value
                    };

                    mailDataList.Add(mailData);
                }

                mailForm.MailDataList = mailDataList;

                SetIconSlot();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred on adding item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetIconSlot()
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(SetIconSlot));
                    return;
                }

                if (dataGridView.SelectedRows.Count == 0)
                {
                    return;
                }

                DataGridViewRow selectedRow = dataGridView.SelectedRows[0];

                string? name = selectedRow.Cells["Name"].Value != DBNull.Value ? selectedRow.Cells["Name"].Value.ToString() : "";
                string? iconName = selectedRow.Cells["szIconName"].Value != DBNull.Value ? selectedRow.Cells["szIconName"].Value.ToString() : "";
                int branch = selectedRow.Cells["nBranch"].Value != DBNull.Value ? Convert.ToInt32(selectedRow.Cells["nBranch"].Value) : 0;

                Image image = LoadIconImage(iconName);
                int branchId = MapBranchIdToImageIndex(branch);
                int amount = (int)numAmount.Value;

                Dictionary<int, (PictureBox, PictureBox, PictureBox, Image, Label, int, int, bool, string?)> iconSlotMap = new()
                {
                    { 1, (mailForm!.pbIcon01, mailForm.pbItem01Branch, mailForm.pbEmpty1, image, mailForm.lbItemAmount1, amount, branchId, true, name) },
                    { 2, (mailForm.pbIcon02, mailForm.pbItem02Branch, mailForm.pbEmpty2, image, mailForm.lbItemAmount2, amount, branchId, true, name) },
                    { 3, (mailForm.pbIcon03, mailForm.pbItem03Branch, mailForm.pbEmpty3, image, mailForm.lbItemAmount3, amount, branchId, true, name) },
                };

                if (iconSlotMap.TryGetValue(activeItemIndex, out var iconSlot))
                {
                    string tooltip = iconSlot.Item9 ?? string.Empty;

                    mailForm.SetpbIconImage(iconSlot.Item1, iconSlot.Item2, iconSlot.Item3, iconSlot.Item4, iconSlot.Item5, iconSlot.Item6, iconSlot.Item7, iconSlot.Item8, tooltip);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred in updating icon slot: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static int MapBranchIdToImageIndex(int branchId)
        {
            return branchId switch
            {
                0 or 1 => 0,
                2 => 1,
                4 => 2,
                5 => 3,
                6 => 4,
                _ => 0,
            };
        }

        private static int GetSocketColorIdOrDefault(string? socketColor)
        {
            if (socketColor == null || !socketColorIdMap.TryGetValue(socketColor, out int colorId))
            {
                return 0;
            }

            return colorId;
        }
        #endregion

        #region Search
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
                e.Handled = true;
            }
        }

        private void Search()
        {
            try
            {
                if (filteredDataTable == null)
                {
                    MessageBox.Show("No data to search. Please load data first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string searchText = txtSearch.Text.Trim().ToLower();

                // Filter the DataTable based on the current ItemType if it's not "All"
                DataView dataView;
                if (selectedItemType != ItemType.All)
                {
                    dataView = new DataView(itemDataTable)
                    {
                        RowFilter = $"ItemType = {(int)selectedItemType}" // Convert enum to its underlying integer value
                    };
                }
                else
                {
                    dataView = new DataView(itemDataTable);
                }

                filteredDataTable = dataView.ToTable();

                if (string.IsNullOrEmpty(searchText))
                {
                    // If search text is empty, display the original filteredDataTable
                    dataGridView.DataSource = filteredDataTable;
                    string totalRows = dataGridView.RowCount.ToString();
                    lbTotal.Text = $"Showing: {totalRows} items";
                }
                else
                {
                    // Filter the DataTable rows based on the search text
                    DataTable searchFilteredDataTable = filteredDataTable.Clone();

                    foreach (DataRow row in filteredDataTable.Rows)
                    {
                        string itemName = (row["wszDesc"]?.ToString() ?? string.Empty).ToLower();
                        string itemId = (row["nID"]?.ToString() ?? string.Empty).ToLower();

                        // Case-insensitive partial match on item name and ID
                        if (itemName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                            itemId.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        {
                            searchFilteredDataTable.ImportRow(row);
                        }
                    }

                    dataGridView.DataSource = searchFilteredDataTable;
                    string totalRows = dataGridView.RowCount.ToString();
                    lbTotal.Text = $"Showing: {totalRows} items";

                    if (searchFilteredDataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No matching items found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Frame Form Configuration

        private int selectedOptionID;
        private readonly Dictionary<ComboBox, int> comboBoxOptionIDMap = new();

        private void OptionComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            try
            {
                if (sender is ComboBox comboBox)
                {
                    selectedOptionID = ((ItemOption)comboBox.SelectedItem).ID;
                    comboBoxOptionIDMap[comboBox] = selectedOptionID;

                    NumericUpDown? numericUpDown = null;
                    string? buffLabelName = null;

                    if (comboBox.Name.Contains("Option"))
                    {
                        int numericUpDownIndex = int.Parse(comboBox.Name[^1..]);
                        numericUpDown = Controls.Find($"numOptionValue{numericUpDownIndex}", true).FirstOrDefault() as NumericUpDown;
                        buffLabelName = $"lbRandomBuff{numericUpDownIndex:D2}";
                    }
                    else if (comboBox.Name.Contains("Socket"))
                    {
                        int numericUpDownIndex = int.Parse(comboBox.Name[^1..]);
                        numericUpDown = Controls.Find($"numSocketValue{numericUpDownIndex}", true).FirstOrDefault() as NumericUpDown;
                        buffLabelName = $"lbSockedBuff{numericUpDownIndex:D2}";
                    }

                    if (numericUpDown != null && buffLabelName != null)
                    {
                        // Retrieve values from the database based on the selected option ID
                        (int maxValue, int minValue) = DBQuery.GetOptionValues(selectedOptionID);

                        // Set the numericUpDown properties based on the retrieved values
                        numericUpDown.Minimum = minValue;
                        numericUpDown.Maximum = Math.Max(minValue, maxValue);
                        numericUpDown.Value = Math.Max(minValue, maxValue);

                        if (maxValue == 10000)
                        {
                            numericUpDown.Increment = 100;
                        }
                        else
                        {
                            numericUpDown.Increment = 10;
                        }

                        numericUpDown.ValueChanged -= OptionValue_ValueChanged;
                        comboBox.SelectedIndexChanged -= OptionComboBox_SelectedIndexChanged;

                        numericUpDown.ValueChanged += OptionValue_ValueChanged;
                        comboBox.SelectedIndexChanged += OptionComboBox_SelectedIndexChanged;

                        OptionValue_ValueChanged(numericUpDown, EventArgs.Empty);

                        // Check if the corresponding cbSocketCode is set to 0
                        int numericIndex = int.Parse(comboBox.Name[^1..]);
                        string socketCodeComboBoxName = $"cbSocketCode{numericIndex}";

                        if (Controls.Find(socketCodeComboBoxName, true).FirstOrDefault() is ComboBox socketCodeComboBox && socketCodeComboBox.SelectedIndex == 0)
                        {
                            // Call gearFrame.SetSocketColor with the selected color from the corresponding cbSocketColor
                            string socketColorComboBoxName = $"cbSocketColor{numericIndex}";

                            if (Controls.Find(socketColorComboBoxName, true).FirstOrDefault() is ComboBox socketColorComboBox && socketColorComboBox.SelectedItem != null)
                            {
                                string selectedColor = socketColorComboBox.SelectedItem?.ToString() ?? "None";

                                if (socketColorIdMap.TryGetValue(selectedColor, out int colorId))
                                {
                                    string socketLabelName = $"lbSockedBuff{numericIndex:D2}";

                                    if (gearFrame != null && !gearFrame.IsDisposed)
                                    {
                                        if (gearFrame.Controls.Find(socketLabelName, true).FirstOrDefault() is Label)
                                        {
                                            gearFrame.SetSocketColor(socketLabelName, colorId);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OptionValue_ValueChanged(object? sender, EventArgs e)
        {
            try
            {
                if (sender is NumericUpDown numericUpDown)
                {
                    if (gearFrame != null && !gearFrame.IsDisposed)
                    {
                        ComboBox associatedComboBox = comboBoxOptionIDMap.FirstOrDefault(x => x.Key.Name == numericUpDown.Name.Replace("numOptionValue", "cbOptionCode") ||
                                                                                                 x.Key.Name == numericUpDown.Name.Replace("numSocketValue", "cbSocketCode")).Key;
                        if (associatedComboBox != null && comboBoxOptionIDMap.TryGetValue(associatedComboBox, out int selectedOptionID))
                        {
                            gearFrame.SetBuffLabel(GetBuffLabelName(numericUpDown), selectedOptionID, (int)numericUpDown.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string GetBuffLabelName(NumericUpDown numericUpDown)
        {
            if (numericUpDown.Name.Contains("Option"))
            {
                int numericUpDownIndex = int.Parse(numericUpDown.Name[^1..]);
                return $"lbRandomBuff{numericUpDownIndex:D2}";
            }
            else if (numericUpDown.Name.Contains("Socket"))
            {
                int numericUpDownIndex = int.Parse(numericUpDown.Name[^1..]);
                return $"lbSockedBuff{numericUpDownIndex:D2}";
            }

            return string.Empty;
        }

        private void SocketColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is ComboBox comboBox)
                {
                    // Determine the corresponding PictureBox based on the ComboBox's name
                    string pictureBoxName = $"pbSocketColor{comboBox.Name[^1]}";

                    if (Controls.Find(pictureBoxName, true).FirstOrDefault() is PictureBox pictureBox && comboBox.SelectedItem != null)
                    {
                        string selectedColor = comboBox.SelectedItem?.ToString() ?? "None";

                        if (socketColorIdMap.TryGetValue(selectedColor, out int colorId))
                        {
                            // Set the image based on the colorId
                            pictureBox.Image = GetImageForColorId(colorId);

                            // Find the corresponding label name based on the numeric index in the ComboBox's name
                            int numericIndex = int.Parse(comboBox.Name[^1..]);
                            string socketLabelName = $"lbSockedBuff{numericIndex:D2}";

                            if (gearFrame != null && !gearFrame.IsDisposed)
                            {
                                // Check if the selected item of the corresponding cbSocketCode is 0
                                string socketCodeComboBoxName = $"cbSocketCode{numericIndex}";

                                if (Controls.Find(socketCodeComboBoxName, true).FirstOrDefault() is ComboBox socketCodeComboBox && socketCodeComboBox.SelectedIndex == 0)
                                {
                                    if (gearFrame.Controls.Find(socketLabelName, true).FirstOrDefault() is Label)
                                    {
                                        gearFrame.SetSocketColor(socketLabelName, colorId);
                                    }
                                }
                            }
                        }
                        else
                        {
                            pictureBox.Image = Properties.Resources.question_icon;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static Image GetImageForColorId(int colorId)
        {
            Dictionary<int, Image> imageMap = new()
            {
                { 0, Properties.Resources.lb_ac_icon_06_socket_a }, // None
                { 1, Properties.Resources.lb_ac_icon_06_socket_r }, // Red
                { 2, Properties.Resources.lb_ac_icon_06_socket_b }, // Blue
                { 3, Properties.Resources.lb_ac_icon_06_socket_y }, // Yellow
                { 4, Properties.Resources.lb_ac_icon_06_socket_g }, // Green
                { 5, Properties.Resources.lb_ac_icon_06_socket_all }, // Colorless
                { 6, Properties.Resources.lb_ac_icon_06_socket_a }, // Gray
                { 7, Properties.Resources.lb_ac_icon_06_socket_a }  // Default (Gray)
            };

            if (imageMap.TryGetValue(colorId, out Image? retrievedImage))
            {
                return retrievedImage ?? Properties.Resources.lb_ac_icon_06_socket_a;
            }

            return Properties.Resources.lb_ac_icon_06_socket_a;
        }

        private void NumDurability_ValueChanged(object sender, EventArgs e)
        {
            numDurability.Maximum = (int)numMaxDurability.Value;

            numDurability.Value = Math.Min(numDurability.Value, numDurability.Maximum);

            if (gearFrame != null && !gearFrame.IsDisposed)
            {
                int durability = (int)numDurability.Value;
                int maxDurability = (int)numMaxDurability.Value;

                gearFrame.UpdateDurability(durability, maxDurability);
            }
        }

        private void NumMaxDurability_ValueChanged(object sender, EventArgs e)
        {
            numDurability.Maximum = (int)numMaxDurability.Value;

            numDurability.Value = Math.Min(numDurability.Value, numDurability.Maximum);

            if (gearFrame != null && !gearFrame.IsDisposed)
            {
                int durability = (int)numDurability.Value;
                int maxDurability = (int)numMaxDurability.Value;

                gearFrame.UpdateDurability(durability, maxDurability);
            }
        }

        private void NumEnchantLevel_ValueChanged(object sender, EventArgs e)
        {
            if (gearFrame != null && !gearFrame.IsDisposed)
            {
                int enchantLevel = (int)numEnchantLevel.Value;
                gearFrame.UpdateItemName(enchantLevel);
            }
        }

        private void NumWeight_ValueChanged(object sender, EventArgs e)
        {
            if (gearFrame != null && !gearFrame.IsDisposed)
            {
                int weight = (int)numWeight.Value;
                gearFrame.UpdateWeight(weight);
            }
        }

        private void NumReconstructionMax_ValueChanged(object sender, EventArgs e)
        {
            if (gearFrame != null && !gearFrame.IsDisposed)
            {
                int attribute = (int)numReconstructionMax.Value;
                int reconNum = Convert.ToInt32(tbReconNum.Text);

                gearFrame.UpdateAttribute(attribute, reconNum);
            }

        }

        private void NumRank_ValueChanged(object sender, EventArgs e)
        {
            if (gearFrame != null && !gearFrame.IsDisposed)
            {
                int rank = (int)numRank.Value;
                gearFrame.UpdateRankText(rank);
            }
        }

        private void NumSocketCount_ValueChanged(object sender, EventArgs e)
        {
            int socketCount = (int)numSocketCount.Value;

            if (gearFrame != null && !gearFrame.IsDisposed)
            {
                gearFrame.UpdateSocketCountLabel(socketCount);
            }

            UpdateControlsBasedOnSocketCount(socketCount);
        }

        private void UpdateControlsBasedOnSocketCount(int socketCount)
        {
            var controls = new[]
            {
                (cbSocketColor1, cbSocketCode1, numSocketValue1),
                (cbSocketColor2, cbSocketCode2, numSocketValue2),
                (cbSocketColor3, cbSocketCode3, numSocketValue3)
            };

            foreach (var controlSet in controls)
            {
                controlSet.Item1.Enabled = false;
                controlSet.Item2.Enabled = false;
                controlSet.Item3.Enabled = false;
            }

            for (int i = 0; i < Math.Min(socketCount, controls.Length); i++)
            {
                controls[i].Item1.Enabled = true;
                controls[i].Item2.Enabled = true;
                controls[i].Item3.Enabled = true;
            }

            for (int i = socketCount; i < controls.Length; i++)
            {
                controls[i].Item1.SelectedIndex = 0;
                controls[i].Item2.SelectedIndex = 0;
                if (controls[i].Item3.Minimum <= 0 && 0 <= controls[i].Item3.Maximum)
                {
                    controls[i].Item3.Value = 0;
                }
            }
        }

        #endregion

        #region Controls Events

        private void Button_MouseHover(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.ImageIndex = 2;
            }
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.ImageIndex = 1;
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.ImageIndex = 0;
            }
        }

        #endregion
    }

}
