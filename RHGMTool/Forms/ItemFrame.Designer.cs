namespace RHGMTool
{
    partial class ItemFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbItemName = new Label();
            lbCategory = new Label();
            lbSubCategory = new Label();
            lbReqLevel = new Label();
            lbWeight = new Label();
            rtbDescription = new RichTextBox();
            lbValue = new Label();
            lbPetFood = new Label();
            lbItemTrade = new Label();
            lbJobClass = new Label();
            SuspendLayout();
            // 
            // lbItemName
            // 
            lbItemName.BackColor = Color.Transparent;
            lbItemName.Font = new Font("Arial Unicode MS", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lbItemName.ForeColor = Color.White;
            lbItemName.Location = new Point(3, 4);
            lbItemName.Name = "lbItemName";
            lbItemName.Size = new Size(289, 16);
            lbItemName.TabIndex = 0;
            lbItemName.Text = "Name\r\n";
            // 
            // lbCategory
            // 
            lbCategory.AutoSize = true;
            lbCategory.BackColor = Color.Transparent;
            lbCategory.Font = new Font("Arial Unicode MS", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lbCategory.ForeColor = Color.White;
            lbCategory.Location = new Point(5, 25);
            lbCategory.Name = "lbCategory";
            lbCategory.Size = new Size(67, 19);
            lbCategory.TabIndex = 1;
            lbCategory.Text = "Category";
            // 
            // lbSubCategory
            // 
            lbSubCategory.Anchor = AnchorStyles.Right;
            lbSubCategory.BackColor = Color.Transparent;
            lbSubCategory.Font = new Font("Arial Unicode MS", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lbSubCategory.ForeColor = Color.White;
            lbSubCategory.Location = new Point(140, 25);
            lbSubCategory.Name = "lbSubCategory";
            lbSubCategory.Size = new Size(152, 23);
            lbSubCategory.TabIndex = 2;
            lbSubCategory.Text = "Category 2";
            lbSubCategory.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lbReqLevel
            // 
            lbReqLevel.AutoSize = true;
            lbReqLevel.BackColor = Color.Transparent;
            lbReqLevel.Font = new Font("Arial Unicode MS", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            lbReqLevel.ForeColor = Color.DarkGray;
            lbReqLevel.Location = new Point(4, 43);
            lbReqLevel.Name = "lbReqLevel";
            lbReqLevel.Size = new Size(121, 19);
            lbReqLevel.TabIndex = 3;
            lbReqLevel.Text = "Required Level: 1";
            // 
            // lbWeight
            // 
            lbWeight.Anchor = AnchorStyles.Right;
            lbWeight.BackColor = Color.Transparent;
            lbWeight.Font = new Font("Arial Unicode MS", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lbWeight.ForeColor = Color.White;
            lbWeight.Location = new Point(196, 65);
            lbWeight.Name = "lbWeight";
            lbWeight.RightToLeft = RightToLeft.No;
            lbWeight.Size = new Size(96, 16);
            lbWeight.TabIndex = 4;
            lbWeight.Text = "0,001Kg";
            lbWeight.TextAlign = ContentAlignment.MiddleRight;
            // 
            // rtbDescription
            // 
            rtbDescription.BackColor = SystemColors.WindowText;
            rtbDescription.BorderStyle = BorderStyle.None;
            rtbDescription.Font = new Font("Arial Unicode MS", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            rtbDescription.ForeColor = Color.White;
            rtbDescription.Location = new Point(12, 102);
            rtbDescription.Name = "rtbDescription";
            rtbDescription.ReadOnly = true;
            rtbDescription.ScrollBars = RichTextBoxScrollBars.None;
            rtbDescription.Size = new Size(276, 171);
            rtbDescription.TabIndex = 7;
            rtbDescription.TabStop = false;
            rtbDescription.Text = "";
            // 
            // lbValue
            // 
            lbValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbValue.BackColor = Color.Transparent;
            lbValue.Font = new Font("Arial Unicode MS", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lbValue.ForeColor = Color.White;
            lbValue.Location = new Point(185, 82);
            lbValue.Name = "lbValue";
            lbValue.Size = new Size(107, 16);
            lbValue.TabIndex = 8;
            lbValue.Text = "10000 Gold";
            lbValue.TextAlign = ContentAlignment.TopRight;
            // 
            // lbPetFood
            // 
            lbPetFood.AutoSize = true;
            lbPetFood.BackColor = Color.Transparent;
            lbPetFood.Font = new Font("Arial Unicode MS", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lbPetFood.ForeColor = Color.Firebrick;
            lbPetFood.Location = new Point(4, 276);
            lbPetFood.Name = "lbPetFood";
            lbPetFood.Size = new Size(249, 19);
            lbPetFood.TabIndex = 9;
            lbPetFood.Text = "This item cannot be used as Pet Food";
            // 
            // lbItemTrade
            // 
            lbItemTrade.AutoSize = true;
            lbItemTrade.BackColor = Color.Transparent;
            lbItemTrade.Font = new Font("Arial Unicode MS", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lbItemTrade.ForeColor = Color.White;
            lbItemTrade.Location = new Point(5, 66);
            lbItemTrade.Name = "lbItemTrade";
            lbItemTrade.Size = new Size(125, 19);
            lbItemTrade.TabIndex = 10;
            lbItemTrade.Text = "Trade Unavailable";
            // 
            // lbJobClass
            // 
            lbJobClass.Anchor = AnchorStyles.Right;
            lbJobClass.BackColor = Color.Transparent;
            lbJobClass.Font = new Font("Arial Unicode MS", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lbJobClass.ForeColor = Color.DarkGray;
            lbJobClass.Location = new Point(155, 45);
            lbJobClass.Name = "lbJobClass";
            lbJobClass.Size = new Size(133, 23);
            lbJobClass.TabIndex = 13;
            lbJobClass.Text = "Frantz";
            lbJobClass.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ItemFrame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.ItemFrame;
            ClientSize = new Size(300, 300);
            ControlBox = false;
            Controls.Add(lbJobClass);
            Controls.Add(lbItemTrade);
            Controls.Add(lbPetFood);
            Controls.Add(lbValue);
            Controls.Add(rtbDescription);
            Controls.Add(lbWeight);
            Controls.Add(lbReqLevel);
            Controls.Add(lbSubCategory);
            Controls.Add(lbCategory);
            Controls.Add(lbItemName);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(385, 38);
            Name = "ItemFrame";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "ItemFrame";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lbReqLevel;
        private RichTextBox rtbDescription;
        private Label lbValue;
        private Label lbPetFood;
        private Label lbItemTrade;
        private Label lbJobClass;
        public Label lbItemName;
        public Label lbWeight;
        private Label lbCategory;
        private Label lbSubCategory;
    }
}