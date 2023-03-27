namespace mixer_control_globalver.View.MainUI
{
    partial class MaterialScale
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
            this.components = new System.ComponentModel.Container();
            this.panelMaterialScaleMain = new System.Windows.Forms.Panel();
            this.lbMaterialName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnProceedAutomation = new XanderUI.XUIButton();
            this.btnStackWeight = new XanderUI.XUIButton();
            this.btnSaveWeight = new XanderUI.XUIButton();
            this.lbToleranceRange = new System.Windows.Forms.Label();
            this.lb4 = new System.Windows.Forms.Label();
            this.btnConnectScale = new XanderUI.XUIButton();
            this.lb3 = new System.Windows.Forms.Label();
            this.cbComPort = new System.Windows.Forms.ComboBox();
            this.panelScaleData = new System.Windows.Forms.Panel();
            this.lbScaleWeight = new System.Windows.Forms.Label();
            this.flpMaterialList = new System.Windows.Forms.FlowLayoutPanel();
            this.lb1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.panelMaterialScaleMain.SuspendLayout();
            this.panelScaleData.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMaterialScaleMain
            // 
            this.panelMaterialScaleMain.Controls.Add(this.lbMaterialName);
            this.panelMaterialScaleMain.Controls.Add(this.label2);
            this.panelMaterialScaleMain.Controls.Add(this.btnProceedAutomation);
            this.panelMaterialScaleMain.Controls.Add(this.btnStackWeight);
            this.panelMaterialScaleMain.Controls.Add(this.btnSaveWeight);
            this.panelMaterialScaleMain.Controls.Add(this.lbToleranceRange);
            this.panelMaterialScaleMain.Controls.Add(this.lb4);
            this.panelMaterialScaleMain.Controls.Add(this.btnConnectScale);
            this.panelMaterialScaleMain.Controls.Add(this.lb3);
            this.panelMaterialScaleMain.Controls.Add(this.cbComPort);
            this.panelMaterialScaleMain.Controls.Add(this.panelScaleData);
            this.panelMaterialScaleMain.Controls.Add(this.flpMaterialList);
            this.panelMaterialScaleMain.Controls.Add(this.lb1);
            this.panelMaterialScaleMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaterialScaleMain.Location = new System.Drawing.Point(0, 0);
            this.panelMaterialScaleMain.Name = "panelMaterialScaleMain";
            this.panelMaterialScaleMain.Size = new System.Drawing.Size(759, 608);
            this.panelMaterialScaleMain.TabIndex = 1;
            // 
            // lbMaterialName
            // 
            this.lbMaterialName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMaterialName.AutoSize = true;
            this.lbMaterialName.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMaterialName.Location = new System.Drawing.Point(565, 77);
            this.lbMaterialName.Name = "lbMaterialName";
            this.lbMaterialName.Size = new System.Drawing.Size(25, 21);
            this.lbMaterialName.TabIndex = 19;
            this.lbMaterialName.Text = "...";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(429, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 38);
            this.label2.TabIndex = 18;
            this.label2.Text = "Tên nguyên liệu:\r\nMaterial name:";
            // 
            // btnProceedAutomation
            // 
            this.btnProceedAutomation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProceedAutomation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnProceedAutomation.ButtonImage = global::mixer_control_globalver.Properties.Resources.right_arrow;
            this.btnProceedAutomation.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnProceedAutomation.ButtonText = "Proceed automation";
            this.btnProceedAutomation.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnProceedAutomation.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnProceedAutomation.CornerRadius = 10;
            this.btnProceedAutomation.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProceedAutomation.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnProceedAutomation.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnProceedAutomation.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnProceedAutomation.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnProceedAutomation.Location = new System.Drawing.Point(448, 529);
            this.btnProceedAutomation.Name = "btnProceedAutomation";
            this.btnProceedAutomation.Size = new System.Drawing.Size(299, 67);
            this.btnProceedAutomation.TabIndex = 17;
            this.btnProceedAutomation.TextColor = System.Drawing.Color.Black;
            this.btnProceedAutomation.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnProceedAutomation.Click += new System.EventHandler(this.btnProceedAutomation_Click);
            // 
            // btnStackWeight
            // 
            this.btnStackWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStackWeight.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnStackWeight.ButtonImage = global::mixer_control_globalver.Properties.Resources.plus;
            this.btnStackWeight.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnStackWeight.ButtonText = "Save";
            this.btnStackWeight.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnStackWeight.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnStackWeight.CornerRadius = 10;
            this.btnStackWeight.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStackWeight.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnStackWeight.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnStackWeight.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnStackWeight.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnStackWeight.Location = new System.Drawing.Point(538, 380);
            this.btnStackWeight.Name = "btnStackWeight";
            this.btnStackWeight.Size = new System.Drawing.Size(209, 56);
            this.btnStackWeight.TabIndex = 16;
            this.btnStackWeight.TextColor = System.Drawing.Color.Black;
            this.btnStackWeight.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnStackWeight.Click += new System.EventHandler(this.btnStackWeight_Click);
            // 
            // btnSaveWeight
            // 
            this.btnSaveWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveWeight.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSaveWeight.ButtonImage = global::mixer_control_globalver.Properties.Resources.save;
            this.btnSaveWeight.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnSaveWeight.ButtonText = "Save";
            this.btnSaveWeight.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnSaveWeight.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnSaveWeight.CornerRadius = 10;
            this.btnSaveWeight.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveWeight.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnSaveWeight.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnSaveWeight.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnSaveWeight.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnSaveWeight.Location = new System.Drawing.Point(538, 442);
            this.btnSaveWeight.Name = "btnSaveWeight";
            this.btnSaveWeight.Size = new System.Drawing.Size(209, 56);
            this.btnSaveWeight.TabIndex = 15;
            this.btnSaveWeight.TextColor = System.Drawing.Color.Black;
            this.btnSaveWeight.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnSaveWeight.Click += new System.EventHandler(this.btnSaveWeight_Click);
            // 
            // lbToleranceRange
            // 
            this.lbToleranceRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbToleranceRange.AutoSize = true;
            this.lbToleranceRange.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbToleranceRange.Location = new System.Drawing.Point(512, 322);
            this.lbToleranceRange.Name = "lbToleranceRange";
            this.lbToleranceRange.Size = new System.Drawing.Size(148, 27);
            this.lbToleranceRange.TabIndex = 14;
            this.lbToleranceRange.Text = "0.000 - 0.000";
            // 
            // lb4
            // 
            this.lb4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb4.AutoSize = true;
            this.lb4.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb4.Location = new System.Drawing.Point(426, 271);
            this.lb4.Name = "lb4";
            this.lb4.Size = new System.Drawing.Size(137, 38);
            this.lb4.TabIndex = 13;
            this.lb4.Text = "Khoảng dung sai:\r\nTolerance range:";
            // 
            // btnConnectScale
            // 
            this.btnConnectScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnectScale.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnConnectScale.ButtonImage = global::mixer_control_globalver.Properties.Resources.scale;
            this.btnConnectScale.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.btnConnectScale.ButtonText = "Connect scale";
            this.btnConnectScale.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnConnectScale.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.btnConnectScale.CornerRadius = 10;
            this.btnConnectScale.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnectScale.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.btnConnectScale.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnConnectScale.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.btnConnectScale.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.btnConnectScale.Location = new System.Drawing.Point(538, 9);
            this.btnConnectScale.Name = "btnConnectScale";
            this.btnConnectScale.Size = new System.Drawing.Size(209, 56);
            this.btnConnectScale.TabIndex = 12;
            this.btnConnectScale.TextColor = System.Drawing.Color.Black;
            this.btnConnectScale.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.btnConnectScale.Click += new System.EventHandler(this.btnConnectScale_Click);
            // 
            // lb3
            // 
            this.lb3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb3.AutoSize = true;
            this.lb3.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb3.Location = new System.Drawing.Point(429, 126);
            this.lb3.Name = "lb3";
            this.lb3.Size = new System.Drawing.Size(231, 38);
            this.lb3.TabIndex = 4;
            this.lb3.Text = "Kết quả cân khối lượng (kg):\r\nWeight scale result (kilogram):";
            // 
            // cbComPort
            // 
            this.cbComPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComPort.FormattingEnabled = true;
            this.cbComPort.Location = new System.Drawing.Point(430, 20);
            this.cbComPort.Name = "cbComPort";
            this.cbComPort.Size = new System.Drawing.Size(102, 27);
            this.cbComPort.TabIndex = 10;
            // 
            // panelScaleData
            // 
            this.panelScaleData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelScaleData.BackColor = System.Drawing.Color.Black;
            this.panelScaleData.Controls.Add(this.lbScaleWeight);
            this.panelScaleData.ForeColor = System.Drawing.SystemColors.Control;
            this.panelScaleData.Location = new System.Drawing.Point(429, 167);
            this.panelScaleData.Name = "panelScaleData";
            this.panelScaleData.Size = new System.Drawing.Size(321, 101);
            this.panelScaleData.TabIndex = 2;
            // 
            // lbScaleWeight
            // 
            this.lbScaleWeight.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbScaleWeight.AutoSize = true;
            this.lbScaleWeight.BackColor = System.Drawing.Color.Transparent;
            this.lbScaleWeight.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbScaleWeight.ForeColor = System.Drawing.Color.White;
            this.lbScaleWeight.Location = new System.Drawing.Point(74, 14);
            this.lbScaleWeight.Name = "lbScaleWeight";
            this.lbScaleWeight.Size = new System.Drawing.Size(179, 70);
            this.lbScaleWeight.TabIndex = 5;
            this.lbScaleWeight.Text = "0.000";
            this.lbScaleWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpMaterialList
            // 
            this.flpMaterialList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpMaterialList.AutoScroll = true;
            this.flpMaterialList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.flpMaterialList.Location = new System.Drawing.Point(12, 50);
            this.flpMaterialList.Name = "flpMaterialList";
            this.flpMaterialList.Size = new System.Drawing.Size(408, 546);
            this.flpMaterialList.TabIndex = 1;
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb1.Location = new System.Drawing.Point(12, 9);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(271, 38);
            this.lb1.TabIndex = 0;
            this.lb1.Text = "Danh sách nguyên vật liệu cần cân:\r\nScale required materials list:";
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // MaterialScale
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(759, 608);
            this.Controls.Add(this.panelMaterialScaleMain);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MaterialScale";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "MaterialScale";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MaterialScale_FormClosing);
            this.Load += new System.EventHandler(this.MaterialScale_Load);
            this.panelMaterialScaleMain.ResumeLayout(false);
            this.panelMaterialScaleMain.PerformLayout();
            this.panelScaleData.ResumeLayout(false);
            this.panelScaleData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMaterialScaleMain;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.FlowLayoutPanel flpMaterialList;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox cbComPort;
        private System.Windows.Forms.Panel panelScaleData;
        private XanderUI.XUIButton btnConnectScale;
        private System.Windows.Forms.Label lb3;
        private System.Windows.Forms.Label lbScaleWeight;
        private System.Windows.Forms.Label lb4;
        private System.Windows.Forms.Label lbToleranceRange;
        private XanderUI.XUIButton btnSaveWeight;
        private XanderUI.XUIButton btnStackWeight;
        private XanderUI.XUIButton btnProceedAutomation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbMaterialName;
    }
}