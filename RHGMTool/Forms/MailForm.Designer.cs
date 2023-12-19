namespace RHGMTool
{
    partial class MailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailForm));
            pbItem01Branch = new PictureBox();
            pbEmpty3 = new PictureBox();
            pbEmpty2 = new PictureBox();
            pbEmpty1 = new PictureBox();
            numReturnDay = new NumericUpDown();
            lbReturn = new Label();
            lbRGold = new Label();
            numReqGold = new NumericUpDown();
            chkSendToAll = new CheckBox();
            pbGold = new PictureBox();
            txtMailContent = new TextBox();
            numMailGold = new NumericUpDown();
            tbSender = new TextBox();
            lbGold = new Label();
            txtRecipient = new TextBox();
            lbRecipient = new Label();
            toolTip = new ToolTip(components);
            btnLoadTemplate = new Button();
            imageListButton = new ImageList(components);
            btnClear = new Button();
            btnSaveTemplate = new Button();
            pbGold2 = new PictureBox();
            lbSender = new Label();
            lbAttachItem = new Label();
            pbItem02Branch = new PictureBox();
            pbItem03Branch = new PictureBox();
            pbItemHelp = new PictureBox();
            pbIcon01 = new PictureBox();
            pbIcon02 = new PictureBox();
            pbIcon03 = new PictureBox();
            lbTitle = new Label();
            imageListClose = new ImageList(components);
            btnClose = new Button();
            btnSend = new Button();
            panelMessage = new Panel();
            imageListBranch = new ImageList(components);
            lbItemAmount3 = new Label();
            lbItemAmount2 = new Label();
            lbItemAmount1 = new Label();
            panelSlots = new Panel();
            ((System.ComponentModel.ISupportInitialize)pbItem01Branch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbEmpty3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbEmpty2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbEmpty1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numReturnDay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numReqGold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbGold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMailGold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbGold2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbItem02Branch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbItem03Branch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbItemHelp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbIcon01).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbIcon02).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbIcon03).BeginInit();
            panelMessage.SuspendLayout();
            panelSlots.SuspendLayout();
            SuspendLayout();
            // 
            // pbItem01Branch
            // 
            pbItem01Branch.BackColor = Color.Transparent;
            pbItem01Branch.Image = Properties.Resources.lb_icon_slot_04epic;
            pbItem01Branch.Location = new Point(9, 7);
            pbItem01Branch.Name = "pbItem01Branch";
            pbItem01Branch.Size = new Size(42, 42);
            pbItem01Branch.SizeMode = PictureBoxSizeMode.AutoSize;
            pbItem01Branch.TabIndex = 75;
            pbItem01Branch.TabStop = false;
            pbItem01Branch.Tag = "0";
            pbItem01Branch.Visible = false;
            pbItem01Branch.MouseClick += PbItemAttach_MouseDown;
            // 
            // pbEmpty3
            // 
            pbEmpty3.BackColor = Color.Black;
            pbEmpty3.Image = Properties.Resources.lb_ac_icon;
            pbEmpty3.Location = new Point(112, 6);
            pbEmpty3.Name = "pbEmpty3";
            pbEmpty3.Size = new Size(44, 44);
            pbEmpty3.SizeMode = PictureBoxSizeMode.Zoom;
            pbEmpty3.TabIndex = 74;
            pbEmpty3.TabStop = false;
            pbEmpty3.Tag = "2";
            toolTip.SetToolTip(pbEmpty3, "Click to attach a item");
            pbEmpty3.MouseDown += PbItemAttach_MouseDown;
            // 
            // pbEmpty2
            // 
            pbEmpty2.BackColor = Color.Black;
            pbEmpty2.Image = Properties.Resources.lb_ac_icon;
            pbEmpty2.Location = new Point(60, 6);
            pbEmpty2.Name = "pbEmpty2";
            pbEmpty2.Size = new Size(44, 44);
            pbEmpty2.SizeMode = PictureBoxSizeMode.Zoom;
            pbEmpty2.TabIndex = 73;
            pbEmpty2.TabStop = false;
            pbEmpty2.Tag = "1";
            toolTip.SetToolTip(pbEmpty2, "Click to attach a item");
            pbEmpty2.MouseDown += PbItemAttach_MouseDown;
            // 
            // pbEmpty1
            // 
            pbEmpty1.BackColor = Color.Black;
            pbEmpty1.Image = Properties.Resources.lb_ac_icon;
            pbEmpty1.Location = new Point(8, 6);
            pbEmpty1.Name = "pbEmpty1";
            pbEmpty1.Size = new Size(44, 44);
            pbEmpty1.SizeMode = PictureBoxSizeMode.Zoom;
            pbEmpty1.TabIndex = 72;
            pbEmpty1.TabStop = false;
            pbEmpty1.Tag = "0";
            toolTip.SetToolTip(pbEmpty1, "Click to attach a item");
            pbEmpty1.MouseDown += PbItemAttach_MouseDown;
            // 
            // numReturnDay
            // 
            numReturnDay.BackColor = Color.Black;
            numReturnDay.BorderStyle = BorderStyle.None;
            numReturnDay.ForeColor = Color.Gold;
            numReturnDay.Location = new Point(26, 473);
            numReturnDay.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            numReturnDay.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numReturnDay.Name = "numReturnDay";
            numReturnDay.RightToLeft = RightToLeft.Yes;
            numReturnDay.Size = new Size(88, 19);
            numReturnDay.TabIndex = 11;
            numReturnDay.Value = new decimal(new int[] { 7, 0, 0, 0 });
            // 
            // lbReturn
            // 
            lbReturn.AutoSize = true;
            lbReturn.BackColor = Color.Transparent;
            lbReturn.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lbReturn.ForeColor = Color.Coral;
            lbReturn.Location = new Point(25, 448);
            lbReturn.Name = "lbReturn";
            lbReturn.Size = new Size(83, 19);
            lbReturn.TabIndex = 12;
            lbReturn.Text = "Return Date";
            toolTip.SetToolTip(lbReturn, "Set a return date for the mail.\r\nMail will be returned if the amount requested is not sent before the return date.");
            // 
            // lbRGold
            // 
            lbRGold.AutoSize = true;
            lbRGold.BackColor = Color.Transparent;
            lbRGold.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lbRGold.ForeColor = Color.Coral;
            lbRGold.Location = new Point(25, 504);
            lbRGold.Margin = new Padding(4, 0, 4, 0);
            lbRGold.Name = "lbRGold";
            lbRGold.Size = new Size(85, 19);
            lbRGold.TabIndex = 21;
            lbRGold.Text = "Item Charge";
            toolTip.SetToolTip(lbRGold, "Mail will be returned if recipient doesnt send the specified amount.");
            // 
            // numReqGold
            // 
            numReqGold.BackColor = Color.Black;
            numReqGold.BorderStyle = BorderStyle.None;
            numReqGold.ForeColor = Color.Gold;
            numReqGold.Location = new Point(27, 529);
            numReqGold.Maximum = new decimal(new int[] { 2100000000, 0, 0, 0 });
            numReqGold.Name = "numReqGold";
            numReqGold.RightToLeft = RightToLeft.Yes;
            numReqGold.Size = new Size(124, 19);
            numReqGold.TabIndex = 22;
            // 
            // chkSendToAll
            // 
            chkSendToAll.AutoSize = true;
            chkSendToAll.BackColor = Color.Transparent;
            chkSendToAll.Location = new Point(340, 53);
            chkSendToAll.Name = "chkSendToAll";
            chkSendToAll.Size = new Size(142, 19);
            chkSendToAll.TabIndex = 24;
            chkSendToAll.Text = "Send to All Characters";
            toolTip.SetToolTip(chkSendToAll, "If checked the mail will be send to all characters on database.");
            chkSendToAll.UseVisualStyleBackColor = false;
            chkSendToAll.CheckedChanged += ChkSendToAll_CheckedChanged;
            // 
            // pbGold
            // 
            pbGold.BackColor = Color.Transparent;
            pbGold.Image = Properties.Resources.lb_ac_icon_gold;
            pbGold.Location = new Point(158, 423);
            pbGold.Name = "pbGold";
            pbGold.Size = new Size(19, 19);
            pbGold.TabIndex = 23;
            pbGold.TabStop = false;
            toolTip.SetToolTip(pbGold, "Enter Gold amount you wish to send.");
            // 
            // txtMailContent
            // 
            txtMailContent.BackColor = Color.Black;
            txtMailContent.BorderStyle = BorderStyle.None;
            txtMailContent.ForeColor = Color.White;
            txtMailContent.Location = new Point(8, 13);
            txtMailContent.Margin = new Padding(4, 3, 4, 3);
            txtMailContent.MaxLength = 100;
            txtMailContent.Multiline = true;
            txtMailContent.Name = "txtMailContent";
            txtMailContent.ScrollBars = ScrollBars.Horizontal;
            txtMailContent.Size = new Size(342, 205);
            txtMailContent.TabIndex = 1;
            txtMailContent.Text = "Game Master InsertItem";
            // 
            // numMailGold
            // 
            numMailGold.BackColor = Color.Black;
            numMailGold.BorderStyle = BorderStyle.None;
            numMailGold.ForeColor = Color.Gold;
            numMailGold.Location = new Point(26, 424);
            numMailGold.Maximum = new decimal(new int[] { 2100000000, 0, 0, 0 });
            numMailGold.Name = "numMailGold";
            numMailGold.RightToLeft = RightToLeft.Yes;
            numMailGold.Size = new Size(125, 19);
            numMailGold.TabIndex = 17;
            // 
            // tbSender
            // 
            tbSender.BackColor = Color.Black;
            tbSender.BorderStyle = BorderStyle.None;
            tbSender.ForeColor = Color.Gold;
            tbSender.Location = new Point(118, 87);
            tbSender.Margin = new Padding(4, 3, 4, 3);
            tbSender.Name = "tbSender";
            tbSender.Size = new Size(206, 16);
            tbSender.TabIndex = 10;
            tbSender.Text = "GameMaster";
            // 
            // lbGold
            // 
            lbGold.AutoSize = true;
            lbGold.BackColor = Color.Transparent;
            lbGold.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lbGold.Location = new Point(25, 400);
            lbGold.Margin = new Padding(4, 0, 4, 0);
            lbGold.Name = "lbGold";
            lbGold.Size = new Size(136, 19);
            lbGold.TabIndex = 3;
            lbGold.Text = "Attach Gold Amount";
            toolTip.SetToolTip(lbGold, "Gold value to be included in the mail.");
            // 
            // txtRecipient
            // 
            txtRecipient.BackColor = Color.Black;
            txtRecipient.BorderStyle = BorderStyle.None;
            txtRecipient.ForeColor = Color.Gold;
            txtRecipient.Location = new Point(118, 53);
            txtRecipient.Margin = new Padding(4, 3, 4, 3);
            txtRecipient.Name = "txtRecipient";
            txtRecipient.Size = new Size(206, 16);
            txtRecipient.TabIndex = 1;
            // 
            // lbRecipient
            // 
            lbRecipient.AutoSize = true;
            lbRecipient.BackColor = Color.Transparent;
            lbRecipient.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lbRecipient.Location = new Point(21, 48);
            lbRecipient.Margin = new Padding(4, 0, 4, 0);
            lbRecipient.Name = "lbRecipient";
            lbRecipient.Size = new Size(78, 19);
            lbRecipient.TabIndex = 0;
            lbRecipient.Text = "Recipient(s)";
            toolTip.SetToolTip(lbRecipient, "Name of the characters that will receive the mail.\r\nNames must be sepparated by comma \",\"");
            // 
            // btnLoadTemplate
            // 
            btnLoadTemplate.BackColor = Color.Transparent;
            btnLoadTemplate.FlatAppearance.BorderColor = Color.Black;
            btnLoadTemplate.FlatAppearance.BorderSize = 0;
            btnLoadTemplate.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnLoadTemplate.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnLoadTemplate.FlatStyle = FlatStyle.Flat;
            btnLoadTemplate.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnLoadTemplate.ForeColor = Color.Transparent;
            btnLoadTemplate.ImageIndex = 0;
            btnLoadTemplate.ImageList = imageListButton;
            btnLoadTemplate.ImeMode = ImeMode.NoControl;
            btnLoadTemplate.Location = new Point(374, 146);
            btnLoadTemplate.Name = "btnLoadTemplate";
            btnLoadTemplate.Size = new Size(120, 26);
            btnLoadTemplate.TabIndex = 86;
            btnLoadTemplate.Text = "Load Template";
            toolTip.SetToolTip(btnLoadTemplate, "Load a previous saved json template");
            btnLoadTemplate.UseVisualStyleBackColor = false;
            btnLoadTemplate.Click += BtnLoadTemplate_Click;
            btnLoadTemplate.MouseDown += Button_MouseDown;
            btnLoadTemplate.MouseLeave += Button_MouseLeave;
            btnLoadTemplate.MouseHover += Button_MouseHover;
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
            // btnClear
            // 
            btnClear.BackColor = Color.Transparent;
            btnClear.FlatAppearance.BorderColor = Color.Black;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnClear.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.ForeColor = Color.Transparent;
            btnClear.ImageIndex = 0;
            btnClear.ImageList = imageListButton;
            btnClear.ImeMode = ImeMode.NoControl;
            btnClear.Location = new Point(374, 178);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(120, 26);
            btnClear.TabIndex = 87;
            btnClear.Text = "Clear";
            toolTip.SetToolTip(btnClear, "Clear all current mail settings");
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += BtnClear_Click;
            btnClear.MouseDown += Button_MouseDown;
            btnClear.MouseLeave += Button_MouseLeave;
            btnClear.MouseHover += Button_MouseHover;
            // 
            // btnSaveTemplate
            // 
            btnSaveTemplate.BackColor = Color.Transparent;
            btnSaveTemplate.FlatAppearance.BorderColor = Color.Black;
            btnSaveTemplate.FlatAppearance.BorderSize = 0;
            btnSaveTemplate.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnSaveTemplate.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSaveTemplate.FlatStyle = FlatStyle.Flat;
            btnSaveTemplate.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnSaveTemplate.ForeColor = Color.Transparent;
            btnSaveTemplate.ImageIndex = 0;
            btnSaveTemplate.ImageList = imageListButton;
            btnSaveTemplate.ImeMode = ImeMode.NoControl;
            btnSaveTemplate.Location = new Point(374, 114);
            btnSaveTemplate.Name = "btnSaveTemplate";
            btnSaveTemplate.Size = new Size(120, 26);
            btnSaveTemplate.TabIndex = 88;
            btnSaveTemplate.Text = "Save Template";
            toolTip.SetToolTip(btnSaveTemplate, "Save the current mail as a json template");
            btnSaveTemplate.UseVisualStyleBackColor = false;
            btnSaveTemplate.Click += BtnSaveTemplate_Click;
            btnSaveTemplate.MouseDown += Button_MouseDown;
            btnSaveTemplate.MouseLeave += Button_MouseLeave;
            btnSaveTemplate.MouseHover += Button_MouseHover;
            // 
            // pbGold2
            // 
            pbGold2.BackColor = Color.Transparent;
            pbGold2.Image = Properties.Resources.lb_ac_icon_gold;
            pbGold2.Location = new Point(158, 528);
            pbGold2.Name = "pbGold2";
            pbGold2.Size = new Size(19, 19);
            pbGold2.TabIndex = 89;
            pbGold2.TabStop = false;
            toolTip.SetToolTip(pbGold2, "Enter Gold amount you wish to send.");
            // 
            // lbSender
            // 
            lbSender.AutoSize = true;
            lbSender.BackColor = Color.Transparent;
            lbSender.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lbSender.Location = new Point(21, 81);
            lbSender.Margin = new Padding(4, 0, 4, 0);
            lbSender.Name = "lbSender";
            lbSender.Size = new Size(51, 19);
            lbSender.TabIndex = 90;
            lbSender.Text = "Sender";
            toolTip.SetToolTip(lbSender, "Name of the characters that will receive the mail.\r\nNames must be sepparated by comma \",\"");
            // 
            // lbAttachItem
            // 
            lbAttachItem.AutoSize = true;
            lbAttachItem.BackColor = Color.Transparent;
            lbAttachItem.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lbAttachItem.Location = new Point(25, 364);
            lbAttachItem.Margin = new Padding(4, 0, 4, 0);
            lbAttachItem.Name = "lbAttachItem";
            lbAttachItem.Size = new Size(81, 19);
            lbAttachItem.TabIndex = 80;
            lbAttachItem.Text = "Attach Item";
            toolTip.SetToolTip(lbAttachItem, "Click on a slot to attach the item(s) you wish to send.");
            // 
            // pbItem02Branch
            // 
            pbItem02Branch.BackColor = Color.Transparent;
            pbItem02Branch.Image = Properties.Resources.lb_icon_slot_04epic;
            pbItem02Branch.Location = new Point(61, 7);
            pbItem02Branch.Name = "pbItem02Branch";
            pbItem02Branch.Size = new Size(42, 42);
            pbItem02Branch.SizeMode = PictureBoxSizeMode.AutoSize;
            pbItem02Branch.TabIndex = 92;
            pbItem02Branch.TabStop = false;
            pbItem02Branch.Tag = "1";
            toolTip.SetToolTip(pbItem02Branch, "Click to attach a item");
            pbItem02Branch.Visible = false;
            pbItem02Branch.MouseClick += PbItemAttach_MouseDown;
            pbItem02Branch.MouseDown += PbItemAttach_MouseDown;
            // 
            // pbItem03Branch
            // 
            pbItem03Branch.BackColor = Color.Transparent;
            pbItem03Branch.Image = Properties.Resources.lb_icon_slot_04epic;
            pbItem03Branch.Location = new Point(113, 7);
            pbItem03Branch.Name = "pbItem03Branch";
            pbItem03Branch.Size = new Size(42, 42);
            pbItem03Branch.SizeMode = PictureBoxSizeMode.AutoSize;
            pbItem03Branch.TabIndex = 93;
            pbItem03Branch.TabStop = false;
            pbItem03Branch.Tag = "2";
            toolTip.SetToolTip(pbItem03Branch, "Click to attach a item");
            pbItem03Branch.Visible = false;
            pbItem03Branch.MouseDown += PbItemAttach_MouseDown;
            // 
            // pbItemHelp
            // 
            pbItemHelp.BackColor = Color.Transparent;
            pbItemHelp.Cursor = Cursors.Help;
            pbItemHelp.Image = Properties.Resources.icon_help_tip;
            pbItemHelp.Location = new Point(113, 367);
            pbItemHelp.Name = "pbItemHelp";
            pbItemHelp.Size = new Size(16, 16);
            pbItemHelp.TabIndex = 99;
            pbItemHelp.TabStop = false;
            toolTip.SetToolTip(pbItemHelp, "Click on a slot to add/edit a item, Right click to remove.");
            // 
            // pbIcon01
            // 
            pbIcon01.BackColor = Color.Transparent;
            pbIcon01.Location = new Point(12, 10);
            pbIcon01.Name = "pbIcon01";
            pbIcon01.Size = new Size(36, 36);
            pbIcon01.SizeMode = PictureBoxSizeMode.StretchImage;
            pbIcon01.TabIndex = 77;
            pbIcon01.TabStop = false;
            pbIcon01.Tag = "0";
            pbIcon01.Visible = false;
            pbIcon01.MouseDown += PbItemAttach_MouseDown;
            // 
            // pbIcon02
            // 
            pbIcon02.BackColor = Color.Transparent;
            pbIcon02.Location = new Point(64, 10);
            pbIcon02.Name = "pbIcon02";
            pbIcon02.Size = new Size(36, 36);
            pbIcon02.SizeMode = PictureBoxSizeMode.AutoSize;
            pbIcon02.TabIndex = 78;
            pbIcon02.TabStop = false;
            pbIcon02.Tag = "1";
            pbIcon02.Visible = false;
            pbIcon02.MouseDown += PbItemAttach_MouseDown;
            // 
            // pbIcon03
            // 
            pbIcon03.BackColor = Color.Transparent;
            pbIcon03.Location = new Point(116, 10);
            pbIcon03.Name = "pbIcon03";
            pbIcon03.Size = new Size(36, 36);
            pbIcon03.SizeMode = PictureBoxSizeMode.AutoSize;
            pbIcon03.TabIndex = 79;
            pbIcon03.TabStop = false;
            pbIcon03.Tag = "2";
            pbIcon03.Visible = false;
            pbIcon03.MouseDown += PbItemAttach_MouseDown;
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.BackColor = Color.Transparent;
            lbTitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lbTitle.Location = new Point(219, 12);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(69, 19);
            lbTitle.TabIndex = 76;
            lbTitle.Text = "Send Mail";
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
            btnClose.Location = new Point(475, 9);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(25, 26);
            btnClose.TabIndex = 83;
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += BtnClose_Click;
            btnClose.MouseDown += BtnClose_MouseDown;
            btnClose.MouseLeave += BtnClose_MouseLeave;
            btnClose.MouseHover += BtnClose_MouseHover;
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.Transparent;
            btnSend.FlatAppearance.BorderColor = Color.Black;
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnSend.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnSend.ForeColor = Color.Transparent;
            btnSend.ImageIndex = 0;
            btnSend.ImageList = imageListButton;
            btnSend.ImeMode = ImeMode.NoControl;
            btnSend.Location = new Point(205, 548);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(120, 26);
            btnSend.TabIndex = 84;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += BtnSend_Click;
            btnSend.MouseDown += Button_MouseDown;
            btnSend.MouseLeave += Button_MouseLeave;
            btnSend.MouseHover += Button_MouseHover;
            // 
            // panelMessage
            // 
            panelMessage.BackColor = Color.Transparent;
            panelMessage.BackgroundImage = Properties.Resources.lb_mail_paper;
            panelMessage.Controls.Add(txtMailContent);
            panelMessage.Location = new Point(12, 114);
            panelMessage.Name = "panelMessage";
            panelMessage.Size = new Size(358, 227);
            panelMessage.TabIndex = 91;
            // 
            // imageListBranch
            // 
            imageListBranch.ColorDepth = ColorDepth.Depth32Bit;
            imageListBranch.ImageStream = (ImageListStreamer)resources.GetObject("imageListBranch.ImageStream");
            imageListBranch.TransparentColor = Color.Transparent;
            imageListBranch.Images.SetKeyName(0, "lb_icon_slot_00normal.png");
            imageListBranch.Images.SetKeyName(1, "lb_icon_slot_02rare.png");
            imageListBranch.Images.SetKeyName(2, "lb_icon_slot_03unique.png");
            imageListBranch.Images.SetKeyName(3, "lb_icon_slot_01magic.png");
            imageListBranch.Images.SetKeyName(4, "lb_icon_slot_04epic.png");
            // 
            // lbItemAmount3
            // 
            lbItemAmount3.Anchor = AnchorStyles.Right;
            lbItemAmount3.BackColor = Color.Transparent;
            lbItemAmount3.Location = new Point(118, 28);
            lbItemAmount3.Name = "lbItemAmount3";
            lbItemAmount3.Size = new Size(34, 18);
            lbItemAmount3.TabIndex = 95;
            lbItemAmount3.Text = "999";
            lbItemAmount3.TextAlign = ContentAlignment.MiddleRight;
            lbItemAmount3.Visible = false;
            // 
            // lbItemAmount2
            // 
            lbItemAmount2.Anchor = AnchorStyles.Right;
            lbItemAmount2.BackColor = Color.Transparent;
            lbItemAmount2.Location = new Point(66, 27);
            lbItemAmount2.Name = "lbItemAmount2";
            lbItemAmount2.Size = new Size(34, 18);
            lbItemAmount2.TabIndex = 96;
            lbItemAmount2.Text = "999";
            lbItemAmount2.TextAlign = ContentAlignment.MiddleRight;
            lbItemAmount2.Visible = false;
            // 
            // lbItemAmount1
            // 
            lbItemAmount1.Anchor = AnchorStyles.Right;
            lbItemAmount1.BackColor = Color.Transparent;
            lbItemAmount1.Location = new Point(14, 28);
            lbItemAmount1.Name = "lbItemAmount1";
            lbItemAmount1.Size = new Size(34, 17);
            lbItemAmount1.TabIndex = 97;
            lbItemAmount1.Text = "999";
            lbItemAmount1.TextAlign = ContentAlignment.MiddleRight;
            lbItemAmount1.Visible = false;
            // 
            // panelSlots
            // 
            panelSlots.BackColor = Color.Transparent;
            panelSlots.Controls.Add(lbItemAmount2);
            panelSlots.Controls.Add(pbItem02Branch);
            panelSlots.Controls.Add(lbItemAmount3);
            panelSlots.Controls.Add(pbIcon02);
            panelSlots.Controls.Add(lbItemAmount1);
            panelSlots.Controls.Add(pbItem01Branch);
            panelSlots.Controls.Add(pbItem03Branch);
            panelSlots.Controls.Add(pbEmpty1);
            panelSlots.Controls.Add(pbIcon01);
            panelSlots.Controls.Add(pbIcon03);
            panelSlots.Controls.Add(pbEmpty2);
            panelSlots.Controls.Add(pbEmpty3);
            panelSlots.Location = new Point(139, 347);
            panelSlots.Name = "panelSlots";
            panelSlots.Size = new Size(166, 54);
            panelSlots.TabIndex = 98;
            // 
            // MailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Magenta;
            BackgroundImage = Properties.Resources.MailForm;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(510, 600);
            Controls.Add(pbItemHelp);
            Controls.Add(panelSlots);
            Controls.Add(panelMessage);
            Controls.Add(lbSender);
            Controls.Add(pbGold2);
            Controls.Add(btnSaveTemplate);
            Controls.Add(btnClear);
            Controls.Add(btnLoadTemplate);
            Controls.Add(btnSend);
            Controls.Add(btnClose);
            Controls.Add(lbRGold);
            Controls.Add(numReqGold);
            Controls.Add(numReturnDay);
            Controls.Add(lbAttachItem);
            Controls.Add(lbReturn);
            Controls.Add(lbTitle);
            Controls.Add(lbRecipient);
            Controls.Add(txtRecipient);
            Controls.Add(lbGold);
            Controls.Add(chkSendToAll);
            Controls.Add(tbSender);
            Controls.Add(pbGold);
            Controls.Add(numMailGold);
            ForeColor = Color.Transparent;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MailForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rusty Hearts Mail Tool";
            TransparencyKey = Color.Magenta;
            FormClosed += MailForm_FormClosed;
            MouseDown += MailForm_MouseDown;
            ((System.ComponentModel.ISupportInitialize)pbItem01Branch).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbEmpty3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbEmpty2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbEmpty1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numReturnDay).EndInit();
            ((System.ComponentModel.ISupportInitialize)numReqGold).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbGold).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMailGold).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbGold2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbItem02Branch).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbItem03Branch).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbItemHelp).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbIcon01).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbIcon02).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbIcon03).EndInit();
            panelMessage.ResumeLayout(false);
            panelMessage.PerformLayout();
            panelSlots.ResumeLayout(false);
            panelSlots.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private NumericUpDown numReturnDay;
        private Label lbReturn;
        private Label lbRGold;
        private NumericUpDown numReqGold;
        private CheckBox chkSendToAll;
        private PictureBox pbGold;
        private Button btnSaveTemplate;
        private Button btnLoadTemplate;
        private Button btnClear;
        private TextBox txtMailContent;
        private NumericUpDown numMailGold;
        private TextBox tbSender;
        private Label lbGold;
        private TextBox txtRecipient;
        private Label lbRecipient;
        private ToolTip toolTip;
        public PictureBox pbEmpty1;
        public PictureBox pbItem01Branch;
        public PictureBox pbEmpty3;
        public PictureBox pbEmpty2;
        private Label lbTitle;
        public PictureBox pbIcon01;
        public PictureBox pbIcon02;
        public PictureBox pbIcon03;
        private Label lbAttachItem;
        private ImageList imageListClose;
        private Button btnClose;
        private Button btnSend;
        private ImageList imageListButton;
        private PictureBox pbGold2;
        private Label lbSender;
        private Panel panelMessage;
        public PictureBox pbItem02Branch;
        public PictureBox pbItem03Branch;
        private ImageList imageListBranch;
        public Label lbItemAmount3;
        public Label lbItemAmount2;
        public Label lbItemAmount1;
        private Panel panelSlots;
        private PictureBox pbItemHelp;
    }
}