using RHGMTool.Data;
using RHGMTool.Helper;

namespace RHGMTool
{
    public partial class ItemFrame : Form
    {
        public ItemFrame()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            rtbDescription.BackColor = ColorTranslator.FromHtml("#0e0906");

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        public void SetItemData(ItemData item)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetItemData(item)));
                return;
            }

            lbItemName.Text = item.Name;

            FrameData.SetItemNameColor(lbItemName, item.Branch);

            (string categoryName, string subCategoryName) = FrameData.GetCategoryNames(item.Category, item.SubCategory);

            lbCategory.Text = categoryName;
            lbSubCategory.Text = subCategoryName;
            FrameData.SetVisibilityAndText(lbJobClass, item.JobClass > 0, EnumMapper.GetJobClassName(item.JobClass));

            FrameData.SetVisibilityAndText(lbWeight, item.Weight > 0, $"{(item.Weight / 1000.0):0.000}Kg");

            FrameData.SetVisibilityAndText(lbValue, item.SellPrice > 0, $"{item.SellPrice:N0} Gold");

            lbReqLevel.Text = $"Required Level: {item.LevelLimit}";

            FrameData.SetVisibilityAndText(lbItemTrade, item.ItemTrade == 0, "Trade Unavailable", ColorTranslator.FromHtml("#e75151"));

            if (item.PetFood == 0)
            {
                lbPetFood.ForeColor = ColorTranslator.FromHtml("#e75151");
                lbPetFood.Text = "This item cannot be used as Pet Food";
            }
            else
            {
                lbPetFood.ForeColor = ColorTranslator.FromHtml("#eed040");
                lbPetFood.Text = "This item can be used as Pet Food";
            }

            FormatRichTextBoxDescription(rtbDescription, item.Description ?? string.Empty);
        }

        private static void FormatRichTextBoxDescription(RichTextBox richTextBox, string description)
        {
            if (!richTextBox.IsHandleCreated)
            {
                richTextBox.HandleCreated += (sender, e) => FormatRichTextBoxDescription(richTextBox, description);
                return;
            }

            richTextBox.BeginInvoke(new Action(() =>
            {
                richTextBox.Clear(); // Clear existing text

                // Split the description into parts based on line breaks ("<br>")
                string[] parts = description.Split(new string[] { "<BR>", "<br>", "<Br>" }, StringSplitOptions.None);

                foreach (string part in parts)
                {
                    // Check if the part contains a color tag
                    if (part.StartsWith("<COLOR:"))
                    {
                        // Extract the color value from the tag, e.g., "<COLOR:06EBE8>"
                        int tagEnd = part.IndexOf(">");
                        if (tagEnd != -1)
                        {
                            string colorTag = part[7..tagEnd];
                            Color customColor = ColorTranslator.FromHtml("#" + colorTag.ToLower()); // Convert to lowercase

                            // Append text with the custom color
                            FormatText(richTextBox, part[(tagEnd + 1)..], customColor);
                        }
                    }
                    else
                    {
                        // No color tag, append the part with default color
                        FormatText(richTextBox, part);
                    }
                }
            }));
        }

        private static void FormatText(RichTextBox richTextBox, string text, Color? color = null)
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionLength = 0;

            richTextBox.SelectionColor = color ?? richTextBox.ForeColor;

            // Remove both opening and closing color tags if present
            text = text.Replace("<COLOR>", "").Replace("</COLOR>", "");

            richTextBox.AppendText(text);
            richTextBox.AppendText(Environment.NewLine); // Add a new line after each part
            richTextBox.SelectionColor = richTextBox.ForeColor; // reset color to default
        }

        

    }
}
