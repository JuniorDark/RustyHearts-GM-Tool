using System.Data.SQLite;
using static RHGMTool.Helper.EnumMapper;

namespace RHGMTool.Data
{
    public class DBQuery
    {
        public static void PopulateOptionCodeComboBox(ComboBox comboBox)
        {
            try
            {
                using var connection = SQLiteDBConnection.OpenDatabaseConnection();
                if (connection == null) return;

                using var optionCommand = new SQLiteCommand("SELECT nID, wszDescNoColor FROM itemoptionlist", connection);
                using var optionReader = optionCommand.ExecuteReader();

                comboBox.Items.Clear();
                comboBox.Items.Add(new ItemOption { ID = 0, Name = "None" });

                while (optionReader.Read())
                {
                    int id = optionReader.GetInt32(0);
                    string name = optionReader.GetString(1);

                    comboBox.Items.Add(new ItemOption { ID = id, Name = name });
                }

                comboBox.DropDown += ComboBox_DropDown;
                comboBox.DisplayMember = "Name";
                comboBox.ValueMember = "ID";
                comboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating the option code combo box: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void PopulateCategoryComboBox(ComboBox comboBox, ItemType itemType)
        {
            try
            {
                using var connection = SQLiteDBConnection.OpenDatabaseConnection();
                if (connection == null) return;

                string query;

                // If itemType is "All" or "Item," retrieve all categories
                if (itemType == ItemType.All || itemType == ItemType.Item)
                {
                    query = "SELECT nID, wszName00 FROM itemcategory WHERE wszName00 <> ''";
                }
                else
                {
                    // Otherwise, get the appropriate nID values based on itemType
                    int[] categoryIDs = GetCategoryIDsForItemType(itemType);
                    string categoryIDsStr = string.Join(",", categoryIDs);
                    query = $"SELECT nID, wszName00 FROM itemcategory WHERE nID IN ({categoryIDsStr}) AND wszName00 <> ''";
                }

                using var categoryCommand = new SQLiteCommand(query, connection);
                using var categoryReader = categoryCommand.ExecuteReader();

                comboBox.Items.Clear();
                comboBox.Items.Add(new ItemOption { ID = 0, Name = "All" }); // Add an option to show all categories

                while (categoryReader.Read())
                {
                    int id = categoryReader.GetInt32(0);
                    string name = categoryReader.GetString(1);

                    comboBox.Items.Add(new ItemOption { ID = id, Name = name });
                }

                comboBox.DisplayMember = "Name";
                comboBox.ValueMember = "ID";
                comboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while populating the {itemType} category combo box: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void PopulateSubCategoryComboBox(ComboBox comboBox, ItemType itemType)
        {
            try
            {
                using var connection = SQLiteDBConnection.OpenDatabaseConnection();
                if (connection == null) return;

                string query;

                // If itemType is "All" or "Item," retrieve all subcategories
                if (itemType == ItemType.All || itemType == ItemType.Item)
                {
                    query = "SELECT nID, wszName01 FROM itemcategory WHERE wszName01 <> ''";
                }
                else
                {
                    // Otherwise, get the appropriate nID values based on itemType
                    int[] subCategoryIDs = GetSubCategoryIDsForItemType(itemType);
                    string subCategoryIDsStr = string.Join(",", subCategoryIDs);
                    query = $"SELECT nID, wszName01 FROM itemcategory WHERE nID IN ({subCategoryIDsStr}) AND wszName01 <> ''";
                }

                using var subCategoryCommand = new SQLiteCommand(query, connection);
                using var subCategoryReader = subCategoryCommand.ExecuteReader();

                comboBox.Items.Clear();
                comboBox.Items.Add(new ItemOption { ID = 0, Name = "All" }); // Add an option to show all subcategories

                while (subCategoryReader.Read())
                {
                    int id = subCategoryReader.GetInt32(0);
                    string name = subCategoryReader.GetString(1);

                    comboBox.Items.Add(new ItemOption { ID = id, Name = name });
                }

                comboBox.DisplayMember = "Name";
                comboBox.ValueMember = "ID";
                comboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while populating the {itemType} subcategory combo box: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static int[] GetCategoryIDsForItemType(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.Weapon => new int[] { 5, 6, 7, 8, 9, 10, 11, 12, 55, 56, 57, 58 },
                ItemType.Armor => new int[] { 1, 2, 3, 4, 17 },
                ItemType.Costume => new int[] { 18 },
                _ => Array.Empty<int>(),
            };
        }

        private static int[] GetSubCategoryIDsForItemType(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.Weapon => new int[] { 1 },
                ItemType.Armor => new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                ItemType.Costume => new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 48, 101, 102, 104, 105, 106, 107, 108, 109 },
                _ => Array.Empty<int>(),
            };
        }

        private static void ComboBox_DropDown(object? sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                int maxDropdownWidth = 300;

                int maxWidth = comboBox.Items.Cast<object>()
                    .Select(item => TextRenderer.MeasureText(comboBox.GetItemText(item)?.ToString() ?? "", comboBox.Font).Width + 10)
                    .Max();

                int finalDropdownWidth = Math.Min(maxWidth, maxDropdownWidth);

                comboBox.DropDownWidth = finalDropdownWidth;
            }
        }

        public static (int maxValue, int minValue) GetOptionValues(int? itemID)
        {
            try
            {
                using var connection = SQLiteDBConnection.OpenDatabaseConnection();
                if (connection == null) return (0, 0);

                using var optionCommand = new SQLiteCommand($"SELECT nCheckMinValue, nCheckMaxValue FROM itemoptionlist WHERE nID = {itemID}", connection);
                using var optionReader = optionCommand.ExecuteReader();

                if (optionReader.Read())
                {
                    int minValue = optionReader.GetInt32(0);
                    int maxValue = optionReader.GetInt32(1);
                    return (maxValue, minValue);
                }

                return (0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving option values from the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (0, 0);
            }
        }

        public static string GetCategoryName(int categoryID, SQLiteConnection connection)
        {
            using SQLiteCommand categoryCommand = new($"SELECT wszName00 FROM itemcategory WHERE nID = {categoryID}", connection);
            using SQLiteDataReader categoryReader = categoryCommand.ExecuteReader();
            return categoryReader.Read() ? categoryReader.GetString(0) : string.Empty;
        }

        public static string GetSubCategoryName(int categoryID, SQLiteConnection connection)
        {
            using SQLiteCommand categoryCommand = new($"SELECT wszName01 FROM itemcategory WHERE nID = {categoryID}", connection);
            using SQLiteDataReader categoryReader = categoryCommand.ExecuteReader();
            return categoryReader.Read() ? categoryReader.GetString(0) : string.Empty;
        }

        public static string GetSetName(int setId, SQLiteConnection connection)
        {
            using SQLiteCommand setIdCommand = new($"SELECT wszName FROM setitem_string WHERE nID = {setId}", connection);
            using SQLiteDataReader setIdReader = setIdCommand.ExecuteReader();
            return setIdReader.Read() ? setIdReader.GetString(0) : string.Empty;
        }

        public static string GetOptionName(int optionID, SQLiteConnection connection)
        {
            using SQLiteCommand optionCommand = new($"SELECT wszDesc FROM itemoptionlist WHERE nID = {optionID}", connection);
            using SQLiteDataReader optionReader = optionCommand.ExecuteReader();
            return optionReader.Read() ? optionReader.GetString(0) : string.Empty;
        }

        public static (int secTime, float value, int maxValue) GetOptionValues(int optionID, SQLiteConnection connection)
        {
            using SQLiteCommand optionCommand = new($"SELECT nSecTime, fValue, nCheckMaxValue FROM itemoptionlist WHERE nID = {optionID}", connection);
            using SQLiteDataReader optionReader = optionCommand.ExecuteReader();
            return optionReader.Read() ? (optionReader.GetInt32(0), optionReader.GetFloat(1), optionReader.GetInt32(2)) : (0, 0, 0);
        }

        public static (int nPhysicalAttackMin, int nPhysicalAttackMax, int nMagicAttackMin, int nMagicAttackMax) GetWeaponStats(int jbClass, int WeaponID00, SQLiteConnection connection)
        {
            string tableName;
            switch (jbClass)
            {
                case 1:
                    tableName = "frantzweapon";
                    break;
                case 2:
                    tableName = "angelaweapon";
                    break;
                case 3:
                    tableName = "tudeweapon";
                    break;
                case 4:
                    tableName = "natashaweapon";
                    break;
                case 0:
                    return (0, 0, 0, 0);
                default:
                    return (0, 0, 0, 0);
            }

            string query = $"SELECT nPhysicalAttackMin, nPhysicalAttackMax, nMagicAttackMin, nMagicAttackMax FROM {tableName} WHERE nID = {WeaponID00}";

            using SQLiteCommand command = new(query, connection);
            using SQLiteDataReader reader = command.ExecuteReader();
            return reader.Read() ? (reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3)) : (0, 0, 0, 0);
        }
    }

}
