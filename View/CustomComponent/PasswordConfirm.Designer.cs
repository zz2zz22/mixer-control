namespace mixer_control_globalver.View.CustomComponent
{
    partial class PasswordConfirm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbAnnounce = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnConfirm = new XanderUI.XUIButton();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbAnnounce);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(520, 62);
            this.panel1.TabIndex = 0;
            // 
            // lbAnnounce
            // 
            this.lbAnnounce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAnnounce.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAnnounce.Location = new System.Drawing.Point(0, 0);
            this.lbAnnounce.Name = "lbAnnounce";
            this.lbAnnounce.Size = new System.Drawing.Size(444, 60);
            this.lbAnnounce.TabIndex = 1;
            this.lbAnnounce.Text = "label1";
            this.lbAnnounce.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::mixer_control_globalver.Properties.Resources.cancel;
            this.pictureBox1.Location = new System.Drawing.Point(444, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(74, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnConfirm);
            this.panel2.Controls.Add(this.txbPassword);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 62);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(520, 181);
            this.panel2.TabIndex = 1;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfirm.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnConfirm.ButtonImage = global::mixer_control_globalver.Properties.Resources.confirmation;
            this.btnConfirm.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnConfirm.ButtonText = "ok";
            this.btnConfirm.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnConfirm.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnConfirm.CornerRadius = 10;
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnConfirm.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnConfirm.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnConfirm.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnConfirm.Location = new System.Drawing.Point(147, 78);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(210, 67);
            this.btnConfirm.TabIndex = 20;
            this.btnConfirm.TextColor = System.Drawing.Color.Black;
            this.btnConfirm.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txbPassword
            // 
            this.txbPassword.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbPassword.Location = new System.Drawing.Point(78, 15);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new System.Drawing.Size(330, 34);
            this.txbPassword.TabIndex = 0;
            this.txbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbPassword_KeyDown);
            // 
            // PasswordConfirm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(520, 243);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(520, 243);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(520, 243);
            this.Name = "PasswordConfirm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PasswordConfirm";
            this.Load += new System.EventHandler(this.PasswordConfirm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbAnnounce;
        private System.Windows.Forms.TextBox txbPassword;
        private XanderUI.XUIButton btnConfirm;
    }
}