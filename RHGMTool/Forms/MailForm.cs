using Newtonsoft.Json;
using RHGMTool.Data;
using RHGMTool.Helper;
using System.Data;
using System.Data.SqlClient;

namespace RHGMTool
{
    public partial class MailForm : Form
    {
        #region Configuration Variables
        public readonly string _SQLServer;
        public readonly string _SQLUser;
        public readonly string _SQLPwd;
        private ToolTip? pictureBoxToolTip;
        #endregion
        public List<MailData> MailDataList { get; set; } = new List<MailData>();

        private ItemForm? itemForm1;
        private ItemForm? itemForm2;
        private ItemForm? itemForm3;

        public MailForm(string SQLServer, string SQLUser, string SQLPwd)
        {
            InitializeComponent();
            MailDataList = new List<MailData>();
            _SQLServer = SQLServer;
            _SQLUser = SQLUser;
            _SQLPwd = SQLPwd;

            ConfigureForm();
        }

        #region Form Event Handlers
        private void MailForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is MailForm mailForm)
            {
                MailDataList.Clear();
                mailForm.Dispose();
            }
        }

        private void ConfigureForm()
        {
            txtRecipient.BackColor = ColorTranslator.FromHtml("#1A1917");
            tbSender.BackColor = ColorTranslator.FromHtml("#1A1917");
            txtMailContent.BackColor = ColorTranslator.FromHtml("#1A1917");
            numMailGold.BackColor = ColorTranslator.FromHtml("#1A1917");
            numReturnDay.BackColor = ColorTranslator.FromHtml("#1A1917");
            numReqGold.BackColor = ColorTranslator.FromHtml("#1A1917");
        }

        #endregion

        #region Save Template

        private void BtnSaveTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                string mailSender = tbSender.Text.Trim();
                bool sendToAll = chkSendToAll.Checked;
                string recipient = txtRecipient.Text.Trim();
                string content = txtMailContent.Text.Trim();
                int gold = (int)numMailGold.Value;
                int reqGold = (int)numReqGold.Value;

                MailTemplateData templateData = new()
                {
                    Sender = mailSender,
                    Recipient = recipient,
                    SendToAll = sendToAll,
                    Content = content,
                    Gold = gold,
                    ReqGold = reqGold,
                    ItemTypes = MailDataList.Select(item => item.ItemType ?? "").ToList(),
                    ItemIDs = MailDataList.Select(item => item.ItemID).ToList(),
                    ItemAmounts = MailDataList.Select(item => item.Amount).ToList(),
                    Durabilities = MailDataList.Select(item => item.Durability).ToList(),
                    EnchantLevels = MailDataList.Select(item => item.EnchantLevel).ToList(),
                    Ranks = MailDataList.Select(item => item.Rank).ToList(),
                    ReconNums = MailDataList.Select(item => item.ReconNum).ToList(),
                    ReconStates = MailDataList.Select(item => item.ReconCount).ToList(),
                    OptionCodes1 = MailDataList.Select(item => item.OptionCode1).ToList(),
                    OptionCodes2 = MailDataList.Select(item => item.OptionCode2).ToList(),
                    OptionCodes3 = MailDataList.Select(item => item.OptionCode3).ToList(),
                    OptionValues1 = MailDataList.Select(item => item.OptionValue1).ToList(),
                    OptionValues2 = MailDataList.Select(item => item.OptionValue2).ToList(),
                    OptionValues3 = MailDataList.Select(item => item.OptionValue3).ToList(),
                    SocketCounts = MailDataList.Select(item => item.SocketCount).ToList(),
                    SocketColors1 = MailDataList.Select(item => item.SocketColor1).ToList(),
                    SocketColors2 = MailDataList.Select(item => item.SocketColor2).ToList(),
                    SocketColors3 = MailDataList.Select(item => item.SocketColor3).ToList(),
                    SocketCodes1 = MailDataList.Select(item => item.SocketCode1).ToList(),
                    SocketCodes2 = MailDataList.Select(item => item.SocketCode2).ToList(),
                    SocketCodes3 = MailDataList.Select(item => item.SocketCode3).ToList(),
                    SocketValues1 = MailDataList.Select(item => item.SocketValue1).ToList(),
                    SocketValues2 = MailDataList.Select(item => item.SocketValue2).ToList(),
                    SocketValues3 = MailDataList.Select(item => item.SocketValue3).ToList(),
                    DurabilityMaxValues = MailDataList.Select(item => item.DurabilityMax).ToList(),
                    WeightValues = MailDataList.Select(item => item.Weight).ToList(),
                };

                using SaveFileDialog saveFileDialog = new();
                saveFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string json = JsonConvert.SerializeObject(templateData);
                    json = json.Insert(1, "\"MailTemplate\": true,");
                    File.WriteAllText(saveFileDialog.FileName, json);

                    MessageBox.Show("Mail template saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving template: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Load Template
        private async void BtnLoadTemplate_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Clear();

                    string json = await File.ReadAllTextAsync(openFileDialog.FileName);

                    btnSaveTemplate.Enabled = false;
                    btnClear.Enabled = false;
                    btnSend.Enabled = false;
                    btnLoadTemplate.Enabled = false;
                    btnLoadTemplate.Text = "Loading...";

                    await LoadTemplateAsync(json);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading template: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task LoadTemplateAsync(string json)
        {
            try
            {
                await Task.Run(() =>
                {
                    LoadTemplate(json);
                });
            }
            finally
            {
                Invoke((MethodInvoker)delegate
                {
                    btnSaveTemplate.Enabled = true;
                    btnClear.Enabled = true;
                    btnSend.Enabled = true;
                    btnLoadTemplate.Enabled = true;
                    btnLoadTemplate.Text = "Load Template";
                });
            }
        }

        private void LoadTemplate(string json)
        {
            if (json.Contains("\"MailTemplate\": true"))
            {
                MailTemplateData? templateData = JsonConvert.DeserializeObject<MailTemplateData>(json);

                if (templateData != null)
                {
                    MailDataList.Clear();

                    UpdateTextBox(tbSender, templateData.Sender);
                    UpdateTextBox(txtRecipient, templateData.Recipient);
                    UpdateTextBox(txtMailContent, templateData.Content);

                    UpdateCheckBox(chkSendToAll, templateData.SendToAll);
                    UpdateNumericUpDown(numMailGold, templateData.Gold);
                    UpdateNumericUpDown(numReqGold, templateData.ReqGold);

                    if (templateData.ItemTypes != null && templateData.ItemTypes.Count > 0)
                    {
                        for (int i = 0; i < templateData.ItemTypes.Count; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    itemForm1 = UpdateItemForm(itemForm1, templateData, i + 1);
                                    break;
                                case 1:
                                    itemForm2 = UpdateItemForm(itemForm2, templateData, i + 1);
                                    break;
                                case 2:
                                    itemForm3 = UpdateItemForm(itemForm3, templateData, i + 1);
                                    break;
                            }
                        }
                    }

                    if (templateData != null && templateData.ItemTypes != null)
                    {
                        for (int i = 0; i < templateData.ItemTypes.Count; i++)
                        {
                            MailData mailData = new()
                            {
                                SlotIndex = i,


                                ItemType = templateData.ItemTypes[i] ?? "",
                                ItemID = templateData.ItemIDs?[i] ?? 0,
                                Amount = templateData.ItemAmounts?[i] ?? 0,
                                Durability = templateData.Durabilities?[i] ?? 0,
                                EnchantLevel = templateData.EnchantLevels?[i] ?? 0,
                                Rank = templateData.Ranks?[i] ?? 0,
                                ReconNum = templateData.ReconNums?[i] ?? 0,
                                ReconCount = templateData.ReconStates?[i] ?? 0,
                                OptionCode1 = templateData.OptionCodes1?[i] ?? 0,
                                OptionCode2 = templateData.OptionCodes2?[i] ?? 0,
                                OptionCode3 = templateData.OptionCodes3?[i] ?? 0,
                                OptionValue1 = templateData.OptionValues1?[i] ?? 0,
                                OptionValue2 = templateData.OptionValues2?[i] ?? 0,
                                OptionValue3 = templateData.OptionValues3?[i] ?? 0,
                                SocketCount = templateData.SocketCounts?[i] ?? 0,
                                SocketColor1 = templateData.SocketColors1?[i] ?? 0,
                                SocketColor2 = templateData.SocketColors2?[i] ?? 0,
                                SocketColor3 = templateData.SocketColors3?[i] ?? 0,
                                SocketCode1 = templateData.SocketCodes1?[i] ?? 0,
                                SocketCode2 = templateData.SocketCodes2?[i] ?? 0,
                                SocketCode3 = templateData.SocketCodes3?[i] ?? 0,
                                SocketValue1 = templateData.SocketValues1?[i] ?? 0,
                                SocketValue2 = templateData.SocketValues2?[i] ?? 0,
                                SocketValue3 = templateData.SocketValues3?[i] ?? 0,
                                DurabilityMax = templateData.DurabilityMaxValues?[i] ?? 0,
                                Weight = templateData.WeightValues?[i] ?? 0,
                            };

                            MailDataList.Add(mailData);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Failed to load JSON template or JSON is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid template file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void UpdateTextBox(TextBox textBox, string? value)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action(() => UpdateTextBox(textBox, value)));
            }
            else
            {
                textBox.Text = value;
            }
        }

        private static void UpdateCheckBox(CheckBox checkBox, bool value)
        {
            if (checkBox.InvokeRequired)
            {
                checkBox.Invoke(new Action(() => UpdateCheckBox(checkBox, value)));
            }
            else
            {
                checkBox.Checked = value;
            }
        }

        private static void UpdateNumericUpDown(NumericUpDown numericUpDown, decimal value)
        {
            if (numericUpDown.InvokeRequired)
            {
                numericUpDown.Invoke(new Action(() => UpdateNumericUpDown(numericUpDown, value)));
            }
            else
            {
                numericUpDown.Value = value;
            }
        }

        private static void UpdateComboBox(ComboBox comboBox, string value)
        {
            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new Action(() => UpdateComboBox(comboBox, value)));
            }
            else
            {
                comboBox.SelectedItem = comboBox.Items.Cast<object>().FirstOrDefault(item => item.ToString() == value);
            }
        }

        private ItemForm UpdateItemForm(ItemForm? itemForm, MailTemplateData templateData, int itemIndex)
        {
            if (itemForm == null || itemForm.IsDisposed)
            {
                itemForm = new ItemForm(this, itemIndex);
            }

            itemForm.SetTemplateData(templateData, itemIndex);

            return itemForm;
        }

        #endregion

        #region Send Mail

        private void BtnSend_Click(object sender, EventArgs e)
        {
            string server = _SQLServer.Trim();
            string sqlUser = _SQLUser.Trim();
            string sqlPwd = _SQLPwd.Trim();
            string content = txtMailContent.Text.Trim();
            content = content.Replace("'", "''");
            int gold = (int)numMailGold.Value;
            int reqGold = (int)numReqGold.Value;
            int createType = (reqGold != 0) ? 5 : 0;

            string connectionString = $"Data Source={server};Initial Catalog=RustyHearts;User Id={sqlUser};Password={sqlPwd};";

            List<string> failedRecipients = new();

            bool sendToAllCharacters = chkSendToAll.Checked;

            string[] recipients;

            if (string.IsNullOrEmpty(txtRecipient.Text) && !sendToAllCharacters)
            {
                MessageBox.Show("Recipient cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (sendToAllCharacters)
            {
                recipients = GetAllCharacterNames(connectionString);
            }
            else
            {
                // Split recipients by comma and trim any extra spaces
                recipients = txtRecipient.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(r => r.Trim())
                                        .ToArray();

                // Validate if any recipient is empty or contains non-letter characters
                if (recipients.Any(string.IsNullOrEmpty) || recipients.Any(r => !r.All(char.IsLetter)))
                {
                    MessageBox.Show("Recipient names must contain only letters and cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check for duplicate recipients
                HashSet<string> uniqueRecipients = new(StringComparer.OrdinalIgnoreCase);
                foreach (var recipient in recipients)
                {
                    if (!uniqueRecipients.Add(recipient))
                    {
                        MessageBox.Show($"Duplicate recipient found: {recipient}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(sqlUser) || string.IsNullOrEmpty(sqlPwd))
            {
                MessageBox.Show("Server address, SQL account, and SQL password cannot be empty!", "SQL Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string confirmationMessage = sendToAllCharacters
                ? "Send this mail to all characters?"
                : $"Send this mail to the following recipients?\n{string.Join(", ", recipients)}";

            DialogResult result = MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string recipient = string.Empty;
                try
                {

                    foreach (var currentRecipient in recipients)
                    {
                        recipient = currentRecipient;

                        using SqlConnection connection = new(connectionString);
                        connection.Open();

                        string characterQuery = $"SELECT character_id, authid FROM CharacterTable WHERE name='{recipient}'";

                        string? characterId = null;
                        string? authId = null;

                        using (SqlCommand characterCommand = new(characterQuery, connection))
                        using (SqlDataReader characterReader = characterCommand.ExecuteReader())
                        {
                            if (characterReader.Read())
                            {
                                characterId = characterReader[0].ToString();
                                authId = characterReader[1].ToString();
                            }

                            if (string.IsNullOrEmpty(characterId) || string.IsNullOrEmpty(authId))
                            {
                                MessageBox.Show($"The recipient ({recipient}) does not exist.", "Failed to send Mail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        string mailId = Guid.NewGuid().ToString();
                        string mailSender = tbSender.Text;
                        string returnDay = numReturnDay.Text;
                        string insertMailQuery = "INSERT INTO mailtable (id, character_id, receiver, send_character_id, sender, msg, gold, return_day, req_gold, is_open, create_time, create_type) " +
                            $"VALUES ('{mailId}', convert(uniqueidentifier, '{characterId}'), '{recipient}', convert(uniqueidentifier, '{characterId}'), '{mailSender}', N'{content}<br><br><right>' + CONVERT(NVARCHAR, GETDATE(), 120), {gold}, {returnDay}, {reqGold}, 0, GETDATE(), {createType})";

                        using (SqlCommand insertMailCommand = new(insertMailQuery, connection))
                        {
                            insertMailCommand.ExecuteNonQuery();
                        }

                        // Iterate over MailDataList and insert item data for each MailData
                        foreach (MailData mailData in MailDataList)
                        {
                            int slotIndex = mailData.SlotIndex;
                            string? itemType = mailData.ItemType;
                            int itemID = mailData.ItemID;
                            int itemCount = mailData.Amount;
                            int durabilityValue = mailData.Durability;
                            int enchantLevelValue = mailData.EnchantLevel;
                            int rankValue = mailData.Rank;
                            int reconCountValue = mailData.ReconCount;
                            int reconNumValue = mailData.ReconNum;
                            int socketCountValue = mailData.SocketCount;
                            int optionCode1Value = mailData.OptionCode1;
                            int optionCode2Value = mailData.OptionCode2;
                            int optionCode3Value = mailData.OptionCode3;
                            int optionValue1Value = mailData.OptionValue1;
                            int optionValue2Value = mailData.OptionValue2;
                            int optionValue3Value = mailData.OptionValue3;
                            int socketColor1Value = mailData.SocketColor1;
                            int socketColor2Value = mailData.SocketColor2;
                            int socketColor3Value = mailData.SocketColor3;
                            int socketCode1Value = mailData.SocketCode1;
                            int socketCode2Value = mailData.SocketCode2;
                            int socketCode3Value = mailData.SocketCode3;
                            int socketValue1Value = mailData.SocketValue1;
                            int socketValue2Value = mailData.SocketValue2;
                            int socketValue3Value = mailData.SocketValue3;
                            int durabilityMaxValue = mailData.DurabilityMax;
                            int weightValue = mailData.Weight;

                            string insertItemQuery = "INSERT INTO n_mailitem (item_uid, mail_uid, character_id, auth_id, page_index, slot_index, code, use_cnt, remain_time, create_time, update_time, gcode, durability, enhance_level, option_1_code, option_1_value, option_2_code, option_2_value, option_3_code, option_3_value, option_group, ReconNum, ReconState, socket_count, socket_1_code, socket_1_value, socket_2_code, socket_2_value, socket_3_code, socket_3_value, expire_time, lock_pwd, activity_value, link_id, is_seizure, socket_1_color, socket_2_color, socket_3_color, rank, acquireroute, physical, magical, durabilitymax, weight) " +
                                "VALUES (NEWID(), @MailId, @CharacterId, @AuthId, 61, @SlotIndex, @Code, @ItemCount, 0, GETDATE(), GETDATE(), 36, @Durability, @EnchantLevel, @OptionCode1, @OptionValue1, @OptionCode2, @OptionValue2, @OptionCode3, @OptionValue3, 0, @ReconNum, @ReconState, @SocketCount, @SocketCode1, @SocketValue1, @SocketCode2, @SocketValue2, @SocketCode3, @SocketValue3, 0, '', 0, '00000000-0000-0000-0000-000000000000', 0, @SocketColor1, @SocketColor2, @SocketColor3, @Rank, 3, 0, 0, @DurabilityMax, @Weight)";

                            using SqlCommand insertItemCommand = new(insertItemQuery, connection);
                            {
                                insertItemCommand.Parameters.AddWithValue("@MailId", mailId);
                                insertItemCommand.Parameters.AddWithValue("@CharacterId", characterId);
                                insertItemCommand.Parameters.AddWithValue("@AuthId", authId);
                                insertItemCommand.Parameters.AddWithValue("@SlotIndex", slotIndex);
                                insertItemCommand.Parameters.AddWithValue("@Code", itemID);
                                insertItemCommand.Parameters.AddWithValue("@Durability", durabilityValue);
                                insertItemCommand.Parameters.AddWithValue("@EnchantLevel", enchantLevelValue);
                                insertItemCommand.Parameters.AddWithValue("@ItemCount", itemCount);
                                insertItemCommand.Parameters.AddWithValue("@Rank", rankValue);
                                insertItemCommand.Parameters.AddWithValue("@ReconNum", reconNumValue);
                                insertItemCommand.Parameters.AddWithValue("@ReconState", reconCountValue);
                                insertItemCommand.Parameters.AddWithValue("@SocketCount", socketCountValue);
                                insertItemCommand.Parameters.AddWithValue("@OptionCode1", optionCode1Value);
                                insertItemCommand.Parameters.AddWithValue("@OptionCode2", optionCode2Value);
                                insertItemCommand.Parameters.AddWithValue("@OptionCode3", optionCode3Value);
                                insertItemCommand.Parameters.AddWithValue("@OptionValue1", optionValue1Value);
                                insertItemCommand.Parameters.AddWithValue("@OptionValue2", optionValue2Value);
                                insertItemCommand.Parameters.AddWithValue("@OptionValue3", optionValue3Value);
                                insertItemCommand.Parameters.AddWithValue("@SocketColor1", socketColor1Value);
                                insertItemCommand.Parameters.AddWithValue("@SocketColor2", socketColor2Value);
                                insertItemCommand.Parameters.AddWithValue("@SocketColor3", socketColor3Value);
                                insertItemCommand.Parameters.AddWithValue("@SocketCode1", socketCode1Value);
                                insertItemCommand.Parameters.AddWithValue("@SocketCode2", socketCode2Value);
                                insertItemCommand.Parameters.AddWithValue("@SocketCode3", socketCode3Value);
                                insertItemCommand.Parameters.AddWithValue("@SocketValue1", socketValue1Value);
                                insertItemCommand.Parameters.AddWithValue("@SocketValue2", socketValue2Value);
                                insertItemCommand.Parameters.AddWithValue("@SocketValue3", socketValue3Value);
                                insertItemCommand.Parameters.AddWithValue("@DurabilityMax", durabilityMaxValue);
                                insertItemCommand.Parameters.AddWithValue("@Weight", weightValue);

                                insertItemCommand.ExecuteNonQuery();
                            }
                        }


                    }
                    if (sendToAllCharacters)
                    {
                        MessageBox.Show($"The letter has been sent successfully to all characters. Please re-login the character/change game server to view the mail.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"The letter has been sent successfully. Please re-login the character/change game server to view the mail for the selected recipients.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    failedRecipients.Add(recipient);
                }

                if (failedRecipients.Count > 0)
                {
                    string failedRecipientsMessage = "Failed to send mails to the following recipients:\n" +
                                                     string.Join("\n", failedRecipients);

                    MessageBox.Show(failedRecipientsMessage, "Failed to Send Mails", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static string[] GetAllCharacterNames(string connectionString)
        {
            List<string> characterNames = new();

            try
            {
                using SqlConnection connection = new(connectionString);
                connection.Open();

                string query = "SELECT name FROM CharacterTable";

                using SqlCommand command = new(query, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string? characterName = reader["name"]?.ToString()?.Trim();

                    if (characterName != null)
                    {
                        characterNames.Add(characterName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving character names: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return characterNames.ToArray();
        }

        private void ChkSendToAll_CheckedChanged(object sender, EventArgs e)
        {
            txtRecipient.Enabled = !chkSendToAll.Checked;
        }
        #endregion

        #region Clear Form

        private void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Clear all mail values?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Clear()
        {
            try
            {
                // Clear text boxes and set default values
                txtRecipient.Text = string.Empty;
                tbSender.Text = "GameMaster";
                txtMailContent.Text = "Game Master InsertItem";
                numMailGold.Value = 0;
                numReqGold.Value = 0;
                numReturnDay.Value = 7;

                // Clear PictureBoxes and associated labels
                SetpbIconImage(pbIcon01, pbItem01Branch, pbEmpty1, null, lbItemAmount1, 0, 0, false, "");
                SetpbIconImage(pbIcon02, pbItem02Branch, pbEmpty2, null, lbItemAmount2, 0, 0, false, "");
                SetpbIconImage(pbIcon03, pbItem03Branch, pbEmpty3, null, lbItemAmount3, 0, 0, false, "");

                // Clear MailDataList
                MailDataList.Clear();

                // Dispose item forms if they exist
                itemForm1?.Dispose();
                itemForm2?.Dispose();
                itemForm3?.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while clearing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Item Methods

        public void SetpbIconImage(PictureBox pbIcon, PictureBox pbItemBranch, PictureBox pbEmpty, Image? image, Label lbItemAmount, int amount, int branch, bool visible, string tooltip)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => SetpbIconImage(pbIcon, pbItemBranch, pbEmpty, image, lbItemAmount, amount, branch, visible, tooltip)));
                    return;
                }

                pictureBoxToolTip = new ToolTip();

                pbIcon.Parent = pbEmpty;
                pbItemBranch.Parent = pbEmpty;
                lbItemAmount.Parent = pbIcon;

                pbIcon.Visible = visible;
                pbIcon.Image = image;
                pbIcon.Location = new Point(4, 4);
                pictureBoxToolTip.SetToolTip(pbIcon, tooltip);

                pbItemBranch.Visible = visible;
                pbItemBranch.Image = imageListBranch.Images[branch];
                pbItemBranch.Location = new Point(1, 1);
                pictureBoxToolTip.SetToolTip(pbItemBranch, tooltip);

                lbItemAmount.Location = new Point(0, 20);
                lbItemAmount.Visible = amount > 1;
                lbItemAmount.Text = $"{amount}";
                lbItemAmount.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An setting icon image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Attach Item
        private void PbItemAttach_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                PictureBox pictureBox = (PictureBox)sender;

                if (pictureBox.Tag == null)
                {
                    return;
                }

                int slotIndex = Convert.ToInt32(pictureBox.Tag);

                if (e.Button == MouseButtons.Right)
                {
                    // Remove the item from the list based on SlotIndex
                    RemoveItemBySlotIndex(slotIndex);

                    switch (slotIndex)
                    {
                        case 0:
                            SetpbIconImage(pbIcon01, pbItem01Branch, pbEmpty1, null, lbItemAmount1, 0, 0, false, "");
                            itemForm1?.Dispose();
                            break;
                        case 1:
                            SetpbIconImage(pbIcon02, pbItem02Branch, pbEmpty2, null, lbItemAmount2, 0, 0, false, "");
                            itemForm2?.Dispose();
                            break;
                        case 2:
                            SetpbIconImage(pbIcon03, pbItem03Branch, pbEmpty3, null, lbItemAmount3, 0, 0, false, "");
                            itemForm3?.Dispose();
                            break;
                    }
                }
                else if (e.Button == MouseButtons.Left)
                {
                    switch (slotIndex)
                    {
                        case 0:
                            if (itemForm1 == null || itemForm1.IsDisposed)
                            {
                                itemForm1 = new ItemForm(this, 1);
                            }

                            itemForm1.ShowDialog();
                            break;
                        case 1:
                            if (itemForm2 == null || itemForm2.IsDisposed)
                            {
                                itemForm2 = new ItemForm(this, 2);
                            }

                            itemForm2.ShowDialog();
                            break;
                        case 2:
                            if (itemForm3 == null || itemForm3.IsDisposed)
                            {
                                itemForm3 = new ItemForm(this, 3);
                            }

                            itemForm3.ShowDialog();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveItemBySlotIndex(int slotIndexToRemove)
        {
            List<MailData> mailDataList = MailDataList;

            MailData? mailDataToRemove = mailDataList.FirstOrDefault(data => data.SlotIndex == slotIndexToRemove);

            if (mailDataToRemove != null)
            {
                mailDataList.Remove(mailDataToRemove);

                mailDataList = mailDataList.OrderBy(data => data.SlotIndex).ToList();

                MailDataList = mailDataList;
            }
        }
        #endregion

        #region Controls Event Handlers
        private void MailForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FormUtils.MoveForm(Handle);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnClose_MouseHover(object sender, EventArgs e)
        {
            btnClose.ImageIndex = 1;
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.ImageIndex = 0;
        }

        private void BtnClose_MouseDown(object sender, MouseEventArgs e)
        {
            btnClose.ImageIndex = 2;
        }
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
