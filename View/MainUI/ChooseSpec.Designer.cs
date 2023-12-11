namespace mixer_control_globalver.View.MainUI
{
    partial class ChooseSpec
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
            this.panelChooseSpecMain = new System.Windows.Forms.Panel();
            this.txbSearchFormula = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbFormulaName = new System.Windows.Forms.Label();
            this.lb1 = new System.Windows.Forms.Label();
            this.btnCheckProcess = new XanderUI.XUIButton();
            this.btnImportTemplate = new XanderUI.XUIButton();
            this.btnGetTemplate = new XanderUI.XUIButton();
            this.btnConfirmChoose = new XanderUI.XUIButton();
            this.btnRefreshFileList = new XanderUI.XUIButton();
            this.lb2 = new System.Windows.Forms.Label();
            this.dtgvListSpecification = new System.Windows.Forms.DataGridView();
            this.picbtnChooseDirectory = new System.Windows.Forms.PictureBox();
            this.panelChooseSpecMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListSpecification)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbtnChooseDirectory)).BeginInit();
            this.SuspendLayout();
            // 
            // panelChooseSpecMain
            // 
            this.panelChooseSpecMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelChooseSpecMain.Controls.Add(this.txbSearchFormula);
            this.panelChooseSpecMain.Controls.Add(this.panel1);
            this.panelChooseSpecMain.Controls.Add(this.lb1);
            this.panelChooseSpecMain.Controls.Add(this.btnCheckProcess);
            this.panelChooseSpecMain.Controls.Add(this.btnImportTemplate);
            this.panelChooseSpecMain.Controls.Add(this.btnGetTemplate);
            this.panelChooseSpecMain.Controls.Add(this.btnConfirmChoose);
            this.panelChooseSpecMain.Controls.Add(this.btnRefreshFileList);
            this.panelChooseSpecMain.Controls.Add(this.lb2);
            this.panelChooseSpecMain.Controls.Add(this.dtgvListSpecification);
            this.panelChooseSpecMain.Controls.Add(this.picbtnChooseDirectory);
            this.panelChooseSpecMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChooseSpecMain.Location = new System.Drawing.Point(0, 0);
            this.panelChooseSpecMain.Name = "panelChooseSpecMain";
            this.panelChooseSpecMain.Size = new System.Drawing.Size(759, 608);
            this.panelChooseSpecMain.TabIndex = 0;
            // 
            // txbSearchFormula
            // 
            this.txbSearchFormula.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbSearchFormula.Location = new System.Drawing.Point(12, 119);
            this.txbSearchFormula.Name = "txbSearchFormula";
            this.txbSearchFormula.Size = new System.Drawing.Size(373, 34);
            this.txbSearchFormula.TabIndex = 20;
            this.txbSearchFormula.TextChanged += new System.EventHandler(this.txbSearchFormula_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbFormulaName);
            this.panel1.Location = new System.Drawing.Point(391, 239);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 89);
            this.panel1.TabIndex = 19;
            // 
            // lbFormulaName
            // 
            this.lbFormulaName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFormulaName.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFormulaName.Location = new System.Drawing.Point(0, 0);
            this.lbFormulaName.Name = "lbFormulaName";
            this.lbFormulaName.Size = new System.Drawing.Size(356, 89);
            this.lbFormulaName.TabIndex = 18;
            this.lbFormulaName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb1.Location = new System.Drawing.Point(387, 198);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(156, 38);
            this.lb1.TabIndex = 17;
            this.lb1.Text = "Công thức đã chọn:\r\nSelected formula:";
            // 
            // btnCheckProcess
            // 
            this.btnCheckProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckProcess.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCheckProcess.ButtonImage = global::mixer_control_globalver.Properties.Resources.information1;
            this.btnCheckProcess.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnCheckProcess.ButtonText = "Check formula";
            this.btnCheckProcess.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnCheckProcess.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnCheckProcess.CornerRadius = 10;
            this.btnCheckProcess.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckProcess.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnCheckProcess.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnCheckProcess.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnCheckProcess.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnCheckProcess.Location = new System.Drawing.Point(503, 358);
            this.btnCheckProcess.Name = "btnCheckProcess";
            this.btnCheckProcess.Size = new System.Drawing.Size(244, 71);
            this.btnCheckProcess.TabIndex = 16;
            this.btnCheckProcess.TextColor = System.Drawing.Color.Black;
            this.btnCheckProcess.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnCheckProcess.Click += new System.EventHandler(this.btnCheckProcess_Click);
            // 
            // btnImportTemplate
            // 
            this.btnImportTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportTemplate.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnImportTemplate.ButtonImage = global::mixer_control_globalver.Properties.Resources.submit;
            this.btnImportTemplate.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnImportTemplate.ButtonText = "Upload formula";
            this.btnImportTemplate.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnImportTemplate.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnImportTemplate.CornerRadius = 10;
            this.btnImportTemplate.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportTemplate.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnImportTemplate.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnImportTemplate.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnImportTemplate.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnImportTemplate.Location = new System.Drawing.Point(74, 9);
            this.btnImportTemplate.Name = "btnImportTemplate";
            this.btnImportTemplate.Size = new System.Drawing.Size(179, 59);
            this.btnImportTemplate.TabIndex = 15;
            this.btnImportTemplate.TextColor = System.Drawing.Color.Black;
            this.btnImportTemplate.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnImportTemplate.Click += new System.EventHandler(this.btnImportTemplate_Click);
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
            this.btnGetTemplate.Location = new System.Drawing.Point(564, 9);
            this.btnGetTemplate.Name = "btnGetTemplate";
            this.btnGetTemplate.Size = new System.Drawing.Size(183, 59);
            this.btnGetTemplate.TabIndex = 14;
            this.btnGetTemplate.TextColor = System.Drawing.Color.Black;
            this.btnGetTemplate.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnGetTemplate.Click += new System.EventHandler(this.btnGetTemplate_Click);
            // 
            // btnConfirmChoose
            // 
            this.btnConfirmChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmChoose.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnConfirmChoose.ButtonImage = global::mixer_control_globalver.Properties.Resources.right_arrow;
            this.btnConfirmChoose.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnConfirmChoose.ButtonText = "Next step";
            this.btnConfirmChoose.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnConfirmChoose.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnConfirmChoose.CornerRadius = 10;
            this.btnConfirmChoose.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmChoose.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnConfirmChoose.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnConfirmChoose.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnConfirmChoose.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnConfirmChoose.Location = new System.Drawing.Point(503, 533);
            this.btnConfirmChoose.Name = "btnConfirmChoose";
            this.btnConfirmChoose.Size = new System.Drawing.Size(244, 63);
            this.btnConfirmChoose.TabIndex = 11;
            this.btnConfirmChoose.TextColor = System.Drawing.Color.Black;
            this.btnConfirmChoose.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnConfirmChoose.Click += new System.EventHandler(this.btnConfirmChoose_Click);
            // 
            // btnRefreshFileList
            // 
            this.btnRefreshFileList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRefreshFileList.ButtonImage = global::mixer_control_globalver.Properties.Resources.update;
            this.btnRefreshFileList.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnRefreshFileList.ButtonText = "Refresh";
            this.btnRefreshFileList.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnRefreshFileList.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnRefreshFileList.CornerRadius = 10;
            this.btnRefreshFileList.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshFileList.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnRefreshFileList.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnRefreshFileList.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnRefreshFileList.ImagePosition = XanderUI.XUIButton.imgPosition.Center;
            this.btnRefreshFileList.Location = new System.Drawing.Point(329, 57);
            this.btnRefreshFileList.Name = "btnRefreshFileList";
            this.btnRefreshFileList.Size = new System.Drawing.Size(56, 51);
            this.btnRefreshFileList.TabIndex = 6;
            this.btnRefreshFileList.TextColor = System.Drawing.Color.Black;
            this.btnRefreshFileList.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnRefreshFileList.Click += new System.EventHandler(this.btnRefreshFileList_Click);
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb2.Location = new System.Drawing.Point(12, 73);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(172, 38);
            this.lb2.TabIndex = 5;
            this.lb2.Text = "Danh sách công thức:\r\nFormula setting files:";
            // 
            // dtgvListSpecification
            // 
            this.dtgvListSpecification.AllowUserToAddRows = false;
            this.dtgvListSpecification.AllowUserToDeleteRows = false;
            this.dtgvListSpecification.AllowUserToOrderColumns = true;
            this.dtgvListSpecification.AllowUserToResizeColumns = false;
            this.dtgvListSpecification.AllowUserToResizeRows = false;
            this.dtgvListSpecification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtgvListSpecification.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvListSpecification.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvListSpecification.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvListSpecification.ColumnHeadersHeight = 60;
            this.dtgvListSpecification.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgvListSpecification.EnableHeadersVisualStyles = false;
            this.dtgvListSpecification.Location = new System.Drawing.Point(12, 159);
            this.dtgvListSpecification.MultiSelect = false;
            this.dtgvListSpecification.Name = "dtgvListSpecification";
            this.dtgvListSpecification.ReadOnly = true;
            this.dtgvListSpecification.RowHeadersVisible = false;
            this.dtgvListSpecification.RowHeadersWidth = 51;
            this.dtgvListSpecification.RowTemplate.Height = 40;
            this.dtgvListSpecification.Size = new System.Drawing.Size(373, 437);
            this.dtgvListSpecification.TabIndex = 4;
            this.dtgvListSpecification.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvListSpecification_CellClick);
            // 
            // picbtnChooseDirectory
            // 
            this.picbtnChooseDirectory.Image = global::mixer_control_globalver.Properties.Resources.control;
            this.picbtnChooseDirectory.Location = new System.Drawing.Point(12, 12);
            this.picbtnChooseDirectory.Name = "picbtnChooseDirectory";
            this.picbtnChooseDirectory.Size = new System.Drawing.Size(56, 51);
            this.picbtnChooseDirectory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbtnChooseDirectory.TabIndex = 1;
            this.picbtnChooseDirectory.TabStop = false;
            this.picbtnChooseDirectory.Click += new System.EventHandler(this.picbtnChooseDirectory_Click);
            // 
            // ChooseSpec
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(759, 608);
            this.Controls.Add(this.panelChooseSpecMain);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChooseSpec";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ChooseSpec";
            this.Load += new System.EventHandler(this.ChooseSpec_Load);
            this.panelChooseSpecMain.ResumeLayout(false);
            this.panelChooseSpecMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListSpecification)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbtnChooseDirectory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelChooseSpecMain;
        private System.Windows.Forms.PictureBox picbtnChooseDirectory;
        private System.Windows.Forms.DataGridView dtgvListSpecification;
        private System.Windows.Forms.Label lb2;
        private XanderUI.XUIButton btnRefreshFileList;
        private XanderUI.XUIButton btnConfirmChoose;
        private XanderUI.XUIButton btnGetTemplate;
        private XanderUI.XUIButton btnImportTemplate;
        private XanderUI.XUIButton btnCheckProcess;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbFormulaName;
        private System.Windows.Forms.TextBox txbSearchFormula;
    }
}