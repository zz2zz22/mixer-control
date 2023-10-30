namespace mixer_control_globalver.View.CustomControls
{
    partial class QuantityInput
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
            this.txbQuantity = new System.Windows.Forms.TextBox();
            this.btnFinishAll = new XanderUI.XUIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txbQuantity
            // 
            this.txbQuantity.Font = new System.Drawing.Font("Microsoft YaHei", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbQuantity.Location = new System.Drawing.Point(17, 37);
            this.txbQuantity.Name = "txbQuantity";
            this.txbQuantity.Size = new System.Drawing.Size(310, 38);
            this.txbQuantity.TabIndex = 1;
            this.txbQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbQuantity_KeyDown);
            this.txbQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbQuantity_KeyPress);
            // 
            // btnFinishAll
            // 
            this.btnFinishAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnFinishAll.Location = new System.Drawing.Point(39, 102);
            this.btnFinishAll.Name = "btnFinishAll";
            this.btnFinishAll.Size = new System.Drawing.Size(253, 88);
            this.btnFinishAll.TabIndex = 20;
            this.btnFinishAll.TextColor = System.Drawing.Color.Black;
            this.btnFinishAll.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnFinishAll.Click += new System.EventHandler(this.btnFinishAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 25);
            this.label1.TabIndex = 21;
            this.label1.Text = "Input weight";
            // 
            // QuantityInput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(334, 221);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFinishAll);
            this.Controls.Add(this.txbQuantity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "QuantityInput";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Weight Input";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbQuantity;
        private XanderUI.XUIButton btnFinishAll;
        private System.Windows.Forms.Label label1;
    }
}