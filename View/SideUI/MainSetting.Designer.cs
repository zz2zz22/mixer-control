namespace mixer_control_globalver.View.SideUI
{
    partial class MainSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainSetting));
            this.btnGetTemplate = new XanderUI.XUIButton();
            this.SuspendLayout();
            // 
            // btnGetTemplate
            // 
            this.btnGetTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetTemplate.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnGetTemplate.ButtonImage = global::mixer_control_globalver.Properties.Resources.download;
            this.btnGetTemplate.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnGetTemplate.ButtonText = "Get Template";
            this.btnGetTemplate.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnGetTemplate.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnGetTemplate.CornerRadius = 10;
            this.btnGetTemplate.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetTemplate.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnGetTemplate.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnGetTemplate.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnGetTemplate.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnGetTemplate.Location = new System.Drawing.Point(759, 12);
            this.btnGetTemplate.Name = "btnGetTemplate";
            this.btnGetTemplate.Size = new System.Drawing.Size(244, 59);
            this.btnGetTemplate.TabIndex = 4;
            this.btnGetTemplate.TextColor = System.Drawing.Color.Black;
            this.btnGetTemplate.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            // 
            // MainSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1015, 549);
            this.Controls.Add(this.btnGetTemplate);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt / Setting";
            this.ResumeLayout(false);

        }

        #endregion

        private XanderUI.XUIButton btnGetTemplate;
    }
}