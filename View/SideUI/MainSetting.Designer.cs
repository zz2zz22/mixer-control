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
            this.lb3 = new System.Windows.Forms.Label();
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
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCompanyLogo)).BeginInit();
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
            this.panelHeader.Size = new System.Drawing.Size(787, 65);
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
            this.btnClose.Location = new System.Drawing.Point(727, 3);
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
            this.lb1.Location = new System.Drawing.Point(12, 83);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(159, 38);
            this.lb1.TabIndex = 7;
            this.lb1.Text = "Địa chỉ IP của PLC:\r\nPLC IP address:";
            // 
            // txbPLCIpSetting
            // 
            this.txbPLCIpSetting.Location = new System.Drawing.Point(177, 83);
            this.txbPLCIpSetting.Name = "txbPLCIpSetting";
            this.txbPLCIpSetting.Size = new System.Drawing.Size(280, 27);
            this.txbPLCIpSetting.TabIndex = 8;
            this.txbPLCIpSetting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbPLCIpSetting_KeyPress);
            // 
            // txbDatabaseNo
            // 
            this.txbDatabaseNo.Location = new System.Drawing.Point(186, 133);
            this.txbDatabaseNo.Name = "txbDatabaseNo";
            this.txbDatabaseNo.Size = new System.Drawing.Size(271, 27);
            this.txbDatabaseNo.TabIndex = 10;
            this.txbDatabaseNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbDatabaseNo_KeyPress);
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.Location = new System.Drawing.Point(12, 133);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(168, 38);
            this.lb2.TabIndex = 9;
            this.lb2.Text = "Số thứ tự database:\r\nDatabase no:";
            // 
            // cbxPLCValueSetting
            // 
            this.cbxPLCValueSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPLCValueSetting.FormattingEnabled = true;
            this.cbxPLCValueSetting.Location = new System.Drawing.Point(16, 451);
            this.cbxPLCValueSetting.Name = "cbxPLCValueSetting";
            this.cbxPLCValueSetting.Size = new System.Drawing.Size(482, 27);
            this.cbxPLCValueSetting.TabIndex = 11;
            this.cbxPLCValueSetting.SelectionChangeCommitted += new System.EventHandler(this.cbxPLCValueSetting_SelectionChangeCommitted);
            // 
            // lbSettingAnnounce
            // 
            this.lbSettingAnnounce.AutoSize = true;
            this.lbSettingAnnounce.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSettingAnnounce.ForeColor = System.Drawing.Color.Red;
            this.lbSettingAnnounce.Location = new System.Drawing.Point(12, 481);
            this.lbSettingAnnounce.Name = "lbSettingAnnounce";
            this.lbSettingAnnounce.Size = new System.Drawing.Size(24, 19);
            this.lbSettingAnnounce.TabIndex = 12;
            this.lbSettingAnnounce.Text = "...";
            // 
            // lb3
            // 
            this.lb3.AutoSize = true;
            this.lb3.Location = new System.Drawing.Point(12, 410);
            this.lb3.Name = "lb3";
            this.lb3.Size = new System.Drawing.Size(225, 38);
            this.lb3.TabIndex = 13;
            this.lb3.Text = "Danh sách các biến cài đặt:\r\nSetting variable list:";
            // 
            // lb4
            // 
            this.lb4.AutoSize = true;
            this.lb4.Location = new System.Drawing.Point(12, 546);
            this.lb4.Name = "lb4";
            this.lb4.Size = new System.Drawing.Size(130, 38);
            this.lb4.TabIndex = 14;
            this.lb4.Text = "Địa chỉ offset:\r\nOffset address:";
            // 
            // txbStartNo
            // 
            this.txbStartNo.Location = new System.Drawing.Point(148, 546);
            this.txbStartNo.Name = "txbStartNo";
            this.txbStartNo.Size = new System.Drawing.Size(100, 27);
            this.txbStartNo.TabIndex = 15;
            this.txbStartNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbStartNo_KeyPress);
            // 
            // txbBitNo
            // 
            this.txbBitNo.Location = new System.Drawing.Point(274, 546);
            this.txbBitNo.Name = "txbBitNo";
            this.txbBitNo.Size = new System.Drawing.Size(100, 27);
            this.txbBitNo.TabIndex = 16;
            this.txbBitNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbBitNo_KeyDown);
            this.txbBitNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbBitNo_KeyPress);
            // 
            // lb5
            // 
            this.lb5.AutoSize = true;
            this.lb5.Location = new System.Drawing.Point(254, 549);
            this.lb5.Name = "lb5";
            this.lb5.Size = new System.Drawing.Size(14, 19);
            this.lb5.TabIndex = 17;
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
            this.btnSaveBaseSetting.Location = new System.Drawing.Point(581, 306);
            this.btnSaveBaseSetting.Name = "btnSaveBaseSetting";
            this.btnSaveBaseSetting.Size = new System.Drawing.Size(194, 67);
            this.btnSaveBaseSetting.TabIndex = 19;
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
            this.btnSaveOffset.Location = new System.Drawing.Point(572, 549);
            this.btnSaveOffset.Name = "btnSaveOffset";
            this.btnSaveOffset.Size = new System.Drawing.Size(194, 67);
            this.btnSaveOffset.TabIndex = 18;
            this.btnSaveOffset.TextColor = System.Drawing.Color.Black;
            this.btnSaveOffset.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnSaveOffset.Click += new System.EventHandler(this.btnSaveOffset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 38);
            this.label1.TabIndex = 20;
            this.label1.Text = "Tốc độ tối đa của động cơ (vòng/phút):\r\nMotor max speed (RPM):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 38);
            this.label2.TabIndex = 21;
            this.label2.Text = "Đường kính trục động cơ (mm):\r\nMotor spindle diameter (mm):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 281);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(266, 38);
            this.label3.TabIndex = 22;
            this.label3.Text = "Đường kính trục cảm biến (mm):\r\nSensor spindle diameter (mm):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 335);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(376, 38);
            this.label4.TabIndex = 23;
            this.label4.Text = "Tỉ lệ chuyển đổi giữ động cơ và cảm biến:\r\nTransmission ratio between motor and s" +
    "ensor:";
            // 
            // txbMotorMaxSpeed
            // 
            this.txbMotorMaxSpeed.Location = new System.Drawing.Point(337, 181);
            this.txbMotorMaxSpeed.Name = "txbMotorMaxSpeed";
            this.txbMotorMaxSpeed.Size = new System.Drawing.Size(215, 27);
            this.txbMotorMaxSpeed.TabIndex = 24;
            this.txbMotorMaxSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbMotorMaxSpeed_KeyPress);
            // 
            // txbMotorDiameter
            // 
            this.txbMotorDiameter.Location = new System.Drawing.Point(337, 226);
            this.txbMotorDiameter.Name = "txbMotorDiameter";
            this.txbMotorDiameter.Size = new System.Drawing.Size(215, 27);
            this.txbMotorDiameter.TabIndex = 25;
            this.txbMotorDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbMotorDiameter_KeyPress);
            // 
            // txbSensorDiameter
            // 
            this.txbSensorDiameter.Location = new System.Drawing.Point(337, 281);
            this.txbSensorDiameter.Name = "txbSensorDiameter";
            this.txbSensorDiameter.Size = new System.Drawing.Size(215, 27);
            this.txbSensorDiameter.TabIndex = 26;
            this.txbSensorDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbSensorDiameter_KeyPress);
            // 
            // txbTransmissionRatio
            // 
            this.txbTransmissionRatio.Location = new System.Drawing.Point(411, 335);
            this.txbTransmissionRatio.Name = "txbTransmissionRatio";
            this.txbTransmissionRatio.Size = new System.Drawing.Size(141, 27);
            this.txbTransmissionRatio.TabIndex = 27;
            this.txbTransmissionRatio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbTransmissionRatio_KeyPress);
            // 
            // MainSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(787, 643);
            this.Controls.Add(this.txbTransmissionRatio);
            this.Controls.Add(this.txbSensorDiameter);
            this.Controls.Add(this.txbMotorDiameter);
            this.Controls.Add(this.txbMotorMaxSpeed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSaveBaseSetting);
            this.Controls.Add(this.btnSaveOffset);
            this.Controls.Add(this.lb5);
            this.Controls.Add(this.txbBitNo);
            this.Controls.Add(this.txbStartNo);
            this.Controls.Add(this.lb4);
            this.Controls.Add(this.lb3);
            this.Controls.Add(this.lbSettingAnnounce);
            this.Controls.Add(this.cbxPLCValueSetting);
            this.Controls.Add(this.txbDatabaseNo);
            this.Controls.Add(this.lb2);
            this.Controls.Add(this.txbPLCIpSetting);
            this.Controls.Add(this.lb1);
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
        private System.Windows.Forms.Label lb3;
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
    }
}