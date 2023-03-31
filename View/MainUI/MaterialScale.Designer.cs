namespace mixer_control_globalver.View.MainUI
{
    partial class MaterialScale
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
            this.panelMaterialScaleMain = new System.Windows.Forms.Panel();
            this.lbMaterialName = new System.Windows.Forms.Label();
            this.lb2 = new System.Windows.Forms.Label();
            this.btnProceedAutomation = new XanderUI.XUIButton();
            this.btnConfirm = new XanderUI.XUIButton();
            this.flpMaterialList = new System.Windows.Forms.FlowLayoutPanel();
            this.lb1 = new System.Windows.Forms.Label();
            this.panelMaterialScaleMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMaterialScaleMain
            // 
            this.panelMaterialScaleMain.Controls.Add(this.lbMaterialName);
            this.panelMaterialScaleMain.Controls.Add(this.lb2);
            this.panelMaterialScaleMain.Controls.Add(this.btnProceedAutomation);
            this.panelMaterialScaleMain.Controls.Add(this.btnConfirm);
            this.panelMaterialScaleMain.Controls.Add(this.flpMaterialList);
            this.panelMaterialScaleMain.Controls.Add(this.lb1);
            this.panelMaterialScaleMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaterialScaleMain.Location = new System.Drawing.Point(0, 0);
            this.panelMaterialScaleMain.Name = "panelMaterialScaleMain";
            this.panelMaterialScaleMain.Size = new System.Drawing.Size(759, 608);
            this.panelMaterialScaleMain.TabIndex = 1;
            // 
            // lbMaterialName
            // 
            this.lbMaterialName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMaterialName.AutoSize = true;
            this.lbMaterialName.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMaterialName.Location = new System.Drawing.Point(518, 168);
            this.lbMaterialName.Name = "lbMaterialName";
            this.lbMaterialName.Size = new System.Drawing.Size(24, 19);
            this.lbMaterialName.TabIndex = 19;
            this.lbMaterialName.Text = "...";
            this.lbMaterialName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb2
            // 
            this.lb2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb2.AutoSize = true;
            this.lb2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb2.Location = new System.Drawing.Point(444, 97);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(130, 38);
            this.lb2.TabIndex = 18;
            this.lb2.Text = "Tên nguyên liệu:\r\nMaterial name:";
            // 
            // btnProceedAutomation
            // 
            this.btnProceedAutomation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProceedAutomation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnProceedAutomation.ButtonImage = global::mixer_control_globalver.Properties.Resources.right_arrow;
            this.btnProceedAutomation.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnProceedAutomation.ButtonText = "Proceed automation";
            this.btnProceedAutomation.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnProceedAutomation.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnProceedAutomation.CornerRadius = 10;
            this.btnProceedAutomation.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProceedAutomation.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnProceedAutomation.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnProceedAutomation.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnProceedAutomation.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnProceedAutomation.Location = new System.Drawing.Point(448, 529);
            this.btnProceedAutomation.Name = "btnProceedAutomation";
            this.btnProceedAutomation.Size = new System.Drawing.Size(299, 67);
            this.btnProceedAutomation.TabIndex = 17;
            this.btnProceedAutomation.TextColor = System.Drawing.Color.Black;
            this.btnProceedAutomation.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnProceedAutomation.Click += new System.EventHandler(this.btnProceedAutomation_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnConfirm.ButtonImage = global::mixer_control_globalver.Properties.Resources.confirmation;
            this.btnConfirm.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnConfirm.ButtonText = "Save";
            this.btnConfirm.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnConfirm.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnConfirm.CornerRadius = 10;
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnConfirm.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnConfirm.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnConfirm.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnConfirm.Location = new System.Drawing.Point(448, 255);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(299, 95);
            this.btnConfirm.TabIndex = 15;
            this.btnConfirm.TextColor = System.Drawing.Color.Black;
            this.btnConfirm.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnConfirm.Click += new System.EventHandler(this.btnSaveWeight_Click);
            // 
            // flpMaterialList
            // 
            this.flpMaterialList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpMaterialList.AutoScroll = true;
            this.flpMaterialList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.flpMaterialList.Location = new System.Drawing.Point(12, 50);
            this.flpMaterialList.Name = "flpMaterialList";
            this.flpMaterialList.Size = new System.Drawing.Size(408, 546);
            this.flpMaterialList.TabIndex = 1;
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb1.Location = new System.Drawing.Point(12, 9);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(310, 38);
            this.lb1.TabIndex = 0;
            this.lb1.Text = "Danh sách nguyên vật liệu cần xác nhận:\r\nConfirmation required materials list:";
            // 
            // MaterialScale
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(759, 608);
            this.Controls.Add(this.panelMaterialScaleMain);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MaterialScale";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "MaterialScale";
            this.Load += new System.EventHandler(this.MaterialScale_Load);
            this.panelMaterialScaleMain.ResumeLayout(false);
            this.panelMaterialScaleMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMaterialScaleMain;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.FlowLayoutPanel flpMaterialList;
        private XanderUI.XUIButton btnConfirm;
        private XanderUI.XUIButton btnProceedAutomation;
        private System.Windows.Forms.Label lb2;
        private System.Windows.Forms.Label lbMaterialName;
    }
}