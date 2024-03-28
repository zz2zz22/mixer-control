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
            this.btnSaveBaseSetting = new XanderUI.XUIButton();
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
            this.switchOpenLit = new XanderUI.XUISwitch();
            this.label9 = new System.Windows.Forms.Label();
            this.switchStopMode = new XanderUI.XUISwitch();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.switchOilMode = new XanderUI.XUISwitch();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txbOilFeederIP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txbOilFeederDatabase = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.switchHideReverse = new XanderUI.XUISwitch();
            this.switchTest = new XanderUI.XUISwitch();
            this.label11 = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCompanyLogo)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(977, 65);
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
            this.btnClose.Location = new System.Drawing.Point(917, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 56);
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
            this.pbxCompanyLogo.Size = new System.Drawing.Size(246, 63);
            this.pbxCompanyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxCompanyLogo.TabIndex = 0;
            this.pbxCompanyLogo.TabStop = false;
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Location = new System.Drawing.Point(6, 32);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(68, 19);
            this.lb1.TabIndex = 11;
            this.lb1.Text = "PLC IP:";
            // 
            // txbPLCIpSetting
            // 
            this.txbPLCIpSetting.Location = new System.Drawing.Point(80, 29);
            this.txbPLCIpSetting.Name = "txbPLCIpSetting";
            this.txbPLCIpSetting.Size = new System.Drawing.Size(265, 27);
            this.txbPLCIpSetting.TabIndex = 1;
            this.txbPLCIpSetting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbPLCIpSetting_KeyPress);
            // 
            // txbDatabaseNo
            // 
            this.txbDatabaseNo.Location = new System.Drawing.Point(445, 29);
            this.txbDatabaseNo.Name = "txbDatabaseNo";
            this.txbDatabaseNo.Size = new System.Drawing.Size(60, 27);
            this.txbDatabaseNo.TabIndex = 2;
            this.txbDatabaseNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbDatabaseNo_KeyPress);
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.Location = new System.Drawing.Point(351, 32);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(88, 19);
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
            this.lb4.Location = new System.Drawing.Point(526, 31);
            this.lb4.Name = "lb4";
            this.lb4.Size = new System.Drawing.Size(62, 19);
            this.lb4.TabIndex = 19;
            this.lb4.Text = "Offset:";
            // 
            // txbStartNo
            // 
            this.txbStartNo.Location = new System.Drawing.Point(530, 64);
            this.txbStartNo.Name = "txbStartNo";
            this.txbStartNo.Size = new System.Drawing.Size(81, 27);
            this.txbStartNo.TabIndex = 9;
            this.txbStartNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbStartNo_KeyPress);
            // 
            // txbBitNo
            // 
            this.txbBitNo.Location = new System.Drawing.Point(637, 64);
            this.txbBitNo.Name = "txbBitNo";
            this.txbBitNo.Size = new System.Drawing.Size(80, 27);
            this.txbBitNo.TabIndex = 10;
            this.txbBitNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbBitNo_KeyDown);
            this.txbBitNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbBitNo_KeyPress);
            // 
            // lb5
            // 
            this.lb5.AutoSize = true;
            this.lb5.Location = new System.Drawing.Point(617, 72);
            this.lb5.Name = "lb5";
            this.lb5.Size = new System.Drawing.Size(14, 19);
            this.lb5.TabIndex = 20;
            this.lb5.Text = ".";
            // 
            // btnSaveBaseSetting
            // 
            this.btnSaveBaseSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveBaseSetting.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSaveBaseSetting.ButtonImage = global::mixer_control_globalver.Properties.Resources.right_arrow;
            this.btnSaveBaseSetting.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnSaveBaseSetting.ButtonText = "Save setting";
            this.btnSaveBaseSetting.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnSaveBaseSetting.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnSaveBaseSetting.CornerRadius = 10;
            this.btnSaveBaseSetting.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveBaseSetting.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnSaveBaseSetting.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnSaveBaseSetting.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnSaveBaseSetting.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnSaveBaseSetting.Location = new System.Drawing.Point(771, 419);
            this.btnSaveBaseSetting.Name = "btnSaveBaseSetting";
            this.btnSaveBaseSetting.Size = new System.Drawing.Size(194, 61);
            this.btnSaveBaseSetting.TabIndex = 7;
            this.btnSaveBaseSetting.TextColor = System.Drawing.Color.Black;
            this.btnSaveBaseSetting.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnSaveBaseSetting.Click += new System.EventHandler(this.btnSaveBaseSetting_Click);
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
            this.btnSaveOffset.Location = new System.Drawing.Point(749, 72);
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
            this.label1.Location = new System.Drawing.Point(6, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(314, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "Maximum motor speed (round/minute):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 19);
            this.label2.TabIndex = 14;
            this.label2.Text = "Motor shaft diameter (mm):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 19);
            this.label3.TabIndex = 15;
            this.label3.Text = "Sensor shaft diameter (mm):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 19);
            this.label4.TabIndex = 16;
            this.label4.Text = "Conversion ratio:";
            // 
            // txbMotorMaxSpeed
            // 
            this.txbMotorMaxSpeed.Location = new System.Drawing.Point(326, 65);
            this.txbMotorMaxSpeed.Name = "txbMotorMaxSpeed";
            this.txbMotorMaxSpeed.Size = new System.Drawing.Size(99, 27);
            this.txbMotorMaxSpeed.TabIndex = 3;
            this.txbMotorMaxSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbMotorMaxSpeed_KeyPress);
            // 
            // txbMotorDiameter
            // 
            this.txbMotorDiameter.Location = new System.Drawing.Point(235, 98);
            this.txbMotorDiameter.Name = "txbMotorDiameter";
            this.txbMotorDiameter.Size = new System.Drawing.Size(98, 27);
            this.txbMotorDiameter.TabIndex = 4;
            this.txbMotorDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbMotorDiameter_KeyPress);
            // 
            // txbSensorDiameter
            // 
            this.txbSensorDiameter.Location = new System.Drawing.Point(243, 135);
            this.txbSensorDiameter.Name = "txbSensorDiameter";
            this.txbSensorDiameter.Size = new System.Drawing.Size(102, 27);
            this.txbSensorDiameter.TabIndex = 5;
            this.txbSensorDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbSensorDiameter_KeyPress);
            // 
            // txbTransmissionRatio
            // 
            this.txbTransmissionRatio.Location = new System.Drawing.Point(158, 170);
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
            this.groupBox1.Controls.Add(this.switchHideReverse);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.switchOpenLit);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.switchStopMode);
            this.groupBox1.Controls.Add(this.label8);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(953, 211);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mixer Settings";
            // 
            // switchOpenLit
            // 
            this.switchOpenLit.BackColor = System.Drawing.Color.Transparent;
            this.switchOpenLit.HandleOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(71)))), ((int)(((byte)(89)))));
            this.switchOpenLit.HandleOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(180)))), ((int)(((byte)(120)))));
            this.switchOpenLit.Location = new System.Drawing.Point(819, 83);
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
            this.label9.Location = new System.Drawing.Point(598, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(213, 19);
            this.label9.TabIndex = 20;
            this.label9.Text = "Open lid if announce skip:";
            // 
            // switchStopMode
            // 
            this.switchStopMode.BackColor = System.Drawing.Color.Transparent;
            this.switchStopMode.HandleOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(71)))), ((int)(((byte)(89)))));
            this.switchStopMode.HandleOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(180)))), ((int)(((byte)(120)))));
            this.switchStopMode.Location = new System.Drawing.Point(819, 21);
            this.switchStopMode.Name = "switchStopMode";
            this.switchStopMode.OffColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(129)))), ((int)(((byte)(136)))));
            this.switchStopMode.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(217)))), ((int)(((byte)(174)))));
            this.switchStopMode.Size = new System.Drawing.Size(60, 30);
            this.switchStopMode.SwitchState = XanderUI.XUISwitch.State.On;
            this.switchStopMode.SwitchStyle = XanderUI.XUISwitch.Style.iOS;
            this.switchStopMode.TabIndex = 19;
            this.switchStopMode.Text = "xuiSwitch1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(597, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(214, 19);
            this.label8.TabIndex = 18;
            this.label8.Text = "Stop motor between step:";
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
            this.groupBox2.Location = new System.Drawing.Point(16, 486);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(949, 145);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Offset Settings";
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
            this.groupBox3.Location = new System.Drawing.Point(12, 288);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(953, 125);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Oil Feeder Settings";
            // 
            // switchOilMode
            // 
            this.switchOilMode.BackColor = System.Drawing.Color.Transparent;
            this.switchOilMode.HandleOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(71)))), ((int)(((byte)(89)))));
            this.switchOilMode.HandleOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(180)))), ((int)(((byte)(120)))));
            this.switchOilMode.Location = new System.Drawing.Point(140, 81);
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
            this.label7.Location = new System.Drawing.Point(6, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 19);
            this.label7.TabIndex = 17;
            this.label7.Text = "Toggle oil feed:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 19);
            this.label5.TabIndex = 15;
            this.label5.Text = "Oil PLC IP:";
            // 
            // txbOilFeederIP
            // 
            this.txbOilFeederIP.Location = new System.Drawing.Point(106, 39);
            this.txbOilFeederIP.Name = "txbOilFeederIP";
            this.txbOilFeederIP.Size = new System.Drawing.Size(259, 27);
            this.txbOilFeederIP.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(397, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(266, 19);
            this.label6.TabIndex = 16;
            this.label6.Text = "Database (Current machine no.):";
            // 
            // txbOilFeederDatabase
            // 
            this.txbOilFeederDatabase.Location = new System.Drawing.Point(669, 39);
            this.txbOilFeederDatabase.Name = "txbOilFeederDatabase";
            this.txbOilFeederDatabase.Size = new System.Drawing.Size(52, 27);
            this.txbOilFeederDatabase.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(597, 158);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(171, 19);
            this.label10.TabIndex = 22;
            this.label10.Text = "Hide reverse button:";
            // 
            // switchHideReverse
            // 
            this.switchHideReverse.BackColor = System.Drawing.Color.Transparent;
            this.switchHideReverse.HandleOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(71)))), ((int)(((byte)(89)))));
            this.switchHideReverse.HandleOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(180)))), ((int)(((byte)(120)))));
            this.switchHideReverse.Location = new System.Drawing.Point(819, 147);
            this.switchHideReverse.Name = "switchHideReverse";
            this.switchHideReverse.OffColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(129)))), ((int)(((byte)(136)))));
            this.switchHideReverse.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(217)))), ((int)(((byte)(174)))));
            this.switchHideReverse.Size = new System.Drawing.Size(60, 30);
            this.switchHideReverse.SwitchState = XanderUI.XUISwitch.State.On;
            this.switchHideReverse.SwitchStyle = XanderUI.XUISwitch.Style.iOS;
            this.switchHideReverse.TabIndex = 23;
            this.switchHideReverse.Text = "xuiSwitch1";
            // 
            // switchTest
            // 
            this.switchTest.BackColor = System.Drawing.Color.Transparent;
            this.switchTest.HandleOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(71)))), ((int)(((byte)(89)))));
            this.switchTest.HandleOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(180)))), ((int)(((byte)(120)))));
            this.switchTest.Location = new System.Drawing.Point(272, 437);
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
            this.label11.Location = new System.Drawing.Point(29, 437);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(236, 19);
            this.label11.TabIndex = 24;
            this.label11.Text = "Skip material confirm to test:";
            // 
            // MainSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(977, 643);
            this.Controls.Add(this.switchTest);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSaveBaseSetting);
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private XanderUI.XUIButton btnSaveBaseSetting;
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
    }
}