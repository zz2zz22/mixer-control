namespace mixer_control_globalver
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnSetting = new XanderUI.XUIButton();
            this.btnHelp = new XanderUI.XUIButton();
            this.btnMaximize = new XanderUI.XUIButton();
            this.btnClose = new XanderUI.XUIButton();
            this.pbxCompanyLogo = new System.Windows.Forms.PictureBox();
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.lbVersion = new System.Windows.Forms.Label();
            this.cbxLanguageChoose = new System.Windows.Forms.ComboBox();
            this.panelBtnAutomation = new System.Windows.Forms.Panel();
            this.btnAutomationTab = new XanderUI.XUIButton();
            this.panelBtnWeight = new System.Windows.Forms.Panel();
            this.btnWeightTab = new XanderUI.XUIButton();
            this.panelBtnChoose = new System.Windows.Forms.Panel();
            this.btnChooseSpecTab = new XanderUI.XUIButton();
            this.panelMainForm = new XanderUI.XUIWidgetPanel();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCompanyLogo)).BeginInit();
            this.panelSideMenu.SuspendLayout();
            this.panelBtnAutomation.SuspendLayout();
            this.panelBtnWeight.SuspendLayout();
            this.panelBtnChoose.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.btnSetting);
            this.panelHeader.Controls.Add(this.btnHelp);
            this.panelHeader.Controls.Add(this.btnMaximize);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.pbxCompanyLogo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1006, 65);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHeader_MouseDown);
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetting.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSetting.ButtonImage = global::mixer_control_globalver.Properties.Resources.control;
            this.btnSetting.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnSetting.ButtonText = "Button";
            this.btnSetting.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnSetting.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnSetting.CornerRadius = 5;
            this.btnSetting.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnSetting.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnSetting.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnSetting.ImagePosition = XanderUI.XUIButton.imgPosition.Center;
            this.btnSetting.Location = new System.Drawing.Point(698, 4);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(56, 56);
            this.btnSetting.TabIndex = 6;
            this.btnSetting.TextColor = System.Drawing.Color.DodgerBlue;
            this.btnSetting.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnHelp.ButtonImage = global::mixer_control_globalver.Properties.Resources.question;
            this.btnHelp.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnHelp.ButtonText = "Button";
            this.btnHelp.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnHelp.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnHelp.CornerRadius = 5;
            this.btnHelp.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnHelp.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnHelp.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnHelp.ImagePosition = XanderUI.XUIButton.imgPosition.Center;
            this.btnHelp.Location = new System.Drawing.Point(637, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(56, 56);
            this.btnHelp.TabIndex = 5;
            this.btnHelp.TextColor = System.Drawing.Color.DodgerBlue;
            this.btnHelp.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnMaximize.ButtonImage = global::mixer_control_globalver.Properties.Resources.maximize;
            this.btnMaximize.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnMaximize.ButtonText = "Button";
            this.btnMaximize.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnMaximize.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnMaximize.CornerRadius = 5;
            this.btnMaximize.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnMaximize.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnMaximize.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnMaximize.ImagePosition = XanderUI.XUIButton.imgPosition.Center;
            this.btnMaximize.Location = new System.Drawing.Point(885, 3);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(56, 56);
            this.btnMaximize.TabIndex = 3;
            this.btnMaximize.TextColor = System.Drawing.Color.DodgerBlue;
            this.btnMaximize.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
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
            this.btnClose.Location = new System.Drawing.Point(946, 3);
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
            this.pbxCompanyLogo.Click += new System.EventHandler(this.pbxCompanyLogo_Click);
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelSideMenu.Controls.Add(this.lbVersion);
            this.panelSideMenu.Controls.Add(this.cbxLanguageChoose);
            this.panelSideMenu.Controls.Add(this.panelBtnAutomation);
            this.panelSideMenu.Controls.Add(this.panelBtnWeight);
            this.panelSideMenu.Controls.Add(this.panelBtnChoose);
            this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSideMenu.Location = new System.Drawing.Point(0, 65);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(247, 608);
            this.panelSideMenu.TabIndex = 1;
            // 
            // lbVersion
            // 
            this.lbVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(12, 580);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(135, 19);
            this.lbVersion.TabIndex = 4;
            this.lbVersion.Text = "Version 0.1 beta";
            // 
            // cbxLanguageChoose
            // 
            this.cbxLanguageChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxLanguageChoose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLanguageChoose.FormattingEnabled = true;
            this.cbxLanguageChoose.Items.AddRange(new object[] {
            "Vietnamese - English",
            "越南语 - 中文"});
            this.cbxLanguageChoose.Location = new System.Drawing.Point(12, 534);
            this.cbxLanguageChoose.Name = "cbxLanguageChoose";
            this.cbxLanguageChoose.Size = new System.Drawing.Size(229, 27);
            this.cbxLanguageChoose.TabIndex = 3;
            this.cbxLanguageChoose.SelectionChangeCommitted += new System.EventHandler(this.cbxLanguageChoose_SelectionChangeCommitted);
            // 
            // panelBtnAutomation
            // 
            this.panelBtnAutomation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBtnAutomation.Controls.Add(this.btnAutomationTab);
            this.panelBtnAutomation.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBtnAutomation.Location = new System.Drawing.Point(0, 168);
            this.panelBtnAutomation.Name = "panelBtnAutomation";
            this.panelBtnAutomation.Size = new System.Drawing.Size(247, 84);
            this.panelBtnAutomation.TabIndex = 2;
            // 
            // btnAutomationTab
            // 
            this.btnAutomationTab.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAutomationTab.ButtonImage = global::mixer_control_globalver.Properties.Resources.automation;
            this.btnAutomationTab.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnAutomationTab.ButtonText = "Automation";
            this.btnAutomationTab.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnAutomationTab.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnAutomationTab.CornerRadius = 10;
            this.btnAutomationTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAutomationTab.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutomationTab.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnAutomationTab.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnAutomationTab.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnAutomationTab.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnAutomationTab.Location = new System.Drawing.Point(0, 0);
            this.btnAutomationTab.Name = "btnAutomationTab";
            this.btnAutomationTab.Size = new System.Drawing.Size(245, 82);
            this.btnAutomationTab.TabIndex = 1;
            this.btnAutomationTab.TextColor = System.Drawing.Color.Black;
            this.btnAutomationTab.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnAutomationTab.Click += new System.EventHandler(this.btnAutomationTab_Click);
            // 
            // panelBtnWeight
            // 
            this.panelBtnWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBtnWeight.Controls.Add(this.btnWeightTab);
            this.panelBtnWeight.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBtnWeight.Location = new System.Drawing.Point(0, 84);
            this.panelBtnWeight.Name = "panelBtnWeight";
            this.panelBtnWeight.Size = new System.Drawing.Size(247, 84);
            this.panelBtnWeight.TabIndex = 1;
            // 
            // btnWeightTab
            // 
            this.btnWeightTab.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnWeightTab.ButtonImage = global::mixer_control_globalver.Properties.Resources.weighing_scale;
            this.btnWeightTab.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnWeightTab.ButtonText = "Confirmation";
            this.btnWeightTab.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnWeightTab.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnWeightTab.CornerRadius = 10;
            this.btnWeightTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWeightTab.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeightTab.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnWeightTab.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnWeightTab.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnWeightTab.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnWeightTab.Location = new System.Drawing.Point(0, 0);
            this.btnWeightTab.Name = "btnWeightTab";
            this.btnWeightTab.Size = new System.Drawing.Size(245, 82);
            this.btnWeightTab.TabIndex = 1;
            this.btnWeightTab.TextColor = System.Drawing.Color.Black;
            this.btnWeightTab.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnWeightTab.Click += new System.EventHandler(this.btnWeightTab_Click);
            // 
            // panelBtnChoose
            // 
            this.panelBtnChoose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBtnChoose.Controls.Add(this.btnChooseSpecTab);
            this.panelBtnChoose.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBtnChoose.Location = new System.Drawing.Point(0, 0);
            this.panelBtnChoose.Name = "panelBtnChoose";
            this.panelBtnChoose.Size = new System.Drawing.Size(247, 84);
            this.panelBtnChoose.TabIndex = 0;
            // 
            // btnChooseSpecTab
            // 
            this.btnChooseSpecTab.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnChooseSpecTab.ButtonImage = global::mixer_control_globalver.Properties.Resources.choose;
            this.btnChooseSpecTab.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnChooseSpecTab.ButtonText = "Choose Specification";
            this.btnChooseSpecTab.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnChooseSpecTab.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnChooseSpecTab.CornerRadius = 10;
            this.btnChooseSpecTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChooseSpecTab.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseSpecTab.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnChooseSpecTab.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnChooseSpecTab.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnChooseSpecTab.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnChooseSpecTab.Location = new System.Drawing.Point(0, 0);
            this.btnChooseSpecTab.Name = "btnChooseSpecTab";
            this.btnChooseSpecTab.Size = new System.Drawing.Size(245, 82);
            this.btnChooseSpecTab.TabIndex = 0;
            this.btnChooseSpecTab.TextColor = System.Drawing.Color.Black;
            this.btnChooseSpecTab.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnChooseSpecTab.Click += new System.EventHandler(this.btnChooseSpecTab_Click);
            // 
            // panelMainForm
            // 
            this.panelMainForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelMainForm.ControlsAsWidgets = false;
            this.panelMainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainForm.Location = new System.Drawing.Point(247, 65);
            this.panelMainForm.Name = "panelMainForm";
            this.panelMainForm.Size = new System.Drawing.Size(759, 608);
            this.panelMainForm.TabIndex = 2;
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1006, 673);
            this.Controls.Add(this.panelMainForm);
            this.Controls.Add(this.panelSideMenu);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 720);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main for";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxCompanyLogo)).EndInit();
            this.panelSideMenu.ResumeLayout(false);
            this.panelSideMenu.PerformLayout();
            this.panelBtnAutomation.ResumeLayout(false);
            this.panelBtnWeight.ResumeLayout(false);
            this.panelBtnChoose.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.PictureBox pbxCompanyLogo;
        private System.Windows.Forms.Panel panelSideMenu;
        private XanderUI.XUIButton btnClose;
        private XanderUI.XUIButton btnMaximize;
        private XanderUI.XUIButton btnHelp;
        private XanderUI.XUIButton btnChooseSpecTab;
        private XanderUI.XUIWidgetPanel panelMainForm;
        private System.Windows.Forms.Panel panelBtnChoose;
        private System.Windows.Forms.Panel panelBtnAutomation;
        private XanderUI.XUIButton btnAutomationTab;
        private System.Windows.Forms.Panel panelBtnWeight;
        private XanderUI.XUIButton btnWeightTab;
        private XanderUI.XUIButton btnSetting;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.ComboBox cbxLanguageChoose;
    }
}

