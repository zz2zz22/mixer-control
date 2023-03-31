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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelChooseSpecMain = new System.Windows.Forms.Panel();
            this.btnGetTemplate = new XanderUI.XUIButton();
            this.lbFormulaName = new System.Windows.Forms.Label();
            this.lb5 = new System.Windows.Forms.Label();
            this.btnConfirmChoose = new XanderUI.XUIButton();
            this.lb4 = new System.Windows.Forms.Label();
            this.lb3 = new System.Windows.Forms.Label();
            this.dtgvSpecProcessList = new System.Windows.Forms.DataGridView();
            this.dtgvSpecMaterialList = new System.Windows.Forms.DataGridView();
            this.btnRefreshFileList = new XanderUI.XUIButton();
            this.lb2 = new System.Windows.Forms.Label();
            this.dtgvListSpecification = new System.Windows.Forms.DataGridView();
            this.picbtnChooseDirectory = new System.Windows.Forms.PictureBox();
            this.panelChooseSpecMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvSpecProcessList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvSpecMaterialList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListSpecification)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbtnChooseDirectory)).BeginInit();
            this.SuspendLayout();
            // 
            // panelChooseSpecMain
            // 
            this.panelChooseSpecMain.Controls.Add(this.btnGetTemplate);
            this.panelChooseSpecMain.Controls.Add(this.lbFormulaName);
            this.panelChooseSpecMain.Controls.Add(this.lb5);
            this.panelChooseSpecMain.Controls.Add(this.btnConfirmChoose);
            this.panelChooseSpecMain.Controls.Add(this.lb4);
            this.panelChooseSpecMain.Controls.Add(this.lb3);
            this.panelChooseSpecMain.Controls.Add(this.dtgvSpecProcessList);
            this.panelChooseSpecMain.Controls.Add(this.dtgvSpecMaterialList);
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
            this.btnGetTemplate.Location = new System.Drawing.Point(504, 9);
            this.btnGetTemplate.Name = "btnGetTemplate";
            this.btnGetTemplate.Size = new System.Drawing.Size(243, 59);
            this.btnGetTemplate.TabIndex = 14;
            this.btnGetTemplate.TextColor = System.Drawing.Color.Black;
            this.btnGetTemplate.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnGetTemplate.Click += new System.EventHandler(this.btnGetTemplate_Click);
            // 
            // lbFormulaName
            // 
            this.lbFormulaName.AutoSize = true;
            this.lbFormulaName.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFormulaName.Location = new System.Drawing.Point(201, 20);
            this.lbFormulaName.Name = "lbFormulaName";
            this.lbFormulaName.Size = new System.Drawing.Size(0, 27);
            this.lbFormulaName.TabIndex = 13;
            // 
            // lb5
            // 
            this.lb5.AutoSize = true;
            this.lb5.Location = new System.Drawing.Point(12, 9);
            this.lb5.Name = "lb5";
            this.lb5.Size = new System.Drawing.Size(167, 38);
            this.lb5.TabIndex = 12;
            this.lb5.Text = "Công thức đã chọn:\r\nSelected formula:";
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
            // lb4
            // 
            this.lb4.AutoSize = true;
            this.lb4.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb4.Location = new System.Drawing.Point(428, 77);
            this.lb4.Name = "lb4";
            this.lb4.Size = new System.Drawing.Size(163, 38);
            this.lb4.TabIndex = 10;
            this.lb4.Text = "Danh sách quy trình:\r\nProcess list:";
            // 
            // lb3
            // 
            this.lb3.AutoSize = true;
            this.lb3.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb3.Location = new System.Drawing.Point(245, 77);
            this.lb3.Name = "lb3";
            this.lb3.Size = new System.Drawing.Size(123, 38);
            this.lb3.TabIndex = 9;
            this.lb3.Text = "Danh sách liệu:\r\nMaterials list:";
            // 
            // dtgvSpecProcessList
            // 
            this.dtgvSpecProcessList.AllowUserToAddRows = false;
            this.dtgvSpecProcessList.AllowUserToDeleteRows = false;
            this.dtgvSpecProcessList.AllowUserToResizeColumns = false;
            this.dtgvSpecProcessList.AllowUserToResizeRows = false;
            this.dtgvSpecProcessList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgvSpecProcessList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtgvSpecProcessList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvSpecProcessList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvSpecProcessList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvSpecProcessList.EnableHeadersVisualStyles = false;
            this.dtgvSpecProcessList.Location = new System.Drawing.Point(432, 118);
            this.dtgvSpecProcessList.MultiSelect = false;
            this.dtgvSpecProcessList.Name = "dtgvSpecProcessList";
            this.dtgvSpecProcessList.ReadOnly = true;
            this.dtgvSpecProcessList.RowHeadersVisible = false;
            this.dtgvSpecProcessList.RowHeadersWidth = 51;
            this.dtgvSpecProcessList.RowTemplate.Height = 40;
            this.dtgvSpecProcessList.Size = new System.Drawing.Size(315, 409);
            this.dtgvSpecProcessList.TabIndex = 8;
            // 
            // dtgvSpecMaterialList
            // 
            this.dtgvSpecMaterialList.AllowUserToAddRows = false;
            this.dtgvSpecMaterialList.AllowUserToDeleteRows = false;
            this.dtgvSpecMaterialList.AllowUserToResizeColumns = false;
            this.dtgvSpecMaterialList.AllowUserToResizeRows = false;
            this.dtgvSpecMaterialList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtgvSpecMaterialList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvSpecMaterialList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvSpecMaterialList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvSpecMaterialList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvSpecMaterialList.EnableHeadersVisualStyles = false;
            this.dtgvSpecMaterialList.Location = new System.Drawing.Point(249, 118);
            this.dtgvSpecMaterialList.MultiSelect = false;
            this.dtgvSpecMaterialList.Name = "dtgvSpecMaterialList";
            this.dtgvSpecMaterialList.ReadOnly = true;
            this.dtgvSpecMaterialList.RowHeadersVisible = false;
            this.dtgvSpecMaterialList.RowHeadersWidth = 51;
            this.dtgvSpecMaterialList.RowTemplate.Height = 40;
            this.dtgvSpecMaterialList.Size = new System.Drawing.Size(177, 409);
            this.dtgvSpecMaterialList.TabIndex = 7;
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
            this.btnRefreshFileList.Location = new System.Drawing.Point(187, 118);
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
            this.lb2.Location = new System.Drawing.Point(8, 77);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(172, 38);
            this.lb2.TabIndex = 5;
            this.lb2.Text = "Danh sách công thức:\r\nFormula setting files:";
            // 
            // dtgvListSpecification
            // 
            this.dtgvListSpecification.AllowUserToAddRows = false;
            this.dtgvListSpecification.AllowUserToDeleteRows = false;
            this.dtgvListSpecification.AllowUserToResizeColumns = false;
            this.dtgvListSpecification.AllowUserToResizeRows = false;
            this.dtgvListSpecification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtgvListSpecification.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvListSpecification.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvListSpecification.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgvListSpecification.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvListSpecification.EnableHeadersVisualStyles = false;
            this.dtgvListSpecification.Location = new System.Drawing.Point(12, 118);
            this.dtgvListSpecification.MultiSelect = false;
            this.dtgvListSpecification.Name = "dtgvListSpecification";
            this.dtgvListSpecification.ReadOnly = true;
            this.dtgvListSpecification.RowHeadersVisible = false;
            this.dtgvListSpecification.RowHeadersWidth = 51;
            this.dtgvListSpecification.RowTemplate.Height = 40;
            this.dtgvListSpecification.Size = new System.Drawing.Size(169, 409);
            this.dtgvListSpecification.TabIndex = 4;
            this.dtgvListSpecification.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvListSpecification_CellClick);
            // 
            // picbtnChooseDirectory
            // 
            this.picbtnChooseDirectory.Image = global::mixer_control_globalver.Properties.Resources.control;
            this.picbtnChooseDirectory.Location = new System.Drawing.Point(187, 186);
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
            ((System.ComponentModel.ISupportInitialize)(this.dtgvSpecProcessList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvSpecMaterialList)).EndInit();
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
        private System.Windows.Forms.DataGridView dtgvSpecProcessList;
        private System.Windows.Forms.DataGridView dtgvSpecMaterialList;
        private System.Windows.Forms.Label lb3;
        private System.Windows.Forms.Label lb4;
        private XanderUI.XUIButton btnConfirmChoose;
        private System.Windows.Forms.Label lb5;
        private System.Windows.Forms.Label lbFormulaName;
        private XanderUI.XUIButton btnGetTemplate;
    }
}