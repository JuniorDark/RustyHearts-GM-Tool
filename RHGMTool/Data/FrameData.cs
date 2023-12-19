using static RHGMTool.Helper.EnumMapper;

namespace RHGMTool.Data
{
    public class FrameData
    {
        public static void SetItemNameColor(Label label, int branch)
        {
            label.ForeColor = branch switch
            {
                0 or 1 => Color.White,
                2 => ColorTranslator.FromHtml("#2adf00"),
                4 => ColorTranslator.FromHtml("#009cff"),
                5 => ColorTranslator.FromHtml("#eed040"),
                6 => ColorTranslator.FromHtml("#d200f8"),
                _ => Color.White,
            };
        }

        public static void SetVisibilityAndText(Label label, bool isVisible, string text = "", Color? textColor = null, string alternativeText = "", Color? alternativeTextColor = null)
        {
            label.Visible = isVisible;
            label.Text = isVisible ? text : alternativeText;
            label.ForeColor = isVisible ? textColor ?? label.ForeColor : alternativeTextColor ?? label.ForeColor;
        }

        public static void SetFixedBuffLabel(Label lbFixedBuff, Label label, int option, int optionValue, Label label2, int option2, int optionValue2)
        {
            bool isVisible = option > 0 || option2 > 0;

            lbFixedBuff.Visible = isVisible;

            if (option == 0)
            {
                label.Visible = false;
            }
            else
            {
                label.Visible = true;
                SetLabelValues(label, option, optionValue);
            }

            if (option2 == 0)
            {
                label2.Visible = false;
            }
            else
            {
                label2.Visible = true;
                SetLabelValues(label2, option2, optionValue2);
            }
        }

        public static void SetLabelValues(Label label, int option, int optionValue)
        {
            string fixedOption = GetOptionName(option);
            (int secTime, float value, int maxValue) = GetOptionValues(option);

            string colorHex = GetColorFromOption(fixedOption);
            string formattedText = FormatItemOption(fixedOption, $"{optionValue}", $"{secTime}", $"{value}", maxValue);

            label.Text = formattedText;
            label.ForeColor = ColorTranslator.FromHtml(colorHex);
        }

        public static void SetSocketColorLabel(Label label, int colorId)
        {
            switch ((SocketColor)colorId)
            {
                case SocketColor.None:
                    label.Text = "Unprocessed Gem Socket";
                    label.ForeColor = Color.White;
                    break;
                case SocketColor.Red:
                    label.Text = "Processed Red Gem Socket";
                    label.ForeColor = Color.Red;
                    break;
                case SocketColor.Blue:
                    label.Text = "Processed Blue Gem Socket";
                    label.ForeColor = Color.Blue;
                    break;
                case SocketColor.Yellow:
                    label.Text = "Processed Yellow Gem Socket";
                    label.ForeColor = Color.Yellow;
                    break;
                case SocketColor.Green:
                    label.Text = "Processed Green Gem Socket";
                    label.ForeColor = Color.Green;
                    break;
                case SocketColor.Colorless:
                    label.Text = "Processed Colorless Gem Socket";
                    label.ForeColor = Color.Gray;
                    break;
                case SocketColor.Gray:
                    label.Text = "Processed Gray Gem Socket";
                    label.ForeColor = Color.Gray;
                    break;
                default:
                    label.Text = "Unprocessed Gem Socket";
                    label.ForeColor = Color.White;
                    break;
            }

        }

        public static void SetSocketCountLabels(Label lbSocketCount, Label lbSockedBuff01, Label lbSockedBuff02, Label lbSockedBuff03, int minCount, int maxCount)
        {
            lbSocketCount.Visible = minCount > 0;

            lbSocketCount.Text = $"Socket: {maxCount}";

            SetVisibilityAndText(lbSockedBuff01, maxCount >= 1, "Unprocessed Gem Socket");
            SetVisibilityAndText(lbSockedBuff02, maxCount >= 2, "Unprocessed Gem Socket");
            SetVisibilityAndText(lbSockedBuff03, maxCount >= 3, "Unprocessed Gem Socket");

        }

        public static void SetRandomBuffLabels(Label lbRandomBuff, Label lbRandomBuff01, Label lbRandomBuff02, Label lbRandomBuff03, int minCount, int maxCount)
        {
            lbRandomBuff.Visible = minCount > 0;

            lbRandomBuff.Text = $"[Random Buff]";

            SetVisibilityAndText(lbRandomBuff01, maxCount >= 1, "No Buff");
            SetVisibilityAndText(lbRandomBuff02, maxCount >= 2, "No Buff");
            SetVisibilityAndText(lbRandomBuff03, maxCount >= 3, "No Buff");
        }

        private const string ColorTagStart = "<COLOR:";
        private const string ColorTagEnd = ">";
        private const string ColorTagClose = "</COLOR>";
        private const string LineBreakTag = "<br>";

        public static string FormatItemOption(string option, string replacement01, string replacement02, string replacement03, int maxValue)
        {
            option = RemoveColorTags(option);

            option = option.Replace(ColorTagClose, "")
                           .Replace(ColorTagStart, "")
                           .Replace(LineBreakTag, " ");

            string valuePlaceholder01 = option.Contains("#@value01@#%") ? "#@value01@#%" : "#@value01@#";
            string valuePlaceholder02 = option.Contains("#@value02@#") ? "#@value02@#" : "#@value02@#%";
            string valuePlaceholder03 = option.Contains("#@value03@#%") ? "#@value03@#%" : "#@value03@#";

            bool hasValuePlaceholder01 = option.Contains(valuePlaceholder01);
            bool hasValuePlaceholder02 = option.Contains(valuePlaceholder02);
            bool hasValuePlaceholder03 = option.Contains(valuePlaceholder03);

            if (hasValuePlaceholder01 && !hasValuePlaceholder02 && !hasValuePlaceholder03)
            {
                option = FormatPercentage(option, valuePlaceholder01, replacement01, maxValue);
            }
            else if (!hasValuePlaceholder01 && hasValuePlaceholder02 && !hasValuePlaceholder03)
            {
                option = FormatPercentage(option, valuePlaceholder02, replacement01, maxValue);
            }
            else if (hasValuePlaceholder01 && hasValuePlaceholder02 && !hasValuePlaceholder03)
            {
                if (option.Contains("chance to cast"))
                {
                    option = FormatPercentage(option, valuePlaceholder01, replacement01, maxValue);
                    option = FormatPercentage(option, valuePlaceholder02, replacement03, maxValue);
                }
                else if (option.Contains("damage will be converted"))
                {
                    //valuePlaceholder02 = option.Contains("#@value02@#%") ? "#@value02@#%" : "#@value02@#";
                    option = option.Replace(valuePlaceholder01, "Physical + Magic");
                    option = FormatPercentage(option, valuePlaceholder02, replacement01, maxValue);
                }
                else if (option.Contains("Recover +"))
                {
                    option = FormatPercentage(option, valuePlaceholder01, replacement01, maxValue);
                    if (int.TryParse(replacement02, out int seconds))
                    {
                        int minutes = seconds / 60;
                        replacement02 = minutes.ToString();
                    }

                    option = option.Replace(valuePlaceholder02, replacement02);
                }
                else if (option.Contains("chance of") || option.Contains("chance to"))
                {
                    option = FormatPercentage(option, valuePlaceholder01, replacement01, maxValue);
                    option = FormatPercentage(option, valuePlaceholder02, replacement03, maxValue);
                }
                else
                {
                    option = FormatPercentage(option, valuePlaceholder01, replacement02, maxValue);
                    option = FormatPercentage(option, valuePlaceholder02, replacement01, maxValue);
                }
            }
            else if (hasValuePlaceholder01 && hasValuePlaceholder02 && hasValuePlaceholder03)
            {
                if (option.Contains("chance of") || option.Contains("chance to"))
                {
                    option = FormatPercentage(option, valuePlaceholder01, replacement01, maxValue);
                    option = FormatPercentage(option, valuePlaceholder02, replacement02, maxValue);
                    option = FormatPercentage(option, valuePlaceholder03, replacement03, maxValue);
                }
                else
                {
                    option = FormatPercentage(option, valuePlaceholder01, replacement01, maxValue);
                    option = FormatPercentage(option, valuePlaceholder02, replacement02, maxValue);
                    option = FormatPercentage(option, valuePlaceholder03, replacement03, maxValue);
                }
            }
            else if (!hasValuePlaceholder01 && hasValuePlaceholder02 && hasValuePlaceholder03)
            {
                if (option.Contains("When hit"))
                {
                    option = FormatPercentage(option, valuePlaceholder02, replacement02, maxValue);
                    option = FormatPercentage(option, valuePlaceholder02, replacement01, maxValue);
                    option = FormatPercentage(option, valuePlaceholder03, replacement01, maxValue);

                }
                else
                {
                    option = FormatPercentage(option, valuePlaceholder03, replacement03, maxValue);
                    option = FormatPercentage(option, valuePlaceholder02, replacement02, maxValue);
                }
                
            }

            return option;
        }

        private static string RemoveColorTags(string input)
        {
            int startIndex = input.IndexOf(ColorTagStart);

            while (startIndex != -1)
            {
                int endIndex = input.IndexOf(ColorTagEnd, startIndex);

                if (endIndex != -1)
                {
                    input = input.Remove(startIndex, endIndex - startIndex + 1);
                }

                startIndex = input.IndexOf(ColorTagStart);
            }

            return input;
        }

        private static string FormatPercentage(string input, string placeholder, string replacement, int maxValue)
        {
            if (placeholder.Contains('%') && int.TryParse(replacement, out int numericValue))
            {
                double formattedValue = (double)numericValue / maxValue;
                input = input.Replace(placeholder, $"{formattedValue:P}");
            }
            else
            {
                input = input.Replace(placeholder, replacement);
            }

            return input;
        }

        public static string GetColorFromOption(string option)
        {
            int startIndex = option.IndexOf("<COLOR:");

            if (startIndex != -1)
            {
                int endIndex = option.IndexOf(">", startIndex);

                if (endIndex != -1)
                {
                    string colorHex = option.Substring(startIndex + 7, endIndex - startIndex - 7);
                    return "#" + colorHex;
                }
            }

            return "#ffffff";
        }

        public static (string categoryName, string subCategoryName) GetCategoryNames(int categoryID, int subCategoryID)
        {
            try
            {
                var connection = SQLiteDBConnection.OpenDatabaseConnection();
                if (connection == null)
                {
                    MessageBox.Show("gmdb.db not found.");
                    return (string.Empty, string.Empty);
                }

                string categoryName = DBQuery.GetCategoryName(categoryID, connection);
                string subCategoryName = DBQuery.GetSubCategoryName(subCategoryID, connection);

                return (categoryName, subCategoryName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving category names from the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (string.Empty, string.Empty);
            }
        }

        public static string GetSetName(int setId)
        {
            try
            {
                var connection = SQLiteDBConnection.OpenDatabaseConnection();
                if (connection == null)
                {
                    MessageBox.Show("gmdb.db not found.");
                    return (string.Empty);
                }

                return DBQuery.GetSetName(setId, connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving set name from the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        public static string GetOptionName(int optionID)
        {
            try
            {
                var connection = SQLiteDBConnection.OpenDatabaseConnection();
                if (connection == null)
                {
                    MessageBox.Show("gmdb.db not found.");
                    return (string.Empty);
                }

                return DBQuery.GetOptionName(optionID, connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving option values from the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (string.Empty);
            }
        }

        public static (int secTime, float value, int maxValue) GetOptionValues(int optionID)
        {
            try
            {
                var connection = SQLiteDBConnection.OpenDatabaseConnection();
                if (connection == null)
                {
                    MessageBox.Show("gmdb.db not found.");
                    return (0, 0, 0);
                }

                return DBQuery.GetOptionValues(optionID, connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving option values from the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (0, 0, 0);
            }
        }

        public static (int nPhysicalAttackMin, int nPhysicalAttackMax, int nMagicAttackMin, int nMagicAttackMax) GetWeaponStats(int jbClass, int WeaponID00)
        {
            try
            {
                var connection = SQLiteDBConnection.OpenDatabaseConnection();
                if (connection == null)
                {
                    MessageBox.Show("gmdb.db not found.");
                    return (0, 0, 0, 0);
                }

                return DBQuery.GetWeaponStats(jbClass, WeaponID00, connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving weapon stats from the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (0, 0, 0, 0);
            }
        }


    }
}
