namespace mixer_control_globalver.View.CustomComponent
{
    partial class AppIntro
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
            this.pbxLogoAnimate = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogoAnimate)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxLogoAnimate
            // 
            this.pbxLogoAnimate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxLogoAnimate.Location = new System.Drawing.Point(0, 0);
            this.pbxLogoAnimate.Margin = new System.Windows.Forms.Padding(4);
            this.pbxLogoAnimate.Name = "pbxLogoAnimate";
            this.pbxLogoAnimate.Size = new System.Drawing.Size(601, 294);
            this.pbxLogoAnimate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLogoAnimate.TabIndex = 0;
            this.pbxLogoAnimate.TabStop = false;
            // 
            // AppIntro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 294);
            this.Controls.Add(this.pbxLogoAnimate);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppIntro";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AppIntro";
            this.Load += new System.EventHandler(this.AppIntro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogoAnimate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxLogoAnimate;
    }
}