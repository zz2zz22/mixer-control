namespace mixer_control_globalver.View.SideUI
{
    partial class MainSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainSetting));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnClose = new XanderUI.XUIButton();
            this.pbxCompanyLogo = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnReportFolder = new XanderUI.XUIButton();
            this.lb1 = new System.Windows.Forms.Label();
            this.txbPLCIpSetting = new System.Windows.Forms.TextBox();
            this.txbDatabaseNo = new System.Windows.Forms.TextBox();
            this.lb2 = new System.Windows.Forms.Label();
            this.cbxPLCValueSetting = new System.Windows.Forms.ComboBox();
            this.lbSettingAnnounce = new System.Windows.Forms.Label();
            this.lb4 = new System.Windows.Forms.Label();
            this.txbStartNo = new System.Windows.Forms.TextBox();
            this.txbBitNo = new System.Windows.Forms.TextBox();
            this.lb5 = new System.Windows.Forms.Label();
            this.btnSaveOffset = new XanderUI.XUIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbMotorMaxSpeed = new System.Windows.Forms.TextBox();
            this.txbMotorDiameter = new System.Windows.Forms.TextBox();
            this.txbSensorDiameter = new System.Windows.Forms.TextBox();
            this.txbTransmissionRatio = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageLogic1 = new System.Windows.Forms.TabPage();
            this.switchAlertPowder = new XanderUI.XUISwitch();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.switchStopMode = new XanderUI.XUISwitch();
            this.switchOpenLit = new XanderUI.XUISwitch();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPageSub1 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.switchHideReverse = new XanderUI.XUISwitch();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.switchOilMode = new XanderUI.XUISwitch();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txbOilFeederIP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txbOilFeederDatabase = new System.Windows.Forms.TextBox();
            this.switchTest = new XanderUI.XUISwitch();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControlMainSetting = new System.Windows.Forms.TabControl();
            this.tabPageBasicSetting = new System.Windows.Forms.TabPage();
            this.switchShowHiddenInfo = new XanderUI.XUISwitch();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPageSubSetting = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txbAuthorSkipPass = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.switchSkipPassword = new XanderUI.XUISwitch();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCompanyLogo)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageLogic1.SuspendLayout();
            this.tabPageSub1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControlMainSetting.SuspendLayout();
            this.tabPageBasicSetting.SuspendLayout();
            this.tabPageSubSetting.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.pbxCompanyLogo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(977, 59);
            this.panelHeader.TabIndex = 5;
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHeader_MouseDown);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnClose.ButtonImage = global::mixer_control_globalver.Properties.Resources.cancel;
            this.btnClose.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnClose.ButtonText = "Button";
            this.btnClose.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnClose.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.CornerRadius = 5;
            this.btnClose.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnClose.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnClose.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.ImagePosition = XanderUI.XUIButton.imgPosition.Center;
            this.btnClose.Location = new System.Drawing.Point(927, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 2;
            this.btnClose.TextColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbxCompanyLogo
            // 
            this.pbxCompanyLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pbxCompanyLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbxCompanyLogo.Image = global::mixer_control_globalver.Properties.Resources.logoTechlinkFix;
            this.pbxCompanyLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pbxCompanyLogo.Location = new System.Drawing.Point(0, 0);
            this.pbxCompanyLogo.Name = "pbxCompanyLogo";
            this.pbxCompanyLogo.Size = new System.Drawing.Size(154, 57);
            this.pbxCompanyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxCompanyLogo.TabIndex = 0;
            this.pbxCompanyLogo.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(10, 293);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(196, 19);
            this.label12.TabIndex = 19;
            this.label12.Text = "Cài thư mục lưu báo cáo:";
            // 
            // btnReportFolder
            // 
            this.btnReportFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReportFolder.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnReportFolder.ButtonImage = global::mixer_control_globalver.Properties.Resources.folder;
            this.btnReportFolder.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnReportFolder.ButtonText = "Button";
            this.btnReportFolder.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnReportFolder.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnReportFolder.CornerRadius = 5;
            this.btnReportFolder.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnReportFolder.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnReportFolder.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnReportFolder.ImagePosition = XanderUI.XUIButton.imgPosition.Center;
            this.btnReportFolder.Location = new System.Drawing.Point(213, 274);
            this.btnReportFolder.Name = "btnReportFolder";
            this.btnReportFolder.Size = new System.Drawing.Size(56, 56);
            this.btnReportFolder.TabIndex = 3;
            this.btnReportFolder.TextColor = System.Drawing.Color.DodgerBlue;
            this.btnReportFolder.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnReportFolder.Click += new System.EventHandler(this.btnReportFolder_Click);
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb1.Location = new System.Drawing.Point(6, 32);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(67, 19);
            this.lb1.TabIndex = 11;
            this.lb1.Text = "IP PLC:";
            // 
            // txbPLCIpSetting
            // 
            this.txbPLCIpSetting.Location = new System.Drawing.Point(79, 29);
            this.txbPLCIpSetting.Name = "txbPLCIpSetting";
            this.txbPLCIpSetting.Size = new System.Drawing.Size(265, 27);
            this.txbPLCIpSetting.TabIndex = 1;
            this.txbPLCIpSetting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbPLCIpSetting_KeyPress);
            // 
            // txbDatabaseNo
            // 
            this.txbDatabaseNo.Location = new System.Drawing.Point(95, 69);
            this.txbDatabaseNo.Name = "txbDatabaseNo";
            this.txbDatabaseNo.Size = new System.Drawing.Size(79, 27);
            this.txbDatabaseNo.TabIndex = 2;
            this.txbDatabaseNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbDatabaseNo_KeyPress);
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb2.Location = new System.Drawing.Point(6, 72);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(83, 19);
            this.lb2.TabIndex = 12;
            this.lb2.Text = "Database:";
            // 
            // cbxPLCValueSetting
            // 
            this.cbxPLCValueSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPLCValueSetting.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPLCValueSetting.FormattingEnabled = true;
            this.cbxPLCValueSetting.Location = new System.Drawing.Point(19, 31);
            this.cbxPLCValueSetting.Name = "cbxPLCValueSetting";
            this.cbxPLCValueSetting.Size = new System.Drawing.Size(482, 32);
            this.cbxPLCValueSetting.TabIndex = 8;
            this.cbxPLCValueSetting.SelectionChangeCommitted += new System.EventHandler(this.cbxPLCValueSetting_SelectionChangeCommitted);
            // 
            // lbSettingAnnounce
            // 
            this.lbSettingAnnounce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSettingAnnounce.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSettingAnnounce.ForeColor = System.Drawing.Color.Red;
            this.lbSettingAnnounce.Location = new System.Drawing.Point(0, 0);
            this.lbSettingAnnounce.Name = "lbSettingAnnounce";
            this.lbSettingAnnounce.Size = new System.Drawing.Size(486, 48);
            this.lbSettingAnnounce.TabIndex = 18;
            this.lbSettingAnnounce.Text = "...";
            // 
            // lb4
            // 
            this.lb4.AutoSize = true;
            this.lb4.Location = new System.Drawing.Point(524, 23);
            this.lb4.Name = "lb4";
            this.lb4.Size = new System.Drawing.Size(62, 19);
            this.lb4.TabIndex = 19;
            this.lb4.Text = "Offset:";
            // 
            // txbStartNo
            // 
            this.txbStartNo.Location = new System.Drawing.Point(528, 56);
            this.txbStartNo.Name = "txbStartNo";
            this.txbStartNo.Size = new System.Drawing.Size(81, 27);
            this.txbStartNo.TabIndex = 9;
            this.txbStartNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbStartNo_KeyPress);
            // 
            // txbBitNo
            // 
            this.txbBitNo.Location = new System.Drawing.Point(635, 56);
            this.txbBitNo.Name = "txbBitNo";
            this.txbBitNo.Size = new System.Drawing.Size(80, 27);
            this.txbBitNo.TabIndex = 10;
            this.txbBitNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbBitNo_KeyDown);
            this.txbBitNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbBitNo_KeyPress);
            // 
            // lb5
            // 
            this.lb5.AutoSize = true;
            this.lb5.Location = new System.Drawing.Point(615, 64);
            this.lb5.Name = "lb5";
            this.lb5.Size = new System.Drawing.Size(14, 19);
            this.lb5.TabIndex = 20;
            this.lb5.Text = ".";
            // 
            // btnSaveOffset
            // 
            this.btnSaveOffset.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSaveOffset.ButtonImage = global::mixer_control_globalver.Properties.Resources.right_arrow;
            this.btnSaveOffset.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnSaveOffset.ButtonText = "Save offset";
            this.btnSaveOffset.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnSaveOffset.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnSaveOffset.CornerRadius = 10;
            this.btnSaveOffset.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveOffset.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnSaveOffset.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnSaveOffset.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnSaveOffset.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnSaveOffset.Location = new System.Drawing.Point(755, 23);
            this.btnSaveOffset.Name = "btnSaveOffset";
            this.btnSaveOffset.Size = new System.Drawing.Size(194, 67);
            this.btnSaveOffset.TabIndex = 11;
            this.btnSaveOffset.TextColor = System.Drawing.Color.Black;
            this.btnSaveOffset.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnSaveOffset.Click += new System.EventHandler(this.btnSaveOffset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "Tốc độ tối đa của motor (vòng/ phút):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 19);
            this.label2.TabIndex = 14;
            this.label2.Text = "Đường kính motor (mm)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 19);
            this.label3.TabIndex = 15;
            this.label3.Text = "Đường kính sensor (mm):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 241);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 19);
            this.label4.TabIndex = 16;
            this.label4.Text = "Tí lệ truyền:";
            // 
            // txbMotorMaxSpeed
            // 
            this.txbMotorMaxSpeed.Location = new System.Drawing.Point(317, 114);
            this.txbMotorMaxSpeed.Name = "txbMotorMaxSpeed";
            this.txbMotorMaxSpeed.Size = new System.Drawing.Size(99, 27);
            this.txbMotorMaxSpeed.TabIndex = 3;
            this.txbMotorMaxSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbMotorMaxSpeed_KeyPress);
            // 
            // txbMotorDiameter
            // 
            this.txbMotorDiameter.Location = new System.Drawing.Point(213, 152);
            this.txbMotorDiameter.Name = "txbMotorDiameter";
            this.txbMotorDiameter.Size = new System.Drawing.Size(98, 27);
            this.txbMotorDiameter.TabIndex = 4;
            this.txbMotorDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbMotorDiameter_KeyPress);
            // 
            // txbSensorDiameter
            // 
            this.txbSensorDiameter.Location = new System.Drawing.Point(225, 196);
            this.txbSensorDiameter.Name = "txbSensorDiameter";
            this.txbSensorDiameter.Size = new System.Drawing.Size(101, 27);
            this.txbSensorDiameter.TabIndex = 5;
            this.txbSensorDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbSensorDiameter_KeyPress);
            // 
            // txbTransmissionRatio
            // 
            this.txbTransmissionRatio.Location = new System.Drawing.Point(115, 238);
            this.txbTransmissionRatio.Name = "txbTransmissionRatio";
            this.txbTransmissionRatio.Size = new System.Drawing.Size(95, 27);
            this.txbTransmissionRatio.TabIndex = 6;
            this.txbTransmissionRatio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbTransmissionRatio_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbSettingAnnounce);
            this.panel1.Location = new System.Drawing.Point(19, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(486, 48);
            this.panel1.TabIndex = 21;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.btnReportFolder);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lb1);
            this.groupBox1.Controls.Add(this.txbPLCIpSetting);
            this.groupBox1.Controls.Add(this.txbTransmissionRatio);
            this.groupBox1.Controls.Add(this.lb2);
            this.groupBox1.Controls.Add(this.txbSensorDiameter);
            this.groupBox1.Controls.Add(this.txbDatabaseNo);
            this.groupBox1.Controls.Add(this.txbMotorDiameter);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txbMotorMaxSpeed);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(957, 348);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cài đặt máy trộn";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageLogic1);
            this.tabControl1.Controls.Add(this.tabPageSub1);
            this.tabControl1.Location = new System.Drawing.Point(422, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(529, 313);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPageLogic1
            // 
            this.tabPageLogic1.BackColor = System.Drawing.Color.PeachPuff;
            this.tabPageLogic1.Controls.Add(this.switchSkipPassword);
            this.tabPageLogic1.Controls.Add(this.label15);
            this.tabPageLogic1.Controls.Add(this.switchAlertPowder);
            this.tabPageLogic1.Controls.Add(this.label14);
            this.tabPageLogic1.Controls.Add(this.label8);
            this.tabPageLogic1.Controls.Add(this.switchStopMode);
            this.tabPageLogic1.Controls.Add(this.switchOpenLit);
            this.tabPageLogic1.Controls.Add(this.label9);
            this.tabPageLogic1.Location = new System.Drawing.Point(4, 28);
            this.tabPageLogic1.Name = "tabPageLogic1";
            this.tabPageLogic1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLogic1.Size = new System.Drawing.Size(521, 281);
            this.tabPageLogic1.TabIndex = 0;
            this.tabPageLogic1.Text = "Logic triggers";
            // 
            // switchAlertPowder
            // 
            this.switchAlertPowder.BackColor = System.Drawing.Color.Transparent;
            this.switchAlertPowder.HandleOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(71)))), ((int)(((byte)(89)))));
            this.switchAlertPowder.HandleOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(180)))), ((int)(((byte)(120)))));
            this.switchAlertPowder.Location = new System.Drawing.Point(343, 134);
            this.switchAlertPowder.Name = "switchAlertPowder";
            this.switchAlertPowder.OffColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(129)))), ((int)(((byte)(136)))));
            this.switchAlertPowder.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(217)))), ((int)(((byte)(174)))));
            this.switchAlertPowder.Size = new System.Drawing.Size(60, 30);
            this.switchAlertPowder.SwitchState = XanderUI.XUISwitch.State.On;
            this.switchAlertPowder.SwitchStyle = XanderUI.XUISwitch.Style.iOS;
            this.switchAlertPowder.TabIndex = 23;
            this.switchAlertPowder.Text = "xuiSwitch1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 125);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(330, 57);
            this.label14.TabIndex = 22;
            this.label14.Text = "Mở thông báo số bao bột cần cấp trước \r\nvà sau khi cấp dầu\r\n(Cài đặt ở trong file" +
    " excel công thức)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(272, 38);
            this.label8.TabIndex = 18;
            this.label8.Text = "Dừng động cơ giữa mỗi bước\r\n(Kết thúc bước là dừng động cơ)";
            // 
            // switchStopMode
            // 
            this.switchStopMode.BackColor = System.Drawing.Color.Transparent;
            this.switchStopMode.HandleOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(71)))), ((int)(((byte)(89)))));
            this.switchStopMode.HandleOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(180)))), ((int)(((byte)(120)))));
            this.switchStopMode.Location = new System.Drawing.Point(285, 17);
            this.switchStopMode.Name = "switchStopMode";
            this.switchStopMode.OffColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(129)))), ((int)(((byte)(136)))));
            this.switchStopMode.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(217)))), ((int)(((byte)(174)))));
            this.switchStopMode.Size = new System.Drawing.Size(60, 30);
            this.switchStopMode.SwitchState = XanderUI.XUISwitch.State.On;
            this.switchStopMode.SwitchStyle = XanderUI.XUISwitch.Style.iOS;
            this.switchStopMode.TabIndex = 19;
            this.switchStopMode.Text = "xuiSwitch1";
            // 
            // switchOpenLit
            // 
            this.switchOpenLit.BackColor = System.Drawing.Color.Transparent;
            this.switchOpenLit.HandleOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(71)))), ((int)(((byte)(89)))));
            this.switchOpenLit.HandleOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(180)))), ((int)(((byte)(120)))));
            this.switchOpenLit.Location = new System.Drawing.Point(311, 69);
            this.switchOpenLit.Name = "switchOpenLit";
            this.switchOpenLit.OffColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(129)))), ((int)(((byte)(136)))));
            this.switchOpenLit.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(217)))), ((int)(((byte)(174)))));
            this.switchOpenLit.Size = new System.Drawing.Size(60, 30);
            this.switchOpenLit.SwitchState = XanderUI.XUISwitch.State.On;
            this.switchOpenLit.SwitchStyle = XanderUI.XUISwitch.Style.iOS;
            this.switchOpenLit.TabIndex = 21;
            this.switchOpenLit.Text = "xuiSwitch1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(299, 38);
            this.label9.TabIndex = 20;
            this.label9.Text = "Mở nắp nếu bỏ qua thông báo\r\n(Cài đặt ở trong file excel công thức)";
            // 
            // tabPageSub1
            // 
            this.tabPageSub1.BackColor = System.Drawing.Color.PeachPuff;
            this.tabPageSub1.Controls.Add(this.label10);
            this.tabPageSub1.Controls.Add(this.switchHideReverse);
            this.tabPageSub1.Location = new System.Drawing.Point(4, 25);
            this.tabPageSub1.Name = "tabPageSub1";
            this.tabPageSub1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSub1.Size = new System.Drawing.Size(521, 284);
            this.tabPageSub1.TabIndex = 1;
            this.tabPageSub1.Text = "Sub triggers";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(162, 19);
            this.label10.TabIndex = 22;
            this.label10.Text = "Ẩn nút quay ngược";
            // 
            // switchHideReverse
            // 
            this.switchHideReverse.BackColor = System.Drawing.Color.Transparent;
            this.switchHideReverse.HandleOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(71)))), ((int)(((byte)(89)))));
            this.switchHideReverse.HandleOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(180)))), ((int)(((byte)(120)))));
            this.switchHideReverse.Location = new System.Drawing.Point(183, 15);
            this.switchHideReverse.Name = "switchHideReverse";
            this.switchHideReverse.OffColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(129)))), ((int)(((byte)(136)))));
            this.switchHideReverse.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(217)))), ((int)(((byte)(174)))));
            this.switchHideReverse.Size = new System.Drawing.Size(60, 30);
            this.switchHideReverse.SwitchState = XanderUI.XUISwitch.State.On;
            this.switchHideReverse.SwitchStyle = XanderUI.XUISwitch.Style.iOS;
            this.switchHideReverse.TabIndex = 23;
            this.switchHideReverse.Text = "xuiSwitch1";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox2.Controls.Add(this.cbxPLCValueSetting);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.lb4);
            this.groupBox2.Controls.Add(this.btnSaveOffset);
            this.groupBox2.Controls.Add(this.txbStartNo);
            this.groupBox2.Controls.Add(this.lb5);
            this.groupBox2.Controls.Add(this.txbBitNo);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(963, 141);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cài đặt offset";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox3.Controls.Add(this.switchOilMode);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txbOilFeederIP);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txbOilFeederDatabase);
            this.groupBox3.Location = new System.Drawing.Point(6, 372);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(957, 99);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cài đặt máy cấp dầu";
            // 
            // switchOilMode
            // 
            this.switchOilMode.BackColor = System.Drawing.Color.Transparent;
            this.switchOilMode.HandleOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(71)))), ((int)(((byte)(89)))));
            this.switchOilMode.HandleOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(180)))), ((int)(((byte)(120)))));
            this.switchOilMode.Location = new System.Drawing.Point(846, 39);
            this.switchOilMode.Name = "switchOilMode";
            this.switchOilMode.OffColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(129)))), ((int)(((byte)(136)))));
            this.switchOilMode.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(217)))), ((int)(((byte)(174)))));
            this.switchOilMode.Size = new System.Drawing.Size(60, 30);
            this.switchOilMode.SwitchState = XanderUI.XUISwitch.State.On;
            this.switchOilMode.SwitchStyle = XanderUI.XUISwitch.Style.iOS;
            this.switchOilMode.TabIndex = 18;
            this.switchOilMode.Text = "xuiSwitch1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(675, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 19);
            this.label7.TabIndex = 17;
            this.label7.Text = "Mở chế độ cấp dầu:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 19);
            this.label5.TabIndex = 15;
            this.label5.Text = "IP máy cấp dầu";
            // 
            // txbOilFeederIP
            // 
            this.txbOilFeederIP.Location = new System.Drawing.Point(140, 39);
            this.txbOilFeederIP.Name = "txbOilFeederIP";
            this.txbOilFeederIP.Size = new System.Drawing.Size(259, 27);
            this.txbOilFeederIP.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(405, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 19);
            this.label6.TabIndex = 16;
            this.label6.Text = "Database:";
            // 
            // txbOilFeederDatabase
            // 
            this.txbOilFeederDatabase.Location = new System.Drawing.Point(499, 39);
            this.txbOilFeederDatabase.Name = "txbOilFeederDatabase";
            this.txbOilFeederDatabase.Size = new System.Drawing.Size(82, 27);
            this.txbOilFeederDatabase.TabIndex = 14;
            // 
            // switchTest
            // 
            this.switchTest.BackColor = System.Drawing.Color.Transparent;
            this.switchTest.HandleOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(71)))), ((int)(((byte)(89)))));
            this.switchTest.HandleOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(180)))), ((int)(((byte)(120)))));
            this.switchTest.Location = new System.Drawing.Point(362, 492);
            this.switchTest.Name = "switchTest";
            this.switchTest.OffColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(129)))), ((int)(((byte)(136)))));
            this.switchTest.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(217)))), ((int)(((byte)(174)))));
            this.switchTest.Size = new System.Drawing.Size(60, 30);
            this.switchTest.SwitchState = XanderUI.XUISwitch.State.On;
            this.switchTest.SwitchStyle = XanderUI.XUISwitch.Style.iOS;
            this.switchTest.TabIndex = 25;
            this.switchTest.Text = "xuiSwitch1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 492);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(343, 38);
            this.label11.TabIndex = 24;
            this.label11.Text = "Bỏ qua bước xác nhận nguyên liệu để test\r\n(Chức năng cho kỹ thuật viên)";
            // 
            // tabControlMainSetting
            // 
            this.tabControlMainSetting.Controls.Add(this.tabPageBasicSetting);
            this.tabControlMainSetting.Controls.Add(this.tabPageSubSetting);
            this.tabControlMainSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMainSetting.Location = new System.Drawing.Point(0, 59);
            this.tabControlMainSetting.Name = "tabControlMainSetting";
            this.tabControlMainSetting.SelectedIndex = 0;
            this.tabControlMainSetting.Size = new System.Drawing.Size(977, 584);
            this.tabControlMainSetting.TabIndex = 26;
            // 
            // tabPageBasicSetting
            // 
            this.tabPageBasicSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tabPageBasicSetting.Controls.Add(this.switchShowHiddenInfo);
            this.tabPageBasicSetting.Controls.Add(this.groupBox1);
            this.tabPageBasicSetting.Controls.Add(this.switchTest);
            this.tabPageBasicSetting.Controls.Add(this.groupBox3);
            this.tabPageBasicSetting.Controls.Add(this.label13);
            this.tabPageBasicSetting.Controls.Add(this.label11);
            this.tabPageBasicSetting.Location = new System.Drawing.Point(4, 28);
            this.tabPageBasicSetting.Name = "tabPageBasicSetting";
            this.tabPageBasicSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBasicSetting.Size = new System.Drawing.Size(969, 552);
            this.tabPageBasicSetting.TabIndex = 0;
            this.tabPageBasicSetting.Text = "Cài đặt cơ bản";
            // 
            // switchShowHiddenInfo
            // 
            this.switchShowHiddenInfo.BackColor = System.Drawing.Color.Transparent;
            this.switchShowHiddenInfo.HandleOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(71)))), ((int)(((byte)(89)))));
            this.switchShowHiddenInfo.HandleOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(180)))), ((int)(((byte)(120)))));
            this.switchShowHiddenInfo.Location = new System.Drawing.Point(811, 492);
            this.switchShowHiddenInfo.Name = "switchShowHiddenInfo";
            this.switchShowHiddenInfo.OffColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(129)))), ((int)(((byte)(136)))));
            this.switchShowHiddenInfo.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(217)))), ((int)(((byte)(174)))));
            this.switchShowHiddenInfo.Size = new System.Drawing.Size(60, 30);
            this.switchShowHiddenInfo.SwitchState = XanderUI.XUISwitch.State.On;
            this.switchShowHiddenInfo.SwitchStyle = XanderUI.XUISwitch.Style.iOS;
            this.switchShowHiddenInfo.TabIndex = 25;
            this.switchShowHiddenInfo.Text = "xuiSwitch1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(454, 492);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(351, 38);
            this.label13.TabIndex = 24;
            this.label13.Text = "Kích hoạt xem thông số ẩn của công thức\r\n(Bấm vào tên công thức trong tab tự động" +
    ")";
            // 
            // tabPageSubSetting
            // 
            this.tabPageSubSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tabPageSubSetting.Controls.Add(this.groupBox4);
            this.tabPageSubSetting.Controls.Add(this.groupBox2);
            this.tabPageSubSetting.Location = new System.Drawing.Point(4, 28);
            this.tabPageSubSetting.Name = "tabPageSubSetting";
            this.tabPageSubSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSubSetting.Size = new System.Drawing.Size(969, 552);
            this.tabPageSubSetting.TabIndex = 1;
            this.tabPageSubSetting.Text = "Cài đặt khác";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox4.Controls.Add(this.txbAuthorSkipPass);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 144);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(963, 405);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Cài đặt khác";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(6, 33);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(159, 19);
            this.label16.TabIndex = 19;
            this.label16.Text = "Mật khẩu skip bước:";
            // 
            // txbAuthorSkipPass
            // 
            this.txbAuthorSkipPass.Location = new System.Drawing.Point(171, 30);
            this.txbAuthorSkipPass.Name = "txbAuthorSkipPass";
            this.txbAuthorSkipPass.Size = new System.Drawing.Size(258, 27);
            this.txbAuthorSkipPass.TabIndex = 20;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 196);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(356, 38);
            this.label15.TabIndex = 24;
            this.label15.Text = "Khoá mật khẩu cho nút bỏ qua bước\r\n(Bật lên để nhập mật khẩu khi bỏ qua bước)";
            // 
            // switchSkipPassword
            // 
            this.switchSkipPassword.BackColor = System.Drawing.Color.Transparent;
            this.switchSkipPassword.HandleOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(71)))), ((int)(((byte)(89)))));
            this.switchSkipPassword.HandleOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(180)))), ((int)(((byte)(120)))));
            this.switchSkipPassword.Location = new System.Drawing.Point(369, 196);
            this.switchSkipPassword.Name = "switchSkipPassword";
            this.switchSkipPassword.OffColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(129)))), ((int)(((byte)(136)))));
            this.switchSkipPassword.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(217)))), ((int)(((byte)(174)))));
            this.switchSkipPassword.Size = new System.Drawing.Size(60, 30);
            this.switchSkipPassword.SwitchState = XanderUI.XUISwitch.State.On;
            this.switchSkipPassword.SwitchStyle = XanderUI.XUISwitch.Style.iOS;
            this.switchSkipPassword.TabIndex = 25;
            this.switchSkipPassword.Text = "xuiSwitch1";
            // 
            // MainSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(977, 643);
            this.Controls.Add(this.tabControlMainSetting);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt / Setting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainSetting_FormClosing);
            this.Load += new System.EventHandler(this.MainSetting_Load);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxCompanyLogo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageLogic1.ResumeLayout(false);
            this.tabPageLogic1.PerformLayout();
            this.tabPageSub1.ResumeLayout(false);
            this.tabPageSub1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControlMainSetting.ResumeLayout(false);
            this.tabPageBasicSetting.ResumeLayout(false);
            this.tabPageBasicSetting.PerformLayout();
            this.tabPageSubSetting.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelHeader;
        private XanderUI.XUIButton btnClose;
        private System.Windows.Forms.PictureBox pbxCompanyLogo;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.TextBox txbPLCIpSetting;
        private System.Windows.Forms.TextBox txbDatabaseNo;
        private System.Windows.Forms.Label lb2;
        private System.Windows.Forms.ComboBox cbxPLCValueSetting;
        private System.Windows.Forms.Label lbSettingAnnounce;
        private System.Windows.Forms.Label lb4;
        private System.Windows.Forms.TextBox txbStartNo;
        private System.Windows.Forms.TextBox txbBitNo;
        private System.Windows.Forms.Label lb5;
        private XanderUI.XUIButton btnSaveOffset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbMotorMaxSpeed;
        private System.Windows.Forms.TextBox txbMotorDiameter;
        private System.Windows.Forms.TextBox txbSensorDiameter;
        private System.Windows.Forms.TextBox txbTransmissionRatio;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbOilFeederIP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbOilFeederDatabase;
        private System.Windows.Forms.Label label7;
        private XanderUI.XUISwitch switchOilMode;
        private XanderUI.XUISwitch switchStopMode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private XanderUI.XUISwitch switchOpenLit;
        private XanderUI.XUISwitch switchHideReverse;
        private System.Windows.Forms.Label label10;
        private XanderUI.XUISwitch switchTest;
        private System.Windows.Forms.Label label11;
        private XanderUI.XUIButton btnReportFolder;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabControl tabControlMainSetting;
        private System.Windows.Forms.TabPage tabPageBasicSetting;
        private System.Windows.Forms.TabPage tabPageSubSetting;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageLogic1;
        private System.Windows.Forms.TabPage tabPageSub1;
        private System.Windows.Forms.Label label13;
        private XanderUI.XUISwitch switchShowHiddenInfo;
        private System.Windows.Forms.Label label14;
        private XanderUI.XUISwitch switchAlertPowder;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txbAuthorSkipPass;
        private System.Windows.Forms.Label label15;
        private XanderUI.XUISwitch switchSkipPassword;
    }
}