namespace mixer_control_globalver.View.CustomComponent
{
    partial class LoadingDialog
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
            this.panelProgressInfo = new System.Windows.Forms.Panel();
            this.lbPercentage = new System.Windows.Forms.Label();
            this.lbAnnounce = new System.Windows.Forms.Label();
            this.lb1 = new System.Windows.Forms.Label();
            this.panelProgressBar = new System.Windows.Forms.Panel();
            this.pgbProcess = new XanderUI.XUIFlatProgressBar();
            this.ptbxLoadingAnimated = new System.Windows.Forms.PictureBox();
            this.panelProgressInfo.SuspendLayout();
            this.panelProgressBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxLoadingAnimated)).BeginInit();
            this.SuspendLayout();
            // 
            // panelProgressInfo
            // 
            this.panelProgressInfo.BackColor = System.Drawing.Color.White;
            this.panelProgressInfo.Controls.Add(this.lbPercentage);
            this.panelProgressInfo.Controls.Add(this.lbAnnounce);
            this.panelProgressInfo.Controls.Add(this.lb1);
            this.panelProgressInfo.Controls.Add(this.panelProgressBar);
            this.panelProgressInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProgressInfo.Location = new System.Drawing.Point(263, 0);
            this.panelProgressInfo.Name = "panelProgressInfo";
            this.panelProgressInfo.Size = new System.Drawing.Size(498, 287);
            this.panelProgressInfo.TabIndex = 1;
            // 
            // lbPercentage
            // 
            this.lbPercentage.AutoSize = true;
            this.lbPercentage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPercentage.Location = new System.Drawing.Point(230, 115);
            this.lbPercentage.Name = "lbPercentage";
            this.lbPercentage.Size = new System.Drawing.Size(38, 24);
            this.lbPercentage.TabIndex = 4;
            this.lbPercentage.Text = "0%";
            this.lbPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAnnounce
            // 
            this.lbAnnounce.AutoSize = true;
            this.lbAnnounce.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAnnounce.Location = new System.Drawing.Point(7, 166);
            this.lbAnnounce.Name = "lbAnnounce";
            this.lbAnnounce.Size = new System.Drawing.Size(74, 19);
            this.lbAnnounce.TabIndex = 3;
            this.lbAnnounce.Text = "Annouce";
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Location = new System.Drawing.Point(7, 13);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(243, 38);
            this.lb1.TabIndex = 2;
            this.lb1.Text = "Đang thực hiện, vui lòng chờ.\r\nProcessing, please wait.";
            // 
            // panelProgressBar
            // 
            this.panelProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProgressBar.BackColor = System.Drawing.Color.Black;
            this.panelProgressBar.Controls.Add(this.pgbProcess);
            this.panelProgressBar.Location = new System.Drawing.Point(6, 61);
            this.panelProgressBar.Name = "panelProgressBar";
            this.panelProgressBar.Size = new System.Drawing.Size(489, 51);
            this.panelProgressBar.TabIndex = 1;
            // 
            // pgbProcess
            // 
            this.pgbProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgbProcess.BarStyle = XanderUI.XUIFlatProgressBar.Style.Material;
            this.pgbProcess.BarThickness = 5;
            this.pgbProcess.CompleteColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(119)))), ((int)(((byte)(215)))));
            this.pgbProcess.InocmpletedColor = System.Drawing.Color.White;
            this.pgbProcess.Location = new System.Drawing.Point(3, 3);
            this.pgbProcess.MaxValue = 100;
            this.pgbProcess.Name = "pgbProcess";
            this.pgbProcess.Size = new System.Drawing.Size(483, 45);
            this.pgbProcess.TabIndex = 0;
            this.pgbProcess.Text = "xuiFlatProgressBar1";
            this.pgbProcess.Value = 50;
            // 
            // ptbxLoadingAnimated
            // 
            this.ptbxLoadingAnimated.Dock = System.Windows.Forms.DockStyle.Left;
            this.ptbxLoadingAnimated.Image = global::mixer_control_globalver.Properties.Resources.loading;
            this.ptbxLoadingAnimated.Location = new System.Drawing.Point(0, 0);
            this.ptbxLoadingAnimated.Name = "ptbxLoadingAnimated";
            this.ptbxLoadingAnimated.Size = new System.Drawing.Size(263, 287);
            this.ptbxLoadingAnimated.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ptbxLoadingAnimated.TabIndex = 0;
            this.ptbxLoadingAnimated.TabStop = false;
            // 
            // LoadingDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(761, 287);
            this.Controls.Add(this.panelProgressInfo);
            this.Controls.Add(this.ptbxLoadingAnimated);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoadingDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoadingDialog";
            this.panelProgressInfo.ResumeLayout(false);
            this.panelProgressInfo.PerformLayout();
            this.panelProgressBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbxLoadingAnimated)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ptbxLoadingAnimated;
        private System.Windows.Forms.Panel panelProgressInfo;
        private System.Windows.Forms.Panel panelProgressBar;
        private XanderUI.XUIFlatProgressBar pgbProcess;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Label lbAnnounce;
        private System.Windows.Forms.Label lbPercentage;
    }
}