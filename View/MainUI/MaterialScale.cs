using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.Device;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomComponent;
using mixer_control_globalver.View.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace mixer_control_globalver.View.MainUI
{
    public partial class MaterialScale : Form, IMessageFilter
    {
        int flag;
        string scaleMatName, scaleMatNo;
        string message = String.Empty, caption = String.Empty;
        private readonly StringBuilder _buffer = new StringBuilder();
        const int WM_CHAR = 0x0102;
        int _keyCount = 0;
        const int SCAN_MIN_LENGTH = 8;
        const double SECONDS_PER_CHARACTER_MIN_PERIOD = 0.3;


        public MaterialScale()
        {
            InitializeComponent();
            // Add message filter to hook WM_KEYDOWN events.
            Application.AddMessageFilter(this);
            Disposed += (sender, e) => Application.RemoveMessageFilter(this);
        }
        public bool PreFilterMessage(ref Message m)
        {
            // SOLUTION DO THIS (Thanks Jimi!)
            if (m.Msg.Equals(WM_CHAR)) detectScan((char)m.WParam);
            // NOT THIS
            // if(m.Msg.Equals(WM_KEYDOWN)) detectScan((char)m.WParam);
            return false;
        }

        private void detectScan(char @char)
        {
            Debug.WriteLine(@char);
            if (_keyCount == 0) _buffer.Clear();
            int charCountCapture = ++_keyCount;
            _buffer.Append(@char);
            Task
                .Delay(TimeSpan.FromSeconds(SECONDS_PER_CHARACTER_MIN_PERIOD))
                .GetAwaiter()
                .OnCompleted(() =>
                {
                    if (charCountCapture.Equals(_keyCount))
                    {
                        _keyCount = 0;
                        if (_buffer.Length > SCAN_MIN_LENGTH)
                        {
                            string[] QRresult = _buffer.ToString().Split(';');
                            if (QRresult.Length > 1)
                            {
                                if (TemporaryVariables.materialDT.AsEnumerable().Any(row => row.Field<String>("mat_name").Contains(QRresult[1].Trim())))
                                {
                                    TemporaryVariables.materialCode = QRresult[1].Trim();
                                    TemporaryVariables.isInputQuantity = false;
                                    double upTolerance = 0, downTolerance = 0, initTolerance, initWeight;
                                    bool isConfirmed = false;
                                    foreach (DataRow dr in TemporaryVariables.materialDT.Rows)
                                    {
                                        if (dr["mat_name"].ToString().Contains(TemporaryVariables.materialCode))
                                        {
                                            initTolerance = Convert.ToDouble(dr["tolerance"].ToString());
                                            initWeight = Convert.ToDouble(dr["weight"].ToString());
                                            upTolerance = initWeight + initTolerance;
                                            downTolerance = initWeight - initTolerance;
                                            if(downTolerance < 0)
                                                downTolerance = 0;
                                            isConfirmed = Convert.ToBoolean(dr["is_confirmed"]);
                                        }
                                    }
                                    if(!isConfirmed)
                                    {
                                        QuantityInput quantityInput = new QuantityInput(TemporaryVariables.materialCode, upTolerance, downTolerance);
                                        quantityInput.FormClosed += quantityInputFormClosed;
                                        quantityInput.ShowDialog();
                                    }
                                    else
                                    {
                                        if (TemporaryVariables.language == 0)
                                        {
                                            message = "Nguyên vật liệu đã đủ trọng lượng!\r\nThe materials have enough weight!";
                                            caption = "Thông tin / Information";
                                        }
                                        else if (TemporaryVariables.language == 1)
                                        {
                                            message = "Nguyên vật liệu đã đủ trọng lượng!\r\n材料有足够的重量！";
                                            caption = "Thông tin / 空中";
                                        }
                                        else if (Settings.Default.language == 2)
                                        {
                                            message = "The materials have enough weight!";
                                            caption = "Information";
                                        }
                                        else if (Settings.Default.language == 3)
                                        {
                                            message = "Nguyên vật liệu đã đủ trọng lượng!";
                                            caption = "Thông tin";
                                        }
                                        else if (Settings.Default.language == 4)
                                        {
                                            message = "材料有足够的重量！";
                                            caption = "空中";
                                        }
                                        CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                    }
                });
        }

        private void quantityInputFormClosed(object sender, EventArgs e)
        {
            ((Form)sender).FormClosed -= quantityInputFormClosed;
            if (TemporaryVariables.isInputQuantity)
            {
                TemporaryVariables.isInputQuantity = false;
                foreach (CustomMaterialDataRow c in flpMaterialList.Controls)
                {
                    if (c.Code.Contains(TemporaryVariables.materialCode))
                    {
                        foreach (DataRow dr in TemporaryVariables.materialDT.Rows)
                        {
                            if (dr["mat_name"].ToString().Contains(TemporaryVariables.materialCode))
                            {
                                c.Quantity += TemporaryVariables.inputQuantity;
                                dr["is_confirmed"] = true;
                                c.Status = (bool)dr["is_confirmed"];
                                dr["weight"] = c.Quantity;
                                TemporaryVariables.inputQuantity = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void LoadFlowLayoutMaterial(DataTable dt)
        {
            flpMaterialList.Controls.Clear();
            if (dt.Rows.Count > 0)
            {
                CustomMaterialDataRow[] listItems = new CustomMaterialDataRow[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!(bool)TemporaryVariables.materialDT.Rows[i]["is_confirmed"])
                    {
                        listItems[i] = new CustomMaterialDataRow
                        {
                            Code = dt.Rows[i]["mat_name"].ToString(),
                            Quantity = 0,
                            Status = (bool)TemporaryVariables.materialDT.Rows[i]["is_confirmed"]
                        };
                    }
                    else
                    {
                        listItems[i] = new CustomMaterialDataRow
                        {
                            Code = dt.Rows[i]["mat_name"].ToString(),
                            Quantity = Convert.ToDouble(dt.Rows[i]["weight"].ToString()),
                            Status = (bool)TemporaryVariables.materialDT.Rows[i]["is_confirmed"]
                        };
                    }

                    flpMaterialList.Controls.Add(listItems[i]);
                }
            }
        }

        private void MaterialScale_Load(object sender, EventArgs e)
        {
            lbFormulaName.Text = TemporaryVariables.tempFileName;
            LoadFlowLayoutMaterial(TemporaryVariables.materialDT);
            CheckMaterialLeft();

            if (TemporaryVariables.language == 0)
            {
                lb1.Text = "Danh sách nguyên vật liệu cần xác nhận:\r\nConfirmation required materials list:";
                lb3.Text = "Công thức:\r\nFormula:";

                btnProceedAutomation.ButtonText = "Tiến hành chạy tự động\r\nBegin automation process";
            }
            else if (TemporaryVariables.language == 1)
            {
                lb1.Text = "Danh sách nguyên vật liệu cần xác nhận:\r\n需要确认的原料列表:";
                lb3.Text = "Công thức:\r\n型号:";

                btnProceedAutomation.ButtonText = "Tiến hành chạy tự động\r\n开始运行";
            }
            else if (Settings.Default.language == 2)
            {
                lb1.Text = "Confirmation required materials list:";
                lb3.Text = "Formula:";

                btnProceedAutomation.ButtonText = "Begin automation process";
            }
            else if (Settings.Default.language == 3)
            {
                lb1.Text = "Danh sách nguyên vật liệu cần xác nhận:";
                lb3.Text = "Công thức:";

                btnProceedAutomation.ButtonText = "Tiến hành chạy tự động";
            }
            else if (Settings.Default.language == 4)
            {
                lb1.Text = "需要确认的原料列表:";
                lb3.Text = "型号:";

                btnProceedAutomation.ButtonText = "开始运行";
            }

        }

        private void lOTInputFormClosed(object sender, EventArgs e)
        {
            ((Form)sender).FormClosed -= lOTInputFormClosed;
            PritingLabel pritingLabel = new PritingLabel();
            double totalQty = 0;
            foreach (DataRow dr in TemporaryVariables.materialDT.Rows)
            {
                totalQty += Convert.ToDouble(dr["weight"].ToString());
            }
            FinalProductLabel finalProductLabel = new FinalProductLabel
            {
                product_code = lbFormulaName.Text,
                lot_no = TemporaryVariables.lotNo,
                total_qty = totalQty.ToString(),
                date_time = DateTime.Now.ToString("dd/MM/yyyy")
            };
            pritingLabel.PrintLabelQR(finalProductLabel, 1);
            Program.main.openAutomationTab();
        }
        private void btnProceedAutomation_Click(object sender, EventArgs e)
        {
            if(CheckMaterialLeft())
            {
                //LOTInput lOTInput = new LOTInput();
                //lOTInput.FormClosed += lOTInputFormClosed;
                //lOTInput.ShowDialog();
                Program.main.openAutomationTab();
            }
            else
            {
                if (TemporaryVariables.language == 0)
                {
                    message = "Vui lòng tiến hành quét mã và cân nguyên vật liệu!\r\nPlease proceed to scan the code and weigh the materials!";
                    caption = "Thông tin / Information";
                }
                else if (TemporaryVariables.language == 1)
                {
                    message = "Vui lòng tiến hành quét mã và cân nguyên vật liệu!\r\n请继续扫码称重材料！";
                    caption = "Thông tin / 空中";
                }
                else if (Settings.Default.language == 2)
                {
                    message = "Please proceed to scan the code and weigh the materials!";
                    caption = "Information";
                }
                else if (Settings.Default.language == 3)
                {
                    message = "Vui lòng tiến hành quét mã và cân nguyên vật liệu!";
                    caption = "Thông tin";
                }
                else if (Settings.Default.language == 4)
                {
                    message = "请继续扫码称重材料！";
                    caption = "空中";
                }
                CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool CheckMaterialLeft()
        {
            foreach (CustomMaterialDataRow c in flpMaterialList.Controls)
            {
                if (!c.Status)
                    return false;
            }
            if (Settings.Default.language == 0)
            {
                lbMaterialName.Text = "Đã hoàn thành xác nhận!\r\nConfirmation has completed!";
            }
            else if (Settings.Default.language == 1)
            {
                lbMaterialName.Text = "Đã hoàn thành xác nhận!\r\n已确认！";
            }
            else if (Settings.Default.language == 2)
            {
                lbMaterialName.Text = "Confirmation has completed!";
            }
            else if (Settings.Default.language == 3)
            {
                lbMaterialName.Text = "Đã hoàn thành xác nhận!";
            }
            else if (Settings.Default.language == 4)
            {
                lbMaterialName.Text = "已确认！";
            }
            return true;
        }
    }
}
