namespace mixer_control_globalver.View.SideUI
{
    partial class OilFeederTest
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
            this.components = new System.ComponentModel.Container();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnClose = new XanderUI.XUIButton();
            this.pbxCompanyLogo = new System.Windows.Forms.PictureBox();
            this.panelTestOil = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.txbTestMass = new System.Windows.Forms.TextBox();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btnStartTesting = new XanderUI.XUIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCompanyLogo)).BeginInit();
            this.panelTestOil.SuspendLayout();
            this.panelStatus.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(556, 52);
            this.panelHeader.TabIndex = 26;
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
            this.btnClose.Location = new System.Drawing.Point(503, 3);
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
            this.pbxCompanyLogo.Size = new System.Drawing.Size(185, 50);
            this.pbxCompanyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxCompanyLogo.TabIndex = 0;
            this.pbxCompanyLogo.TabStop = false;
            // 
            // panelTestOil
            // 
            this.panelTestOil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelTestOil.Controls.Add(this.label2);
            this.panelTestOil.Controls.Add(this.rtb_log);
            this.panelTestOil.Controls.Add(this.txbTestMass);
            this.panelTestOil.Controls.Add(this.panelStatus);
            this.panelTestOil.Controls.Add(this.btnStartTesting);
            this.panelTestOil.Controls.Add(this.label1);
            this.panelTestOil.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTestOil.Location = new System.Drawing.Point(0, 52);
            this.panelTestOil.Name = "panelTestOil";
            this.panelTestOil.Size = new System.Drawing.Size(556, 362);
            this.panelTestOil.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 23);
            this.label2.TabIndex = 21;
            this.label2.Text = "Log section:\r\n";
            // 
            // rtb_log
            // 
            this.rtb_log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.rtb_log.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtb_log.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_log.Location = new System.Drawing.Point(0, 193);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtb_log.Size = new System.Drawing.Size(556, 169);
            this.rtb_log.TabIndex = 20;
            this.rtb_log.Text = "";
            this.rtb_log.TextChanged += new System.EventHandler(this.rtb_log_TextChanged);
            // 
            // txbTestMass
            // 
            this.txbTestMass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txbTestMass.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbTestMass.Location = new System.Drawing.Point(16, 40);
            this.txbTestMass.Name = "txbTestMass";
            this.txbTestMass.Size = new System.Drawing.Size(200, 47);
            this.txbTestMass.TabIndex = 19;
            this.txbTestMass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txbTestMass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbTestMass_KeyPress);
            // 
            // panelStatus
            // 
            this.panelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStatus.Controls.Add(this.lbStatus);
            this.panelStatus.Location = new System.Drawing.Point(236, 40);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(308, 116);
            this.panelStatus.TabIndex = 18;
            // 
            // lbStatus
            // 
            this.lbStatus.BackColor = System.Drawing.Color.Silver;
            this.lbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbStatus.Font = new System.Drawing.Font("Microsoft YaHei", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.ForeColor = System.Drawing.Color.Red;
            this.lbStatus.Location = new System.Drawing.Point(0, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(306, 114);
            this.lbStatus.TabIndex = 0;
            this.lbStatus.Text = "...";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStartTesting
            // 
            this.btnStartTesting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartTesting.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnStartTesting.ButtonImage = global::mixer_control_globalver.Properties.Resources.confirmation;
            this.btnStartTesting.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnStartTesting.ButtonText = "Start Testing";
            this.btnStartTesting.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnStartTesting.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnStartTesting.CornerRadius = 10;
            this.btnStartTesting.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartTesting.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnStartTesting.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnStartTesting.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnStartTesting.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnStartTesting.Location = new System.Drawing.Point(16, 93);
            this.btnStartTesting.Name = "btnStartTesting";
            this.btnStartTesting.Size = new System.Drawing.Size(200, 71);
            this.btnStartTesting.TabIndex = 17;
            this.btnStartTesting.TextColor = System.Drawing.Color.Black;
            this.btnStartTesting.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnStartTesting.Click += new System.EventHandler(this.btnStartTesting_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(326, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhập khối lượng dầu muốn thử nghiệm:\r\n";
            // 
            // OilFeederTest
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(556, 414);
            this.Controls.Add(this.panelTestOil);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OilFeederTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OilFeederTest";
            this.Load += new System.EventHandler(this.OilFeederTest_Load);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxCompanyLogo)).EndInit();
            this.panelTestOil.ResumeLayout(false);
            this.panelTestOil.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private XanderUI.XUIButton btnClose;
        private System.Windows.Forms.PictureBox pbxCompanyLogo;
        private System.Windows.Forms.Panel panelTestOil;
        private System.Windows.Forms.Label label1;
        private XanderUI.XUIButton btnStartTesting;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lbStatus;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox txbTestMass;
        private System.Windows.Forms.RichTextBox rtb_log;
        private System.Windows.Forms.Label label2;
    }
}