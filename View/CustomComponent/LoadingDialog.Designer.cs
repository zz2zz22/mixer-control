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
            this.lb1 = new System.Windows.Forms.Label();
            this.ptbxLoadingAnimated = new System.Windows.Forms.PictureBox();
            this.panelProgressInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxLoadingAnimated)).BeginInit();
            this.SuspendLayout();
            // 
            // panelProgressInfo
            // 
            this.panelProgressInfo.BackColor = System.Drawing.Color.White;
            this.panelProgressInfo.Controls.Add(this.lb1);
            this.panelProgressInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProgressInfo.Location = new System.Drawing.Point(263, 0);
            this.panelProgressInfo.Name = "panelProgressInfo";
            this.panelProgressInfo.Size = new System.Drawing.Size(443, 245);
            this.panelProgressInfo.TabIndex = 1;
            // 
            // lb1
            // 
            this.lb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb1.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb1.Location = new System.Drawing.Point(0, 0);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(443, 245);
            this.lb1.TabIndex = 2;
            this.lb1.Text = "Đang thực hiện, vui lòng chờ.\r\nProcessing, please wait.";
            this.lb1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ptbxLoadingAnimated
            // 
            this.ptbxLoadingAnimated.Dock = System.Windows.Forms.DockStyle.Left;
            this.ptbxLoadingAnimated.Image = global::mixer_control_globalver.Properties.Resources.loading;
            this.ptbxLoadingAnimated.Location = new System.Drawing.Point(0, 0);
            this.ptbxLoadingAnimated.Name = "ptbxLoadingAnimated";
            this.ptbxLoadingAnimated.Size = new System.Drawing.Size(263, 245);
            this.ptbxLoadingAnimated.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ptbxLoadingAnimated.TabIndex = 0;
            this.ptbxLoadingAnimated.TabStop = false;
            // 
            // LoadingDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(706, 245);
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
            ((System.ComponentModel.ISupportInitialize)(this.ptbxLoadingAnimated)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ptbxLoadingAnimated;
        private System.Windows.Forms.Panel panelProgressInfo;
        private System.Windows.Forms.Label lb1;
    }
}