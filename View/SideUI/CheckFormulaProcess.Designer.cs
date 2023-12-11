namespace mixer_control_globalver.View.SideUI
{
    partial class CheckFormulaProcess
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnClose = new XanderUI.XUIButton();
            this.pbxCompanyLogo = new System.Windows.Forms.PictureBox();
            this.dtgvSpecProcessList = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCompanyLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvSpecProcessList)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.pbxCompanyLogo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(787, 65);
            this.panelHeader.TabIndex = 25;
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHeader_MouseDown);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnClose.Location = new System.Drawing.Point(727, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 56);
            this.btnClose.TabIndex = 2;
            this.btnClose.TextColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbxCompanyLogo
            // 
            this.pbxCompanyLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pbxCompanyLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbxCompanyLogo.Image = global::mixer_control_globalver.Properties.Resources.logoTechlinkFix;
            this.pbxCompanyLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pbxCompanyLogo.Location = new System.Drawing.Point(0, 0);
            this.pbxCompanyLogo.Name = "pbxCompanyLogo";
            this.pbxCompanyLogo.Size = new System.Drawing.Size(246, 63);
            this.pbxCompanyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxCompanyLogo.TabIndex = 0;
            this.pbxCompanyLogo.TabStop = false;
            // 
            // dtgvSpecProcessList
            // 
            this.dtgvSpecProcessList.AllowUserToAddRows = false;
            this.dtgvSpecProcessList.AllowUserToDeleteRows = false;
            this.dtgvSpecProcessList.AllowUserToResizeColumns = false;
            this.dtgvSpecProcessList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvSpecProcessList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dtgvSpecProcessList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvSpecProcessList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvSpecProcessList.ColumnHeadersHeight = 60;
            this.dtgvSpecProcessList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgvSpecProcessList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvSpecProcessList.EnableHeadersVisualStyles = false;
            this.dtgvSpecProcessList.Location = new System.Drawing.Point(0, 65);
            this.dtgvSpecProcessList.MultiSelect = false;
            this.dtgvSpecProcessList.Name = "dtgvSpecProcessList";
            this.dtgvSpecProcessList.ReadOnly = true;
            this.dtgvSpecProcessList.RowHeadersVisible = false;
            this.dtgvSpecProcessList.RowHeadersWidth = 51;
            this.dtgvSpecProcessList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dtgvSpecProcessList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgvSpecProcessList.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvSpecProcessList.RowTemplate.Height = 45;
            this.dtgvSpecProcessList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dtgvSpecProcessList.Size = new System.Drawing.Size(787, 578);
            this.dtgvSpecProcessList.TabIndex = 26;
            // 
            // CheckFormulaProcess
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(787, 643);
            this.Controls.Add(this.dtgvSpecProcessList);
            this.Controls.Add(this.panelHeader);
            this.DoubleBuffered = true;
            this.Name = "CheckFormulaProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CheckFormulaProcess";
            this.Load += new System.EventHandler(this.CheckFormulaProcess_Load);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxCompanyLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvSpecProcessList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private XanderUI.XUIButton btnClose;
        private System.Windows.Forms.PictureBox pbxCompanyLogo;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.DataGridView dtgvSpecProcessList;
    }
}