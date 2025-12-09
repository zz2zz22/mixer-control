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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingDialog));
            this.panelProgressInfo = new System.Windows.Forms.Panel();
            this.lb1 = new System.Windows.Forms.Label();
            this.ptbxLoadingAnimated = new System.Windows.Forms.PictureBox();
            this.panelProgressInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxLoadingAnimated)).BeginInit();
            this.SuspendLayout();
            // 
            // panelProgressInfo
            // 
            resources.ApplyResources(this.panelProgressInfo, "panelProgressInfo");
            this.panelProgressInfo.BackColor = System.Drawing.Color.White;
            this.panelProgressInfo.Controls.Add(this.lb1);
            this.panelProgressInfo.Name = "panelProgressInfo";
            // 
            // lb1
            // 
            resources.ApplyResources(this.lb1, "lb1");
            this.lb1.Name = "lb1";
            // 
            // ptbxLoadingAnimated
            // 
            resources.ApplyResources(this.ptbxLoadingAnimated, "ptbxLoadingAnimated");
            this.ptbxLoadingAnimated.Image = global::mixer_control_globalver.Properties.Resources.loading;
            this.ptbxLoadingAnimated.Name = "ptbxLoadingAnimated";
            this.ptbxLoadingAnimated.TabStop = false;
            // 
            // LoadingDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panelProgressInfo);
            this.Controls.Add(this.ptbxLoadingAnimated);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadingDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
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