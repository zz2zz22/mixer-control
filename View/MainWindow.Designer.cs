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
            this.btnMinimize = new XanderUI.XUIButton();
            this.btnMaximize = new XanderUI.XUIButton();
            this.btnClose = new XanderUI.XUIButton();
            this.lbHeader = new System.Windows.Forms.Label();
            this.pbxCompanyLogo = new System.Windows.Forms.PictureBox();
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.panelBtnAutomation = new System.Windows.Forms.Panel();
            this.btnAutomationTab = new XanderUI.XUIButton();
            this.panelBtnWeight = new System.Windows.Forms.Panel();
            this.btnWeightTab = new XanderUI.XUIButton();
            this.panelBtnChoose = new System.Windows.Forms.Panel();
            this.btnChooseSpecTab = new XanderUI.XUIButton();
            this.panelMainForm = new XanderUI.XUIWidgetPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCompanyLogo)).BeginInit();
            this.panelSideMenu.SuspendLayout();
            this.panelBtnAutomation.SuspendLayout();
            this.panelBtnWeight.SuspendLayout();
            this.panelBtnChoose.SuspendLayout();
            this.panelMainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.btnSetting);
            this.panelHeader.Controls.Add(this.btnHelp);
            this.panelHeader.Controls.Add(this.btnMinimize);
            this.panelHeader.Controls.Add(this.btnMaximize);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.lbHeader);
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
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
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
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.TextColor = System.Drawing.Color.DodgerBlue;
            this.btnHelp.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnMinimize
            // 
            resources.ApplyResources(this.btnMinimize, "btnMinimize");
            this.btnMinimize.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnMinimize.ButtonImage = global::mixer_control_globalver.Properties.Resources.subtraction;
            this.btnMinimize.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnMinimize.ButtonText = "Button";
            this.btnMinimize.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnMinimize.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnMinimize.CornerRadius = 5;
            this.btnMinimize.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnMinimize.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnMinimize.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnMinimize.ImagePosition = XanderUI.XUIButton.imgPosition.Center;
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.TextColor = System.Drawing.Color.DodgerBlue;
            this.btnMinimize.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
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
            // lbHeader
            // 
            resources.ApplyResources(this.lbHeader, "lbHeader");
            this.lbHeader.Name = "lbHeader";
            // 
            // pbxCompanyLogo
            // 
            this.pbxCompanyLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            resources.ApplyResources(this.pbxCompanyLogo, "pbxCompanyLogo");
            this.pbxCompanyLogo.Image = global::mixer_control_globalver.Properties.Resources.logoTechlinkFix;
            this.pbxCompanyLogo.Name = "pbxCompanyLogo";
            this.pbxCompanyLogo.TabStop = false;
            this.pbxCompanyLogo.Click += new System.EventHandler(this.pbxCompanyLogo_Click);
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelSideMenu.Controls.Add(this.panelBtnAutomation);
            this.panelSideMenu.Controls.Add(this.panelBtnWeight);
            this.panelSideMenu.Controls.Add(this.panelBtnChoose);
            resources.ApplyResources(this.panelSideMenu, "panelSideMenu");
            this.panelSideMenu.Name = "panelSideMenu";
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
            this.btnAutomationTab.ButtonText = "Automation";
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
            this.btnWeightTab.ButtonText = "Scale";
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
            this.btnChooseSpecTab.ButtonText = "Choose Specification";
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
            // 
            // panelMainForm
            // 
            this.panelMainForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelMainForm.Controls.Add(this.label3);
            this.panelMainForm.Controls.Add(this.label2);
            this.panelMainForm.Controls.Add(this.label1);
            this.panelMainForm.ControlsAsWidgets = false;
            resources.ApplyResources(this.panelMainForm, "panelMainForm");
            this.panelMainForm.Name = "panelMainForm";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMainForm);
            this.Controls.Add(this.panelSideMenu);
            this.Controls.Add(this.panelHeader);
            this.Name = "MainWindow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCompanyLogo)).EndInit();
            this.panelSideMenu.ResumeLayout(false);
            this.panelBtnAutomation.ResumeLayout(false);
            this.panelBtnWeight.ResumeLayout(false);
            this.panelBtnChoose.ResumeLayout(false);
            this.panelMainForm.ResumeLayout(false);
            this.panelMainForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lbHeader;
        private System.Windows.Forms.PictureBox pbxCompanyLogo;
        private System.Windows.Forms.Panel panelSideMenu;
        private XanderUI.XUIButton btnClose;
        private XanderUI.XUIButton btnMinimize;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

