using mixer_control_globalver.Controller;
using mixer_control_globalver.View.CustomComponent;
using mixer_control_globalver.View.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace mixer_control_globalver.View.MainUI
{
    public partial class MaterialScale : Form
    {
        int flag;
        bool isFinished;
        string dataIn, scaleMatName, scaleMatNo;
        double matWeight, matTolerance, weightRT, tempWeightRT;
        public MaterialScale(string fileName)
        {
            InitializeComponent();
        }

        public void LoadFlowLayoutMaterial(DataTable dt)
        {
            flag = 0;
            flpMaterialList.Controls.Clear();
            CustomMaterialDataRow customMaterial;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!(bool)TemporaryVariables.materialDT.Rows[i]["is_packed"])
                {
                    if (!(bool)TemporaryVariables.materialDT.Rows[i]["is_scaled"])
                        customMaterial = new CustomMaterialDataRow(dt.Rows[i]["mat_name"].ToString(), dt.Rows[i]["weight"].ToString(), false, false);
                    else
                        customMaterial = new CustomMaterialDataRow(dt.Rows[i]["mat_name"].ToString(), dt.Rows[i]["weight"].ToString(), true, false);
                }
                else
                {
                    customMaterial = new CustomMaterialDataRow(dt.Rows[i]["mat_name"].ToString(), dt.Rows[i]["weight"].ToString(), true, true);
                }
                flpMaterialList.Controls.Add(customMaterial);
            }
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (!(bool)TemporaryVariables.materialDT.Rows[j]["is_scaled"] && !(bool)TemporaryVariables.materialDT.Rows[j]["is_packed"])
                {
                    flag = j;
                    break;
                }
                else
                {
                    flag++;
                }
            }
        }

        private void MaterialScale_Load(object sender, EventArgs e)
        {
            isFinished = false;
            LoadFlowLayoutMaterial(TemporaryVariables.materialDT);
            NextMatScale(TemporaryVariables.materialDT, flag);

            string[] ports = SerialPort.GetPortNames();
            cbComPort.Items.AddRange(ports);

            if (!serialPort1.IsOpen)
            {
                cbComPort.Enabled = true;
                btnConnectScale.ButtonText = "Kết nối cân" + Environment.NewLine + "Connect Scale";
            }
            else
            {
                cbComPort.Enabled = false;
                btnConnectScale.ButtonText = "Ngắt kết nối cân" + Environment.NewLine + "Disconnect Scale";
            }
            if (cbComPort.Items.Count <= 0)
            {
                MessageBox.Show("Không tìm được cổng kết nối serial port! Vui lòng kiểm tra kết nối và thử lại!" + Environment.NewLine + "Serial ports not found! Please check ports connection and try again!", "Cảnh báo / Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnConnectScale.ButtonText = "Tải danh sách COM" + Environment.NewLine + "Load list COM";
            }

            btnSaveWeight.ButtonText = "Lưu KL" + Environment.NewLine + "Save weight";
            btnStackWeight.ButtonText = "Cộng dồn KL" + Environment.NewLine + "Stack weight";
            btnProceedAutomation.ButtonText = "Tiến hành chạy tự động" + Environment.NewLine + "Begin automation process";
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            dataIn = serialPort1.ReadExisting().Replace("\r\n", "").Replace("kg", "").Trim();
            this.Invoke(new EventHandler(showData));
        }

        private void showData(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(dataIn))
            {
                if (Double.TryParse(dataIn, out weightRT))
                {
                    weightRT += tempWeightRT;
                    lbScaleWeight.Text = weightRT.ToString();
                    if (CheckWeight(weightRT))
                    {
                        panelScaleData.BackColor = Color.Green;
                        lbScaleWeight.ForeColor = Color.Black;
                    }
                    else
                    {
                        panelScaleData.BackColor = Color.Black;
                        lbScaleWeight.ForeColor = Color.White;
                    }
                }
            }
        }

        private void btnStackWeight_Click(object sender, EventArgs e)
        {
            if (!Double.TryParse(lbScaleWeight.Text.Trim(), out tempWeightRT))
            {
                MessageBox.Show("Lỗi khi cộng dồn khối lượng. Vui lòng thử lại." + Environment.NewLine + "Error when stack up weight. Please try again.", "Lỗi / Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProceedAutomation_Click(object sender, EventArgs e)
        {
            AppIntro.main.openAutomationTab();
        }

        private void NextMatScale(DataTable dt, int i)
        {
            if (i < dt.Rows.Count)
            {
                scaleMatName = dt.Rows[i]["mat_name"].ToString();
                lbMaterialName.Text = scaleMatName;
                scaleMatNo = dt.Rows[i]["mat_no"].ToString();
                matWeight = Convert.ToDouble(dt.Rows[i]["weight"].ToString());
                matTolerance = Convert.ToDouble(dt.Rows[i]["tolerance"].ToString());
                lbToleranceRange.Text = Convert.ToString(matWeight - matTolerance) + " - " + Convert.ToString(matWeight + matTolerance);
            }
            else
            {
                lbToleranceRange.Text = "0.000 - 0.000";
                scaleMatName = null;
                scaleMatNo = null;
                matWeight = 0;
                matTolerance = 0;
                isFinished = true;
                lbMaterialName.Text = String.Empty;
                lbScaleWeight.Text = "FINISHED";
                panelScaleData.BackColor = Color.Black;
                lbScaleWeight.ForeColor = Color.White;
            }
        }

        private void btnSaveWeight_Click(object sender, EventArgs e)
        {
            if (CheckWeight(weightRT))
            {
                foreach (DataRow dr in TemporaryVariables.materialDT.Rows)
                {
                    if (dr["mat_name"].ToString() == scaleMatName && dr["mat_no"].ToString() == scaleMatNo)
                    {
                        dr["is_scaled"] = true;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Chưa đạt trọng lượng yêu cầu!" + Environment.NewLine + "The required weight has not been reached!！", "Cảnh báo / Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            LoadFlowLayoutMaterial(TemporaryVariables.materialDT);
            NextMatScale(TemporaryVariables.materialDT, flag);
            tempWeightRT = 0;
        }

        private bool CheckWeight(double w)
        {
            if (w <= (matWeight + matTolerance) && w >= (matWeight - matTolerance))
            {
                return true;
            }
            else { return false; }
        }

        private void btnConnectScale_Click(object sender, EventArgs e)
        {
            if (cbComPort.Items.Count <= 0)
            {
                MessageBox.Show("Không tìm được cổng kết nối serial port! Vui lòng kiểm tra kết nối và thử lại!" + Environment.NewLine + "Serial ports not found! Please check ports connection and try again!", "Cảnh báo / Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnConnectScale.ButtonText = "Tải danh sách COM" + Environment.NewLine + "Load list COM";
            }
            else
            {
                if (!String.IsNullOrEmpty(cbComPort.Text))
                {
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Close();
                        btnConnectScale.ButtonText = "Kết nối cân" + Environment.NewLine + "Connect Scale";
                    }
                    else
                    {
                        try
                        {
                            serialPort1.PortName = cbComPort.Text;
                            serialPort1.BaudRate = Convert.ToInt32("9600");
                            serialPort1.DataBits = Convert.ToInt32("8");
                            serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");
                            serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
                            serialPort1.Open();
                            btnConnectScale.ButtonText = "Ngắt kết nối" + Environment.NewLine + "Disconnect scale";
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show("Lỗi kết nối cân" + Environment.NewLine + "Error when connect to scale" + "\r\n\r\n" + err.Message, "Lỗi / Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn cổng kết nối và thử lại!" + Environment.NewLine + "Please choose connect port and try again!", "Cảnh báo / Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void MaterialScale_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }
    }
}
