namespace RHGMTool
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            pbMail = new PictureBox();
            GroupBoxSQL = new GroupBox();
            btnSQLConnection = new Button();
            imageListButton = new ImageList(components);
            pictureBoxEyeSqlPassword = new PictureBox();
            tbSQLPassword = new TextBox();
            tbSQLAddress = new TextBox();
            tbSQLAccount = new TextBox();
            lbSQLPassword = new Label();
            lbSQLAccount = new Label();
            LbSQLAddress = new Label();
            gbTools = new GroupBox();
            btnItemDatabase = new Button();
            pbItemDB = new PictureBox();
            btnSendMail = new Button();
            lbVersion = new Label();
            toolTip = new ToolTip(components);
            imageListClose = new ImageList(components);
            btnClose = new Button();
            lbTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)pbMail).BeginInit();
            GroupBoxSQL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEyeSqlPassword).BeginInit();
            gbTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbItemDB).BeginInit();
            SuspendLayout();
            // 
            // pbMail
            // 
            pbMail.BackColor = Color.Transparent;
            pbMail.Image = Properties.Resources.icon_mail;
            pbMail.Location = new Point(10, 27);
            pbMail.Name = "pbMail";
            pbMail.Size = new Size(36, 36);
            pbMail.SizeMode = PictureBoxSizeMode.AutoSize;
            pbMail.TabIndex = 42;
            pbMail.TabStop = false;
            // 
            // GroupBoxSQL
            // 
            GroupBoxSQL.BackColor = Color.Transparent;
            GroupBoxSQL.Controls.Add(btnSQLConnection);
            GroupBoxSQL.Controls.Add(pictureBoxEyeSqlPassword);
            GroupBoxSQL.Controls.Add(tbSQLPassword);
            GroupBoxSQL.Controls.Add(tbSQLAddress);
            GroupBoxSQL.Controls.Add(tbSQLAccount);
            GroupBoxSQL.Controls.Add(lbSQLPassword);
            GroupBoxSQL.Controls.Add(lbSQLAccount);
            GroupBoxSQL.Controls.Add(LbSQLAddress);
            GroupBoxSQL.ForeColor = Color.White;
            GroupBoxSQL.Location = new Point(14, 44);
            GroupBoxSQL.Name = "GroupBoxSQL";
            GroupBoxSQL.Size = new Size(472, 193);
            GroupBoxSQL.TabIndex = 40;
            GroupBoxSQL.TabStop = false;
            GroupBoxSQL.Text = "SQL Settings";
            // 
            // btnSQLConnection
            // 
            btnSQLConnection.BackColor = Color.Transparent;
            btnSQLConnection.FlatAppearance.BorderColor = Color.Black;
            btnSQLConnection.FlatAppearance.BorderSize = 0;
            btnSQLConnection.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnSQLConnection.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSQLConnection.FlatStyle = FlatStyle.Flat;
            btnSQLConnection.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnSQLConnection.ForeColor = Color.Transparent;
            btnSQLConnection.ImageIndex = 0;
            btnSQLConnection.ImageList = imageListButton;
            btnSQLConnection.ImeMode = ImeMode.NoControl;
            btnSQLConnection.Location = new Point(175, 143);
            btnSQLConnection.Name = "btnSQLConnection";
            btnSQLConnection.Size = new Size(120, 26);
            btnSQLConnection.TabIndex = 90;
            btnSQLConnection.Text = "Test Connection";
            btnSQLConnection.UseVisualStyleBackColor = false;
            btnSQLConnection.Click += BtnSQLConnection_Click;
            btnSQLConnection.MouseDown += Button_MouseDown;
            btnSQLConnection.MouseLeave += Button_MouseLeave;
            btnSQLConnection.MouseHover += Button_MouseHover;
            // 
            // imageListButton
            // 
            imageListButton.ColorDepth = ColorDepth.Depth32Bit;
            imageListButton.ImageStream = (ImageListStreamer)resources.GetObject("imageListButton.ImageStream");
            imageListButton.TransparentColor = Color.Transparent;
            imageListButton.Images.SetKeyName(0, "Button_normal.png");
            imageListButton.Images.SetKeyName(1, "Button_down.png");
            imageListButton.Images.SetKeyName(2, "Button_up.png");
            imageListButton.Images.SetKeyName(3, "Button_d.png");
            // 
            // pictureBoxEyeSqlPassword
            // 
            pictureBoxEyeSqlPassword.BackColor = Color.Transparent;
            pictureBoxEyeSqlPassword.Image = Properties.Resources.icons8_eye_16;
            pictureBoxEyeSqlPassword.Location = new Point(277, 93);
            pictureBoxEyeSqlPassword.Name = "pictureBoxEyeSqlPassword";
            pictureBoxEyeSqlPassword.Size = new Size(16, 16);
            pictureBoxEyeSqlPassword.TabIndex = 55;
            pictureBoxEyeSqlPassword.TabStop = false;
            pictureBoxEyeSqlPassword.MouseDown += PictureBoxEyeSqlPassword_MouseDown;
            pictureBoxEyeSqlPassword.MouseUp += PictureBoxEyeSqlPassword_MouseUp;
            // 
            // tbSQLPassword
            // 
            tbSQLPassword.BackColor = Color.Black;
            tbSQLPassword.BorderStyle = BorderStyle.None;
            tbSQLPassword.ForeColor = Color.White;
            tbSQLPassword.Location = new Point(84, 93);
            tbSQLPassword.Margin = new Padding(4, 3, 4, 3);
            tbSQLPassword.Name = "tbSQLPassword";
            tbSQLPassword.Size = new Size(211, 16);
            tbSQLPassword.TabIndex = 87;
            tbSQLPassword.UseSystemPasswordChar = true;
            // 
            // tbSQLAddress
            // 
            tbSQLAddress.BackColor = Color.Black;
            tbSQLAddress.BorderStyle = BorderStyle.None;
            tbSQLAddress.ForeColor = Color.White;
            tbSQLAddress.Location = new Point(83, 25);
            tbSQLAddress.Margin = new Padding(4, 3, 4, 3);
            tbSQLAddress.Name = "tbSQLAddress";
            tbSQLAddress.Size = new Size(212, 16);
            tbSQLAddress.TabIndex = 85;
            tbSQLAddress.Text = "127.0.0.1";
            // 
            // tbSQLAccount
            // 
            tbSQLAccount.BackColor = Color.Black;
            tbSQLAccount.BorderStyle = BorderStyle.None;
            tbSQLAccount.ForeColor = Color.White;
            tbSQLAccount.Location = new Point(84, 60);
            tbSQLAccount.Margin = new Padding(4, 3, 4, 3);
            tbSQLAccount.Name = "tbSQLAccount";
            tbSQLAccount.Size = new Size(211, 16);
            tbSQLAccount.TabIndex = 86;
            tbSQLAccount.Text = "sa";
            // 
            // lbSQLPassword
            // 
            lbSQLPassword.AutoSize = true;
            lbSQLPassword.BackColor = Color.Transparent;
            lbSQLPassword.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            lbSQLPassword.ForeColor = Color.White;
            lbSQLPassword.Location = new Point(4, 93);
            lbSQLPassword.Name = "lbSQLPassword";
            lbSQLPassword.Size = new Size(78, 13);
            lbSQLPassword.TabIndex = 7;
            lbSQLPassword.Text = "SQL Password";
            // 
            // lbSQLAccount
            // 
            lbSQLAccount.AutoSize = true;
            lbSQLAccount.BackColor = Color.Transparent;
            lbSQLAccount.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            lbSQLAccount.ForeColor = Color.White;
            lbSQLAccount.Location = new Point(5, 61);
            lbSQLAccount.Name = "lbSQLAccount";
            lbSQLAccount.Size = new Size(71, 13);
            lbSQLAccount.TabIndex = 6;
            lbSQLAccount.Text = "SQL Account";
            // 
            // LbSQLAddress
            // 
            LbSQLAddress.AutoSize = true;
            LbSQLAddress.BackColor = Color.Transparent;
            LbSQLAddress.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            LbSQLAddress.ForeColor = Color.White;
            LbSQLAddress.Location = new Point(5, 27);
            LbSQLAddress.Name = "LbSQLAddress";
            LbSQLAddress.Size = new Size(70, 13);
            LbSQLAddress.TabIndex = 5;
            LbSQLAddress.Text = "SQL Address";
            // 
            // gbTools
            // 
            gbTools.BackColor = Color.Transparent;
            gbTools.Controls.Add(btnItemDatabase);
            gbTools.Controls.Add(pbItemDB);
            gbTools.Controls.Add(btnSendMail);
            gbTools.Controls.Add(pbMail);
            gbTools.ForeColor = Color.White;
            gbTools.Location = new Point(14, 254);
            gbTools.Name = "gbTools";
            gbTools.Size = new Size(472, 309);
            gbTools.TabIndex = 41;
            gbTools.TabStop = false;
            gbTools.Text = "Tools";
            // 
            // btnItemDatabase
            // 
            btnItemDatabase.BackColor = Color.Transparent;
            btnItemDatabase.FlatAppearance.BorderColor = Color.Black;
            btnItemDatabase.FlatAppearance.BorderSize = 0;
            btnItemDatabase.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnItemDatabase.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnItemDatabase.FlatStyle = FlatStyle.Flat;
            btnItemDatabase.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnItemDatabase.ForeColor = Color.Transparent;
            btnItemDatabase.ImageIndex = 0;
            btnItemDatabase.ImageList = imageListButton;
            btnItemDatabase.ImeMode = ImeMode.NoControl;
            btnItemDatabase.Location = new Point(324, 32);
            btnItemDatabase.Name = "btnItemDatabase";
            btnItemDatabase.Size = new Size(120, 26);
            btnItemDatabase.TabIndex = 91;
            btnItemDatabase.Text = "Item Database";
            btnItemDatabase.UseVisualStyleBackColor = false;
            btnItemDatabase.Click += BtnItemDatabase_Click;
            btnItemDatabase.MouseDown += Button_MouseDown;
            btnItemDatabase.MouseLeave += Button_MouseLeave;
            btnItemDatabase.MouseHover += Button_MouseHover;
            // 
            // pbItemDB
            // 
            pbItemDB.BackColor = Color.Transparent;
            pbItemDB.Image = Properties.Resources.icon_cash_optionchange;
            pbItemDB.Location = new Point(282, 27);
            pbItemDB.Name = "pbItemDB";
            pbItemDB.Size = new Size(36, 36);
            pbItemDB.SizeMode = PictureBoxSizeMode.CenterImage;
            pbItemDB.TabIndex = 90;
            pbItemDB.TabStop = false;
            // 
            // btnSendMail
            // 
            btnSendMail.BackColor = Color.Transparent;
            btnSendMail.FlatAppearance.BorderColor = Color.Black;
            btnSendMail.FlatAppearance.BorderSize = 0;
            btnSendMail.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnSendMail.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSendMail.FlatStyle = FlatStyle.Flat;
            btnSendMail.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnSendMail.ForeColor = Color.Transparent;
            btnSendMail.ImageIndex = 0;
            btnSendMail.ImageList = imageListButton;
            btnSendMail.ImeMode = ImeMode.NoControl;
            btnSendMail.Location = new Point(52, 32);
            btnSendMail.Name = "btnSendMail";
            btnSendMail.Size = new Size(120, 26);
            btnSendMail.TabIndex = 89;
            btnSendMail.Text = "Send Mail";
            btnSendMail.UseVisualStyleBackColor = false;
            btnSendMail.Click += BtnSendMail_Click;
            btnSendMail.MouseDown += Button_MouseDown;
            btnSendMail.MouseLeave += Button_MouseLeave;
            btnSendMail.MouseHover += Button_MouseHover;
            // 
            // lbVersion
            // 
            lbVersion.AutoSize = true;
            lbVersion.BackColor = Color.Transparent;
            lbVersion.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lbVersion.ForeColor = Color.White;
            lbVersion.Location = new Point(406, 566);
            lbVersion.Name = "lbVersion";
            lbVersion.Size = new Size(57, 19);
            lbVersion.TabIndex = 42;
            lbVersion.Text = "Version:";
            toolTip.SetToolTip(lbVersion, "Click to open github repository.");
            lbVersion.Click += VersionLabel_Click;
            // 
            // imageListClose
            // 
            imageListClose.ColorDepth = ColorDepth.Depth32Bit;
            imageListClose.ImageStream = (ImageListStreamer)resources.GetObject("imageListClose.ImageStream");
            imageListClose.TransparentColor = Color.Transparent;
            imageListClose.Images.SetKeyName(0, "lb_close_button05.png");
            imageListClose.Images.SetKeyName(1, "lb_close_button06.png");
            imageListClose.Images.SetKeyName(2, "lb_close_button07.png");
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Transparent;
            btnClose.FlatAppearance.BorderColor = Color.Black;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnClose.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.ForeColor = Color.Transparent;
            btnClose.ImageIndex = 0;
            btnClose.ImageList = imageListClose;
            btnClose.ImeMode = ImeMode.NoControl;
            btnClose.Location = new Point(476, 9);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(25, 26);
            btnClose.TabIndex = 84;
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += BtnClose_Click;
            btnClose.MouseDown += BtnClose_MouseDown;
            btnClose.MouseLeave += BtnClose_MouseLeave;
            btnClose.MouseHover += BtnClose_MouseHover;
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.BackColor = Color.Transparent;
            lbTitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lbTitle.ForeColor = Color.White;
            lbTitle.Location = new Point(179, 13);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(143, 19);
            lbTitle.TabIndex = 85;
            lbTitle.Text = "Rusty Hearts GM Tool";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Magenta;
            BackgroundImage = Properties.Resources.MainForm;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(510, 600);
            Controls.Add(lbTitle);
            Controls.Add(GroupBoxSQL);
            Controls.Add(btnClose);
            Controls.Add(lbVersion);
            Controls.Add(gbTools);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rusty Hearts GM Tool";
            TransparencyKey = Color.Magenta;
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            MouseDown += MainForm_MouseDown;
            ((System.ComponentModel.ISupportInitialize)pbMail).EndInit();
            GroupBoxSQL.ResumeLayout(false);
            GroupBoxSQL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEyeSqlPassword).EndInit();
            gbTools.ResumeLayout(false);
            gbTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbItemDB).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox GroupBoxSQL;
        private PictureBox pictureBoxEyeSqlPassword;
        private Label lbSQLPassword;
        private Label lbSQLAccount;
        private Label LbSQLAddress;
        private GroupBox gbTools;
        private PictureBox pbMail;
        private Label lbVersion;
        private ToolTip toolTip;
        private ImageList imageListButton;
        private ImageList imageListClose;
        private Button btnSendMail;
        private Button btnClose;
        private TextBox tbSQLPassword;
        private TextBox tbSQLAddress;
        private TextBox tbSQLAccount;
        private Label lbTitle;
        private Button btnSQLConnection;
        private Button btnItemDatabase;
        private PictureBox pbItemDB;
    }
}