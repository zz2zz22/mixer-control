namespace mixer_control_globalver.View.MainUI
{
    partial class AutomationInfo
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
            this.panelAutomationInfoMain = new System.Windows.Forms.Panel();
            this.lbFormulaName = new System.Windows.Forms.Label();
            this.lbShowF = new System.Windows.Forms.Label();
            this.btnContinueStep = new XanderUI.XUIButton();
            this.btnStartProcess = new XanderUI.XUIButton();
            this.btnActivateSpeedControl = new System.Windows.Forms.Button();
            this.rtbRemark = new System.Windows.Forms.RichTextBox();
            this.btnResetRoll = new System.Windows.Forms.Button();
            this.lbProcessNo = new System.Windows.Forms.Label();
            this.btnReverseRoll = new System.Windows.Forms.Button();
            this.btnNormalRoll = new System.Windows.Forms.Button();
            this.panelShowSpeed = new System.Windows.Forms.Panel();
            this.lb2 = new System.Windows.Forms.Label();
            this.lbRollSpeed = new System.Windows.Forms.Label();
            this.lb1 = new System.Windows.Forms.Label();
            this.panelShowTemperature = new System.Windows.Forms.Panel();
            this.lb4 = new System.Windows.Forms.Label();
            this.lbTemperature = new System.Windows.Forms.Label();
            this.lb3 = new System.Windows.Forms.Label();
            this.lbCountDown = new System.Windows.Forms.Label();
            this.lb5 = new System.Windows.Forms.Label();
            this.panelAutomationInfoMain.SuspendLayout();
            this.panelShowSpeed.SuspendLayout();
            this.panelShowTemperature.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAutomationInfoMain
            // 
            this.panelAutomationInfoMain.Controls.Add(this.lbFormulaName);
            this.panelAutomationInfoMain.Controls.Add(this.lbShowF);
            this.panelAutomationInfoMain.Controls.Add(this.btnContinueStep);
            this.panelAutomationInfoMain.Controls.Add(this.btnStartProcess);
            this.panelAutomationInfoMain.Controls.Add(this.btnActivateSpeedControl);
            this.panelAutomationInfoMain.Controls.Add(this.rtbRemark);
            this.panelAutomationInfoMain.Controls.Add(this.btnResetRoll);
            this.panelAutomationInfoMain.Controls.Add(this.lbProcessNo);
            this.panelAutomationInfoMain.Controls.Add(this.btnReverseRoll);
            this.panelAutomationInfoMain.Controls.Add(this.btnNormalRoll);
            this.panelAutomationInfoMain.Controls.Add(this.panelShowSpeed);
            this.panelAutomationInfoMain.Controls.Add(this.panelShowTemperature);
            this.panelAutomationInfoMain.Controls.Add(this.lbCountDown);
            this.panelAutomationInfoMain.Controls.Add(this.lb5);
            this.panelAutomationInfoMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAutomationInfoMain.Location = new System.Drawing.Point(0, 0);
            this.panelAutomationInfoMain.Name = "panelAutomationInfoMain";
            this.panelAutomationInfoMain.Size = new System.Drawing.Size(759, 608);
            this.panelAutomationInfoMain.TabIndex = 1;
            // 
            // lbFormulaName
            // 
            this.lbFormulaName.AutoSize = true;
            this.lbFormulaName.Location = new System.Drawing.Point(147, 21);
            this.lbFormulaName.Name = "lbFormulaName";
            this.lbFormulaName.Size = new System.Drawing.Size(24, 19);
            this.lbFormulaName.TabIndex = 27;
            this.lbFormulaName.Text = "...";
            // 
            // lbShowF
            // 
            this.lbShowF.AutoSize = true;
            this.lbShowF.Location = new System.Drawing.Point(12, 12);
            this.lbShowF.Name = "lbShowF";
            this.lbShowF.Size = new System.Drawing.Size(99, 38);
            this.lbShowF.TabIndex = 26;
            this.lbShowF.Text = "Công thức:\r\nFormula:";
            // 
            // btnContinueStep
            // 
            this.btnContinueStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContinueStep.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnContinueStep.ButtonImage = global::mixer_control_globalver.Properties.Resources.cycle;
            this.btnContinueStep.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnContinueStep.ButtonText = "Refresh";
            this.btnContinueStep.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnContinueStep.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnContinueStep.CornerRadius = 10;
            this.btnContinueStep.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinueStep.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnContinueStep.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnContinueStep.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnContinueStep.ImagePosition = XanderUI.XUIButton.imgPosition.Center;
            this.btnContinueStep.Location = new System.Drawing.Point(415, 538);
            this.btnContinueStep.Name = "btnContinueStep";
            this.btnContinueStep.Size = new System.Drawing.Size(69, 58);
            this.btnContinueStep.TabIndex = 25;
            this.btnContinueStep.TextColor = System.Drawing.Color.Black;
            this.btnContinueStep.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnContinueStep.Click += new System.EventHandler(this.btnContinueStep_Click);
            // 
            // btnStartProcess
            // 
            this.btnStartProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartProcess.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnStartProcess.ButtonImage = global::mixer_control_globalver.Properties.Resources.right_arrow;
            this.btnStartProcess.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnStartProcess.ButtonText = "Start process";
            this.btnStartProcess.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnStartProcess.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnStartProcess.CornerRadius = 10;
            this.btnStartProcess.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartProcess.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnStartProcess.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnStartProcess.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnStartProcess.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnStartProcess.Location = new System.Drawing.Point(12, 529);
            this.btnStartProcess.Name = "btnStartProcess";
            this.btnStartProcess.Size = new System.Drawing.Size(297, 67);
            this.btnStartProcess.TabIndex = 19;
            this.btnStartProcess.TextColor = System.Drawing.Color.Black;
            this.btnStartProcess.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnStartProcess.Click += new System.EventHandler(this.btnStartProcess_Click);
            // 
            // btnActivateSpeedControl
            // 
            this.btnActivateSpeedControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActivateSpeedControl.BackColor = System.Drawing.Color.Yellow;
            this.btnActivateSpeedControl.Enabled = false;
            this.btnActivateSpeedControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivateSpeedControl.Location = new System.Drawing.Point(503, 538);
            this.btnActivateSpeedControl.Name = "btnActivateSpeedControl";
            this.btnActivateSpeedControl.Size = new System.Drawing.Size(244, 61);
            this.btnActivateSpeedControl.TabIndex = 24;
            this.btnActivateSpeedControl.Text = "Chế độ tự động đang bật\r\nAutomation mode ON";
            this.btnActivateSpeedControl.UseVisualStyleBackColor = false;
            // 
            // rtbRemark
            // 
            this.rtbRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbRemark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.rtbRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbRemark.Location = new System.Drawing.Point(12, 101);
            this.rtbRemark.Name = "rtbRemark";
            this.rtbRemark.ReadOnly = true;
            this.rtbRemark.Size = new System.Drawing.Size(321, 412);
            this.rtbRemark.TabIndex = 1;
            this.rtbRemark.Text = "";
            // 
            // btnResetRoll
            // 
            this.btnResetRoll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetRoll.BackColor = System.Drawing.Color.White;
            this.btnResetRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetRoll.Location = new System.Drawing.Point(561, 477);
            this.btnResetRoll.Name = "btnResetRoll";
            this.btnResetRoll.Size = new System.Drawing.Size(186, 55);
            this.btnResetRoll.TabIndex = 23;
            this.btnResetRoll.Text = "Ngừng Quay\r\nStop Motor";
            this.btnResetRoll.UseVisualStyleBackColor = false;
            this.btnResetRoll.Click += new System.EventHandler(this.btnResetRoll_Click);
            // 
            // lbProcessNo
            // 
            this.lbProcessNo.AutoSize = true;
            this.lbProcessNo.Location = new System.Drawing.Point(12, 56);
            this.lbProcessNo.Name = "lbProcessNo";
            this.lbProcessNo.Size = new System.Drawing.Size(148, 38);
            this.lbProcessNo.TabIndex = 0;
            this.lbProcessNo.Text = "Bắt đầu bước số:\r\nStart ...\r\n";
            // 
            // btnReverseRoll
            // 
            this.btnReverseRoll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReverseRoll.BackColor = System.Drawing.Color.White;
            this.btnReverseRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReverseRoll.Location = new System.Drawing.Point(561, 416);
            this.btnReverseRoll.Name = "btnReverseRoll";
            this.btnReverseRoll.Size = new System.Drawing.Size(186, 55);
            this.btnReverseRoll.TabIndex = 22;
            this.btnReverseRoll.Text = "Quay Ngược\r\nReverse Clockwise";
            this.btnReverseRoll.UseVisualStyleBackColor = false;
            this.btnReverseRoll.Click += new System.EventHandler(this.btnReverseRoll_Click);
            // 
            // btnNormalRoll
            // 
            this.btnNormalRoll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNormalRoll.BackColor = System.Drawing.Color.White;
            this.btnNormalRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNormalRoll.Location = new System.Drawing.Point(346, 416);
            this.btnNormalRoll.Name = "btnNormalRoll";
            this.btnNormalRoll.Size = new System.Drawing.Size(181, 55);
            this.btnNormalRoll.TabIndex = 21;
            this.btnNormalRoll.Text = "Quay Thuận\r\nClockwise";
            this.btnNormalRoll.UseVisualStyleBackColor = false;
            this.btnNormalRoll.Click += new System.EventHandler(this.btnNormalRoll_Click);
            // 
            // panelShowSpeed
            // 
            this.panelShowSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelShowSpeed.BackColor = System.Drawing.Color.Black;
            this.panelShowSpeed.Controls.Add(this.lb2);
            this.panelShowSpeed.Controls.Add(this.lbRollSpeed);
            this.panelShowSpeed.Controls.Add(this.lb1);
            this.panelShowSpeed.Location = new System.Drawing.Point(339, 12);
            this.panelShowSpeed.Name = "panelShowSpeed";
            this.panelShowSpeed.Size = new System.Drawing.Size(408, 98);
            this.panelShowSpeed.TabIndex = 2;
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.BackColor = System.Drawing.Color.Transparent;
            this.lb2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb2.ForeColor = System.Drawing.Color.White;
            this.lb2.Location = new System.Drawing.Point(3, 44);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(91, 38);
            this.lb2.TabIndex = 3;
            this.lb2.Text = "(vòng/phút)\r\n(rpm)\r\n";
            // 
            // lbRollSpeed
            // 
            this.lbRollSpeed.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbRollSpeed.AutoSize = true;
            this.lbRollSpeed.BackColor = System.Drawing.Color.Transparent;
            this.lbRollSpeed.Font = new System.Drawing.Font("Arial", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRollSpeed.ForeColor = System.Drawing.Color.White;
            this.lbRollSpeed.Location = new System.Drawing.Point(201, 27);
            this.lbRollSpeed.Name = "lbRollSpeed";
            this.lbRollSpeed.Size = new System.Drawing.Size(89, 55);
            this.lbRollSpeed.TabIndex = 2;
            this.lbRollSpeed.Text = "0.0";
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.BackColor = System.Drawing.Color.Transparent;
            this.lb1.ForeColor = System.Drawing.Color.White;
            this.lb1.Location = new System.Drawing.Point(3, 0);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(131, 38);
            this.lb1.TabIndex = 1;
            this.lb1.Text = "Tốc độ hiện tại:\r\nCurrent speed:\r\n";
            // 
            // panelShowTemperature
            // 
            this.panelShowTemperature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelShowTemperature.BackColor = System.Drawing.Color.Black;
            this.panelShowTemperature.Controls.Add(this.lb4);
            this.panelShowTemperature.Controls.Add(this.lbTemperature);
            this.panelShowTemperature.Controls.Add(this.lb3);
            this.panelShowTemperature.Location = new System.Drawing.Point(339, 116);
            this.panelShowTemperature.Name = "panelShowTemperature";
            this.panelShowTemperature.Size = new System.Drawing.Size(408, 98);
            this.panelShowTemperature.TabIndex = 4;
            // 
            // lb4
            // 
            this.lb4.AutoSize = true;
            this.lb4.BackColor = System.Drawing.Color.Transparent;
            this.lb4.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb4.ForeColor = System.Drawing.Color.White;
            this.lb4.Location = new System.Drawing.Point(3, 44);
            this.lb4.Name = "lb4";
            this.lb4.Size = new System.Drawing.Size(74, 38);
            this.lb4.TabIndex = 3;
            this.lb4.Text = "(Độ C)\r\n(Celsius)";
            // 
            // lbTemperature
            // 
            this.lbTemperature.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbTemperature.AutoSize = true;
            this.lbTemperature.BackColor = System.Drawing.Color.Transparent;
            this.lbTemperature.Font = new System.Drawing.Font("Arial", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTemperature.ForeColor = System.Drawing.Color.White;
            this.lbTemperature.Location = new System.Drawing.Point(201, 27);
            this.lbTemperature.Name = "lbTemperature";
            this.lbTemperature.Size = new System.Drawing.Size(89, 55);
            this.lbTemperature.TabIndex = 2;
            this.lbTemperature.Text = "0.0";
            // 
            // lb3
            // 
            this.lb3.AutoSize = true;
            this.lb3.BackColor = System.Drawing.Color.Transparent;
            this.lb3.ForeColor = System.Drawing.Color.White;
            this.lb3.Location = new System.Drawing.Point(3, 0);
            this.lb3.Name = "lb3";
            this.lb3.Size = new System.Drawing.Size(177, 38);
            this.lb3.TabIndex = 1;
            this.lb3.Text = "Nhiệt độ hiện tại:\r\nCurrent temperature:";
            // 
            // lbCountDown
            // 
            this.lbCountDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCountDown.AutoSize = true;
            this.lbCountDown.BackColor = System.Drawing.Color.Transparent;
            this.lbCountDown.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCountDown.ForeColor = System.Drawing.Color.Black;
            this.lbCountDown.Location = new System.Drawing.Point(417, 290);
            this.lbCountDown.Name = "lbCountDown";
            this.lbCountDown.Size = new System.Drawing.Size(268, 70);
            this.lbCountDown.TabIndex = 4;
            this.lbCountDown.Text = "00:00:00";
            // 
            // lb5
            // 
            this.lb5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb5.AutoSize = true;
            this.lb5.Location = new System.Drawing.Point(342, 227);
            this.lb5.Name = "lb5";
            this.lb5.Size = new System.Drawing.Size(324, 38);
            this.lb5.TabIndex = 5;
            this.lb5.Text = "Thời gian còn lại đến khi kết thúc bước:\r\nTime left untill process ended:\r\n";
            // 
            // AutomationInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(759, 608);
            this.Controls.Add(this.panelAutomationInfoMain);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AutomationInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "AutomationInfo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutomationInfo_FormClosing);
            this.Load += new System.EventHandler(this.AutomationInfo_Load);
            this.panelAutomationInfoMain.ResumeLayout(false);
            this.panelAutomationInfoMain.PerformLayout();
            this.panelShowSpeed.ResumeLayout(false);
            this.panelShowSpeed.PerformLayout();
            this.panelShowTemperature.ResumeLayout(false);
            this.panelShowTemperature.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelAutomationInfoMain;
        private System.Windows.Forms.Label lbProcessNo;
        private System.Windows.Forms.RichTextBox rtbRemark;
        private XanderUI.XUIButton btnStartProcess;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Panel panelShowSpeed;
        private System.Windows.Forms.Label lbRollSpeed;
        private System.Windows.Forms.Label lb2;
        private System.Windows.Forms.Panel panelShowTemperature;
        private System.Windows.Forms.Label lb4;
        private System.Windows.Forms.Label lbTemperature;
        private System.Windows.Forms.Label lb3;
        private System.Windows.Forms.Label lb5;
        private System.Windows.Forms.Label lbCountDown;
        private System.Windows.Forms.Button btnActivateSpeedControl;
        private System.Windows.Forms.Button btnResetRoll;
        private System.Windows.Forms.Button btnReverseRoll;
        private System.Windows.Forms.Button btnNormalRoll;
        private XanderUI.XUIButton btnContinueStep;
        private System.Windows.Forms.Label lbShowF;
        private System.Windows.Forms.Label lbFormulaName;
    }
}