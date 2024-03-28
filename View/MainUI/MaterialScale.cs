using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.LogFile;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace mixer_control_globalver.View.MainUI
{
    public partial class MaterialScale : Form, IMessageFilter
    {
        int totalMaterial = 0;

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

        //Methods
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
                            int countSharp = _buffer.ToString().Count(f => f == '#');
                            int countSemiColon = _buffer.ToString().Count(f => f == ';');
                            if (countSharp >= 3)
                            {
                                string[] data = _buffer.ToString().Split('#');
                                if (data[0] == lbFormulaName.Text.Trim() || lbFormulaName.Text.Trim().Contains(data[0]))
                                {
                                    if (!String.IsNullOrEmpty(data[3]))
                                        totalMaterial = Convert.ToInt32(data[3]);
                                }
                                else
                                {
                                    if (Settings.Default.language == 0)
                                    {
                                        message = "Công thức không trùng khớp!";
                                        caption = "Cảnh báo";
                                    }
                                    else if (Settings.Default.language == 1)
                                    {
                                        message = "公式不符！";
                                        caption = "警报";
                                    }
                                    else if (Settings.Default.language == 2)
                                    {
                                        message = "Formula is not match!";
                                        caption = "Warning";
                                    }
                                    CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Program.main.openSpecTab();
                                }
                            }
                            else if (countSemiColon >= 3)
                            {
                                if (totalMaterial == 0)
                                {
                                    if (Settings.Default.language == 0)
                                    {
                                        message = "Vui lòng quét mã vạch của công thức trước!";
                                        caption = "Cảnh báo";
                                    }
                                    else if (Settings.Default.language == 1)
                                    {
                                        message = "请先扫描菜谱条形码！";
                                        caption = "警报";
                                    }
                                    else if (Settings.Default.language == 2)
                                    {
                                        message = "Please scan the formula barcode first!";
                                        caption = "Warning";
                                    }
                                    CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    string result = _buffer.ToString().Substring(_buffer.ToString().IndexOf("s") + 1, _buffer.ToString().LastIndexOf("e") - _buffer.ToString().IndexOf("s") - 1);
                                    string[] data = result.Split(';');
                                    if (data.Length > 3)
                                    {
                                        DataRow[] foundAuthors = TemporaryVariables.materialDT.Select("mat_name = '" + data[0] + "' and id = '" + data[1] +"'");
                                        if (foundAuthors.Length == 0)
                                        {
                                            TemporaryVariables.materialDT.Rows.Add(data[0], Convert.ToInt32(data[1]), Convert.ToDouble(data[2]), data[3]);
                                            CustomMaterialDataRow customMaterial = new CustomMaterialDataRow(data[0], data[2], data[3]);
                                            lbJustConfirm.Text = data[0];
                                            lbConfirmAmount.Text = TemporaryVariables.materialDT.Rows.Count.ToString() + "/" + totalMaterial;
                                            flpMaterialList.Controls.Add(customMaterial);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Settings.Default.language == 0)
                            {
                                message = "Không thể nhận dạng mã QR!";
                                caption = "Lỗi";
                            }
                            else if (Settings.Default.language == 1)
                            {
                                message = "无法识别二维码！";
                                caption = "错误";
                            }
                            else if (Settings.Default.language == 2)
                            {
                                message = "Can not read QR code!";
                                caption = "Error";
                            }
                            SystemLog.Output(SystemLog.MSG_TYPE.Err, caption, message);
                            CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                });
        }

        private void MaterialScale_Load(object sender, EventArgs e)
        {
            lbFormulaName.Text = TemporaryVariables.tempFileName;
            if (TemporaryVariables.materialDT.Rows.Count > 0)
            {
                flpMaterialList.Controls.Clear();
                for (int i= 0; i < TemporaryVariables.materialDT.Rows.Count; i ++)
                {
                    CustomMaterialDataRow customMaterial = new CustomMaterialDataRow(TemporaryVariables.materialDT.Rows[i]["mat_name"].ToString(), TemporaryVariables.materialDT.Rows[i]["weight"].ToString(), TemporaryVariables.materialDT.Rows[i]["lot_no"].ToString());
                    flpMaterialList.Controls.Add(customMaterial);
                }
            }     
            totalMaterial = TemporaryVariables.materialDT.Rows.Count;
            if (Settings.Default.language == 0)
            {
                lb1.Text = "Danh sách nguyên vật liệu đã xác nhận:";
                lb2.Text = "Số nguyên vật liệu đã xác nhận:";
                lb3.Text = "Công thức:";
                lb4.Text = "Nguyên vật liệu vừa xác nhận:";
                btnProceedAutomation.ButtonText = "Tiến hành chạy tự động";
            }
            else if (Settings.Default.language == 1)
            {
                lb1.Text = "确认材料清单：";
                lb2.Text = "确认材料数量：";
                lb3.Text = "型号:";
                lb4.Text = "最新确认材料：";
                btnProceedAutomation.ButtonText = "开始运行";
            }
            else if (Settings.Default.language == 2)
            {
                lb1.Text = "Confirmed materials list:";
                lb2.Text = "Confirmed materials amount:";
                lb3.Text = "Formula:";
                lb4.Text = "Latest confirmed material:";
                btnProceedAutomation.ButtonText = "Begin automation process";
            }
        }

        private void btnProceedAutomation_Click(object sender, EventArgs e)
        {
            if(!Properties.Settings.Default.isTesting)
            {
                if (TemporaryVariables.materialDT.Rows.Count == totalMaterial)
                    Program.main.openAutomationTab();
                else
                {
                    if (Settings.Default.language == 0)
                    {
                        message = "Chưa quét đủ số lượng nguyên vật liệu. Vui lòng kiểm tra lại.";
                        caption = "Cảnh báo";
                    }
                    else if (Settings.Default.language == 1)
                    {
                        message = "没有扫描足够的材料。请再检查一次。";
                        caption = "警报";
                    }
                    else if (Settings.Default.language == 2)
                    {
                        message = "Not scanning enough materials. Please check again.";
                        caption = "Warning";
                    }
                    CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                Program.main.openAutomationTab();
            }
        }
    }
}
