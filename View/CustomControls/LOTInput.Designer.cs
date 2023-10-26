namespace mixer_control_globalver.View.CustomControls
{
    partial class LOTInput
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
            this.txbLOTNo = new System.Windows.Forms.TextBox();
            this.btnFinishAll = new XanderUI.XUIButton();
            this.SuspendLayout();
            // 
            // txbLOTNo
            // 
            this.txbLOTNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbLOTNo.Font = new System.Drawing.Font("Microsoft YaHei", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbLOTNo.Location = new System.Drawing.Point(12, 32);
            this.txbLOTNo.Name = "txbLOTNo";
            this.txbLOTNo.Size = new System.Drawing.Size(418, 38);
            this.txbLOTNo.TabIndex = 1;
            this.txbLOTNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbLOTNo_KeyDown);
            // 
            // btnFinishAll
            // 
            this.btnFinishAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnFinishAll.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnFinishAll.ButtonImage = global::mixer_control_globalver.Properties.Resources.save;
            this.btnFinishAll.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnFinishAll.ButtonText = "Finish";
            this.btnFinishAll.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnFinishAll.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnFinishAll.CornerRadius = 10;
            this.btnFinishAll.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinishAll.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnFinishAll.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnFinishAll.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnFinishAll.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnFinishAll.Location = new System.Drawing.Point(94, 96);
            this.btnFinishAll.Name = "btnFinishAll";
            this.btnFinishAll.Size = new System.Drawing.Size(253, 88);
            this.btnFinishAll.TabIndex = 21;
            this.btnFinishAll.TextColor = System.Drawing.Color.Black;
            this.btnFinishAll.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnFinishAll.Click += new System.EventHandler(this.btnFinishAll_Click);
            // 
            // LOTInput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(442, 223);
            this.Controls.Add(this.btnFinishAll);
            this.Controls.Add(this.txbLOTNo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LOTInput";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOT Input";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbLOTNo;
        private XanderUI.XUIButton btnFinishAll;
    }
}