using RHGMTool.Data;
using static RHGMTool.Helper.EnumMapper;

namespace RHGMTool
{
    public partial class GearFrame : Form
    {
        public GearFrame()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            lbItemName.Location = new Point(5, 4);
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
        public void SetItemData(ItemData gearItem)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => SetItemData(gearItem)));
                    return;
                }

                lbItemName.Text = gearItem.Name;

                FrameData.SetItemNameColor(lbItemName, gearItem.Branch);

                (string categoryName, string subCategoryName) = FrameData.GetCategoryNames(gearItem.Category, gearItem.SubCategory);

                lbCategory.Text = categoryName;
                lbSubCategory.Text = subCategoryName;

                UpdateWeight(gearItem.Weight);
                UpdateDurability(gearItem.Durability, gearItem.Durability);

                FrameData.SetVisibilityAndText(lbPsysicalDefence, gearItem.Type != "Weapon" && gearItem.Defense > 0, $"Physical Defense +{gearItem.Defense}");
                FrameData.SetVisibilityAndText(lbMagicDefence, gearItem.Type != "Weapon" && gearItem.MagicDefense > 0, $"Magic Defence +{gearItem.MagicDefense}");

                (int PhysicalAttackMin, int PhysicalAttackMax, int MagicAttackMin, int MagicAttackMax) = FrameData.GetWeaponStats(gearItem.JobClass, gearItem.WeaponID00);

                FrameData.SetVisibilityAndText(lbPsysicalDamage, gearItem.Type != "Armor" && gearItem.WeaponID00 != 0, $"Physical Damage +{PhysicalAttackMin}~{PhysicalAttackMax}");
                FrameData.SetVisibilityAndText(lbMagicDamage, gearItem.Type != "Armor" && gearItem.WeaponID00 != 0, $"Magic Damage +{MagicAttackMin}~{MagicAttackMax}");

                FrameData.SetVisibilityAndText(lbValue, gearItem.SellPrice > 0, $"{gearItem.SellPrice:N0} Gold");

                lbReqLevel.Text = $"Required Level: {gearItem.LevelLimit}";

                FrameData.SetVisibilityAndText(lbItemTrade, gearItem.ItemTrade == 0, "Trade Unavailable", ColorTranslator.FromHtml("#e75151"));

                UpdateAttribute(gearItem.ReconstructionMax, gearItem.ReconstructionMax);

                if (gearItem.PetFood == 0)
                {
                    lbPetFood.ForeColor = ColorTranslator.FromHtml("#e75151");
                    lbPetFood.Text = "This item cannot be used as Pet Food";
                }
                else
                {
                    lbPetFood.ForeColor = ColorTranslator.FromHtml("#eed040");
                    lbPetFood.Text = "This item can be used as Pet Food";
                }

                FrameData.SetVisibilityAndText(lbJobClass, gearItem.JobClass > 0, GetJobClassName(gearItem.JobClass));

                string setName = FrameData.GetSetName(gearItem.SetId);
                lbSetName.Visible = gearItem.SetId != 0;
                lbSetName.Text = setName;

                FrameData.SetFixedBuffLabel(lbFixedBuff, lbFixedBuff01, gearItem.FixOption00, gearItem.FixOptionValue00, lbFixedBuff02, gearItem.FixOption01, gearItem.FixOptionValue01);
                FrameData.SetSocketCountLabels(lbSocketCount, lbSockedBuff01, lbSockedBuff02, lbSockedBuff03, gearItem.SocketCountMin, gearItem.SocketCountMax);
                FrameData.SetRandomBuffLabels(lbRandomBuff, lbRandomBuff01, lbRandomBuff02, lbRandomBuff03, gearItem.OptionCountMin, gearItem.OptionCountMax);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating item data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateItemName(int enchantLevel)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateItemName(enchantLevel)));
                return;
            }

            if (enchantLevel > 0 && enchantLevel <= 9)
            {
                lbEnchant.Visible = true;
                lbEnchant.Size = new Size(30, 21);
                lbItemName.Location = new Point(30, 4);

                lbEnchant.Text = $"+{enchantLevel}";
                lbEnchant.ForeColor = ColorTranslator.FromHtml("#06EBE8");
            }
            else if (enchantLevel > 0 && enchantLevel > 9)
            {
                lbEnchant.Visible = true;
                lbEnchant.Size = new Size(36, 21);
                lbItemName.Location = new Point(36, 4);

                lbEnchant.Text = $"+{enchantLevel}";
                lbEnchant.ForeColor = ColorTranslator.FromHtml("#06EBE8");
            }
            else
            {
                lbEnchant.Visible = false;
                lbItemName.Location = new Point(5, 4);
            }
        }

        public void UpdateDurability(int durability, int maxDurability)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateDurability(durability, maxDurability)));
                return;
            }

            FrameData.SetVisibilityAndText(lbDurability, durability > 0, $"Durability: {durability / 100}/{maxDurability / 100}");
        }

        public void UpdateWeight(int weight)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateWeight(weight)));
                return;
            }

            lbWeight.Text = $"{weight / 1000.0:0.000}Kg";
        }

        public void UpdateAttribute(int attribute, int maxAttribute)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateAttribute(attribute, maxAttribute)));
                return;
            }

            if (maxAttribute == 0)
            {
                lbAttribute.Visible = false;
            }
            else
            {
                lbAttribute.Visible = true;
                lbAttribute.Text = $"Attribute Item ({attribute} Times/{maxAttribute} Times)";
            }
        }

        public void UpdateRankText(int rank)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateRankText(rank)));
                return;
            }

            string rankText = rank switch
            {
                5 => "1st",
                4 => "2nd",
                3 => "3rd",
                2 => "4th",
                1 => "5th",
                _ => rank + "th",
            };
            lbRank.Text = $"Rank: {rankText} Rank";
        }

        public void SetBuffLabel(string labelName, int option, int optionValue)
        {
            if (Controls.Find(labelName, true).FirstOrDefault() is Label label)
            {
                if (option == 0)
                {
                    label.Text = "No Buff";
                    label.ForeColor = Color.White;
                }
                else
                {
                    FrameData.SetLabelValues(label, option, optionValue);
                }
            }
        }

        public void SetSocketColor(string labelName, int colorId)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetSocketColor(labelName, colorId)));
                return;
            }

            if (Controls.Find(labelName, true).FirstOrDefault() is Label label)
            {
                FrameData.SetSocketColorLabel(label, colorId);
            }
        }

        public void UpdateSocketCountLabel(int socketCount)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateSocketCountLabel(socketCount)));
                return;
            }

            lbSocketCount.Text = $"Socket: {socketCount}";

            lbSockedBuff01.Visible = socketCount > 0;
            lbSockedBuff02.Visible = socketCount >= 2;
            lbSockedBuff03.Visible = socketCount >= 3;

        }

        private static void AdjustLabelPosition(Control sourceControl, Control targetControl)
        {
            int newYPosition = sourceControl.Location.Y + sourceControl.Height;

            targetControl.Location = new Point(targetControl.Location.X, newYPosition);
        }

        private void LbFixedBuff01_SizeChanged(object sender, EventArgs e)
        {
            AdjustLabelPosition(lbFixedBuff01, lbFixedBuff02);
        }

        private void LbRandomBuff01_SizeChanged(object sender, EventArgs e)
        {
            AdjustLabelPosition(lbRandomBuff01, lbRandomBuff02);
            AdjustLabelPosition(lbRandomBuff02, lbRandomBuff03);
        }

        private void LbRandomBuff02_SizeChanged(object sender, EventArgs e)
        {
            AdjustLabelPosition(lbRandomBuff02, lbRandomBuff03);
        }

        private void LbSockedBuff01_SizeChanged(object sender, EventArgs e)
        {
            AdjustLabelPosition(lbSockedBuff01, lbSockedBuff02);
            AdjustLabelPosition(lbSockedBuff02, lbSockedBuff03);
        }

        private void LbSockedBuff02_SizeChanged(object sender, EventArgs e)
        {
            AdjustLabelPosition(lbSockedBuff02, lbSockedBuff03);
        }
    }
}
