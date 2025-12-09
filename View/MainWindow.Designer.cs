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
            this.btnMaximize = new XanderUI.XUIButton();
            this.btnClose = new XanderUI.XUIButton();
            this.pbxCompanyLogo = new System.Windows.Forms.PictureBox();
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.lbOilTestStatus = new System.Windows.Forms.Label();
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
            this.panelHeader.Controls.Add(this.btnMaximize);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.pbxCompanyLogo);
            resources.ApplyResources(this.panelHeader, "panelHeader");
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHeader_MouseDown);
            // 
            // btnSetting
            // 
            resources.ApplyResources(this.btnSetting, "btnSetting");
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
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.TextColor = System.Drawing.Color.DodgerBlue;
            this.btnSetting.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnMaximize
            // 
            resources.ApplyResources(this.btnMaximize, "btnMaximize");
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
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.TextColor = System.Drawing.Color.DodgerBlue;
            this.btnMaximize.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
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
            this.btnClose.Name = "btnClose";
            this.btnClose.TextColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbxCompanyLogo
            // 
            this.pbxCompanyLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            resources.ApplyResources(this.pbxCompanyLogo, "pbxCompanyLogo");
            this.pbxCompanyLogo.Image = global::mixer_control_globalver.Properties.Resources.logoTechlinkFix;
            this.pbxCompanyLogo.Name = "pbxCompanyLogo";
            this.pbxCompanyLogo.TabStop = false;
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelSideMenu.Controls.Add(this.lbOilTestStatus);
            this.panelSideMenu.Controls.Add(this.lbVersion);
            this.panelSideMenu.Controls.Add(this.cbxLanguageChoose);
            this.panelSideMenu.Controls.Add(this.panelBtnAutomation);
            this.panelSideMenu.Controls.Add(this.panelBtnWeight);
            this.panelSideMenu.Controls.Add(this.panelBtnChoose);
            resources.ApplyResources(this.panelSideMenu, "panelSideMenu");
            this.panelSideMenu.Name = "panelSideMenu";
            // 
            // lbOilTestStatus
            // 
            resources.ApplyResources(this.lbOilTestStatus, "lbOilTestStatus");
            this.lbOilTestStatus.Name = "lbOilTestStatus";
            // 
            // lbVersion
            // 
            resources.ApplyResources(this.lbVersion, "lbVersion");
            this.lbVersion.Name = "lbVersion";
            // 
            // cbxLanguageChoose
            // 
            resources.ApplyResources(this.cbxLanguageChoose, "cbxLanguageChoose");
            this.cbxLanguageChoose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLanguageChoose.FormattingEnabled = true;
            this.cbxLanguageChoose.Items.AddRange(new object[] {
            resources.GetString("cbxLanguageChoose.Items"),
            resources.GetString("cbxLanguageChoose.Items1"),
            resources.GetString("cbxLanguageChoose.Items2")});
            this.cbxLanguageChoose.Name = "cbxLanguageChoose";
            this.cbxLanguageChoose.SelectionChangeCommitted += new System.EventHandler(this.cbxLanguageChoose_SelectionChangeCommitted);
            // 
            // panelBtnAutomation
            // 
            this.panelBtnAutomation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBtnAutomation.Controls.Add(this.btnAutomationTab);
            resources.ApplyResources(this.panelBtnAutomation, "panelBtnAutomation");
            this.panelBtnAutomation.Name = "panelBtnAutomation";
            // 
            // btnAutomationTab
            // 
            this.btnAutomationTab.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAutomationTab.ButtonImage = global::mixer_control_globalver.Properties.Resources.automation;
            this.btnAutomationTab.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnAutomationTab.ButtonText = "...";
            this.btnAutomationTab.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnAutomationTab.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnAutomationTab.CornerRadius = 10;
            resources.ApplyResources(this.btnAutomationTab, "btnAutomationTab");
            this.btnAutomationTab.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnAutomationTab.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnAutomationTab.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnAutomationTab.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnAutomationTab.Name = "btnAutomationTab";
            this.btnAutomationTab.TextColor = System.Drawing.Color.Black;
            this.btnAutomationTab.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnAutomationTab.Click += new System.EventHandler(this.btnAutomationTab_Click);
            // 
            // panelBtnWeight
            // 
            this.panelBtnWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBtnWeight.Controls.Add(this.btnWeightTab);
            resources.ApplyResources(this.panelBtnWeight, "panelBtnWeight");
            this.panelBtnWeight.Name = "panelBtnWeight";
            // 
            // btnWeightTab
            // 
            this.btnWeightTab.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnWeightTab.ButtonImage = global::mixer_control_globalver.Properties.Resources.weighing_scale;
            this.btnWeightTab.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnWeightTab.ButtonText = "...";
            this.btnWeightTab.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnWeightTab.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnWeightTab.CornerRadius = 10;
            resources.ApplyResources(this.btnWeightTab, "btnWeightTab");
            this.btnWeightTab.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnWeightTab.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnWeightTab.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnWeightTab.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnWeightTab.Name = "btnWeightTab";
            this.btnWeightTab.TextColor = System.Drawing.Color.Black;
            this.btnWeightTab.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnWeightTab.Click += new System.EventHandler(this.btnWeightTab_Click);
            // 
            // panelBtnChoose
            // 
            this.panelBtnChoose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBtnChoose.Controls.Add(this.btnChooseSpecTab);
            resources.ApplyResources(this.panelBtnChoose, "panelBtnChoose");
            this.panelBtnChoose.Name = "panelBtnChoose";
            // 
            // btnChooseSpecTab
            // 
            this.btnChooseSpecTab.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnChooseSpecTab.ButtonImage = global::mixer_control_globalver.Properties.Resources.choose;
            this.btnChooseSpecTab.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnChooseSpecTab.ButtonText = "...";
            this.btnChooseSpecTab.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnChooseSpecTab.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnChooseSpecTab.CornerRadius = 10;
            resources.ApplyResources(this.btnChooseSpecTab, "btnChooseSpecTab");
            this.btnChooseSpecTab.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnChooseSpecTab.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnChooseSpecTab.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnChooseSpecTab.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnChooseSpecTab.Name = "btnChooseSpecTab";
            this.btnChooseSpecTab.TextColor = System.Drawing.Color.Black;
            this.btnChooseSpecTab.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnChooseSpecTab.Click += new System.EventHandler(this.btnChooseSpecTab_Click);
            // 
            // panelMainForm
            // 
            this.panelMainForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelMainForm.ControlsAsWidgets = false;
            resources.ApplyResources(this.panelMainForm, "panelMainForm");
            this.panelMainForm.Name = "panelMainForm";
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panelMainForm);
            this.Controls.Add(this.panelSideMenu);
            this.Controls.Add(this.panelHeader);
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
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
        private System.Windows.Forms.Label lbOilTestStatus;
    }
}

