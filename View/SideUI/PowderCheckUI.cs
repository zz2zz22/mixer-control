using Microsoft.Office.Interop.Excel;
using mixer_control_globalver.Controller.Device;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace mixer_control_globalver.View.SideUI
{
    public partial class PowderCheckUI : Form, IMessageFilter
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private readonly StringBuilder _buffer = new StringBuilder();
        const int WM_CHAR = 0x0102;
        int _keyCount = 0;
        const int SCAN_MIN_LENGTH = 8;
        const double SECONDS_PER_CHARACTER_MIN_PERIOD = 0.3;

        //Test led
        int nRetCode = 0;
        int nNetProtocol = 1;
        int updatestyle = 1;
        bool save = false;
        bool flash = false;
        int ledcolour = 1;
        int width = 1;
        int higth = 1;
        string text = "";
        int colour = 1;
        int font = 1;
        int size = 1;
        string ip = "";
        int style = 1;
        int ledSpeed = 1;
        int stoptime = 1;
        int timeformat = 0;
        int showformat = 0;
        int nRotateMode = 0;

        int currentScan = 0;
        int totalMaterialType = 0;
        int currentMaterialNo = 0;
        string matCodeBefore, matCodeAfter;
        List<string> finalData = new List<string>();
        int step;
        int total;
        int stepTotal;
        int type;


        public PowderCheckUI(string materialCodeBefore, string materialCodeAfter, int currentStepNo, int totalPowder, int stepTotalPowder, int showType)
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
            Disposed += (sender, e) => Application.RemoveMessageFilter(this);

            matCodeBefore = materialCodeBefore;
            matCodeAfter = materialCodeAfter;
            step = currentStepNo;
            total = totalPowder;
            stepTotal = stepTotalPowder;
            type = showType;
        }

        //Methods
        private void SendLEDData(string data)
        {
            ip = SettingsManager.GetSetting(s => s.LedScreenIp);
            ledcolour = -1;
            width = 128;
            higth = 64;
            colour = SettingsManager.GetSetting(s => s.LedScreenColor);
            font = 1;
            size = 1;
            style = SettingsManager.GetSetting(s => s.LedScreenStyle);
            ledSpeed = 1;
            stoptime = 1;
            updatestyle = 1;
            timeformat = 1;
            showformat = 1;
            //UID = Convert.ToInt32(textBoxUID.Text.Trim());
            nRotateMode = 0;
            save = false;
            flash = false;
            nNetProtocol = 1;
            string[] linesText = data.Split(';');
            nRetCode = QYLED_DLL.ClearLED(2);
            for (int i = 0; i < 5; i++)
            {
                nRetCode = QYLED_DLL.SendInternalText_Net(linesText[i], ip, nNetProtocol, width, higth, i + 1, ledcolour, style, ledSpeed, stoptime, colour, font, size, updatestyle, save, nRotateMode);
            }
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
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
                            int countSharp = _buffer.ToString().Count(f => f == '#');
                            if (countSharp == 2)
                            {
                                try
                                {
                                    string[] bufferData = _buffer.ToString().Split('#');
                                    if (bufferData[0].ToString().Equals(lbMatCode.Text.Trim(), StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        string loadMaterialCode = finalData[currentMaterialNo].Split('#')[0];
                                        string loadMaterialQuantity = finalData[currentMaterialNo].Split('#')[1];
                                        string data = String.Empty;
                                        currentScan++;
                                        lbCurrentStatus.Text = currentScan + "/" + loadMaterialQuantity;
                                        data = lbMatCode.Text.Trim() + ";" + total + ";" + step + ";" + stepTotal + ";" + currentScan + "/" + loadMaterialQuantity;
                                        SendLEDData(data);
                                        if (currentScan == Convert.ToInt32(loadMaterialQuantity))
                                        {
                                            if (currentMaterialNo == totalMaterialType - 1)
                                            {
                                                this.Close();
                                            }
                                            else
                                            {
                                                currentScan = 0;
                                                currentMaterialNo++;
                                                loadMaterialCode = finalData[currentMaterialNo].Split('#')[0];
                                                loadMaterialQuantity = finalData[currentMaterialNo].Split('#')[1];
                                                lbMatCode.Text = loadMaterialCode;
                                                lbCurrentStatus.Text = currentScan + "/" + loadMaterialQuantity;
                                                data = loadMaterialCode + ";" + total + ";" + step + ";" + stepTotal + ";" + currentScan + "/" + loadMaterialQuantity;
                                                SendLEDData(data);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //lbAlert.Text = "Mã nguyên liệu không trùng khớp!";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append("QR code :" + _buffer.ToString() + "\r\n\r\n");
                                    sb.Append("##: <Tên> # <Lot> # <Trọng lượng>\r\n");
                                    sb.Append("Ví dụ: YZJ-HX-200#Test#20\r\n");
                                    sb.Append(ex.Message);
                                    CTMessageBox.Show(sb.ToString());
                                }
                            }
                        }
                    }
                });
        }

        private void PowderCheckUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearMemory();
            this.Dispose();
        }

        private void PowderCheckUI_Load(object sender, EventArgs e)
        {
            showDataInit();
        }


        private void showDataInit()
        {
            finalData = new List<string>();
            List<string> baseCode = new List<string>();
            switch (type)
            {
                case 0:
                    if (!string.IsNullOrEmpty(matCodeBefore))
                    {
                        if (matCodeBefore.Contains('&'))
                        {
                            baseCode = matCodeBefore.Split('&').ToList();
                        }
                        else
                            baseCode.Add(matCodeBefore);
                    }
                    if (!string.IsNullOrEmpty(matCodeAfter))
                    {
                        if (matCodeAfter.Contains('&'))
                        {
                            if (baseCode.Count == 0)
                            {
                                baseCode = matCodeAfter.Split('&').ToList();
                            }
                            else
                            {
                                foreach (string s in matCodeAfter.Split('&'))
                                    baseCode.Add(s);
                            }
                        }
                        else
                        {
                            baseCode.Add(matCodeAfter);
                        }
                    }

                    if (baseCode.Count > 1)
                        finalData = baseCode.Select(item => item.Split('#'))
        .GroupBy(parts => parts[0])
        .Select(g => $"{g.Key}#{g.Sum(x => int.Parse(x[1]))}")
        .ToList();

                    if (finalData.Count > 0)
                    {
                        string listMaterial = string.Empty;
                        for (int k = 0; k < finalData.Count; k++)
                        {
                            if (k == 0)
                                listMaterial = finalData[k].Split('#')[0] + "(" + finalData[k].Split('#')[1] + " bao)";
                            else
                                listMaterial += ", " + finalData[k].Split('#')[0] + "(" + finalData[k].Split('#')[1] + " bao)";
                        }
                        totalMaterialType = finalData.Count;
                        lbAlert.Text = "Bước này cần cấp " + totalMaterialType + " loại bột (此步骤需要投入" + totalMaterialType + "袋面粉):" + listMaterial;
                    }
                    else
                    {
                        this.Close();
                    }
                    break;
                case 1:
                case 2:
                    if (type == 1)
                    {
                        if (!string.IsNullOrEmpty(matCodeBefore))
                        {
                            if (matCodeBefore.Contains('&'))
                            {
                                baseCode = matCodeBefore.Split('&').ToList();
                            }
                            else
                                baseCode.Add(matCodeBefore);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(matCodeAfter))
                        {
                            if (matCodeAfter.Contains('&'))
                            {
                                baseCode = matCodeAfter.Split('&').ToList();
                            }
                            else
                                baseCode.Add(matCodeAfter);
                        }
                    }

                    for (int i = 0; i < baseCode.Count; i++)
                    {
                        if (finalData.Count == 0)
                        {
                            finalData.Add(baseCode[i]);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(baseCode[i]) && baseCode[i].Contains('#'))
                            {
                                string[] splitDetail = baseCode[i].Split('#');
                                if (finalData.Contains(splitDetail[0]))
                                {
                                    for (int y = 0; y < finalData.Count; y++)
                                    {
                                        if (finalData[i].Contains(splitDetail[0]))
                                        {
                                            string[] splitFoundData = finalData[i].Split('#');
                                            int total = Convert.ToInt32(splitDetail[1]) + Convert.ToInt32(splitFoundData[1]);
                                            finalData[i] = finalData[0] + total;
                                        }
                                    }
                                }
                                else
                                {
                                    finalData.Add(baseCode[i]);
                                }
                            }
                        }
                    }
                    if (finalData.Count > 0)
                    {
                        string listMaterial = string.Empty;
                        for (int k = 0; k < finalData.Count; k++)
                        {
                            if (k == 0)
                                listMaterial = finalData[k].Split('#')[0] + "(" + finalData[k].Split('#')[1] + " bao)";
                            else
                                listMaterial += ", " + finalData[k].Split('#')[0] + "(" + finalData[k].Split('#')[1] + " bao)";
                        }
                        totalMaterialType = finalData.Count;
                        if (type == 1)
                            lbAlert.Text = "Trước khi cấp dầu cần cấp " + totalMaterialType + " loại bột (加油之前需要先投入" + totalMaterialType + "袋面粉): " + listMaterial;
                        else
                            lbAlert.Text = "Sau khi cấp dầu cần cấp " + totalMaterialType + " loại bột (加油之后需要再投入" + totalMaterialType + "袋面粉): " + listMaterial;
                    }
                    else
                    {
                        this.Close();
                    }
                    break;
            }

            currentScan = 0;
            currentMaterialNo = 0;
            if (finalData.Count == 0)
            {
                this.Close();
                return;
            }
            string[] trueData = finalData[currentMaterialNo].Split('#');
            string loadMaterialCode = trueData[0];
            string loadMaterialQuantity = trueData[1];

            lbStepNo.Text = step.ToString();
            lbMatCode.Text = loadMaterialCode;
            lbTotal.Text = total.ToString();
            lbStepTotal.Text = stepTotal.ToString();
            lbCurrentStatus.Text = currentScan + "/" + loadMaterialQuantity;
            string data = loadMaterialCode + ";" + total + ";" + step + ";" + stepTotal + ";" + currentScan + "/" + loadMaterialQuantity;
            SendLEDData(data);
        }
    }
}
