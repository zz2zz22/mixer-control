using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.LogFile;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomControls;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mixer_control_globalver.View.MainUI
{
    public partial class MaterialScale : Form, IMessageFilter
    {
        int totalMaterial = 0;

        private readonly StringBuilder _buffer = new StringBuilder();
        const int WM_CHAR = 0x0102;
        int _keyCount = 0;
        const int SCAN_MIN_LENGTH = 8;
        const double SECONDS_PER_CHARACTER_MIN_PERIOD = 0.3;

        public MaterialScale()
        {
            switch (SettingsManager.GetSetting(s => s.Language))
            {
                case 0:
                    SubMethods.SetLanguage("vi-VN");
                    break;
                case 1:
                    SubMethods.SetLanguage("zh-CN");
                    break;
                case 2:
                    SubMethods.SetLanguage("en-US");
                    break;
                default:
                    SubMethods.SetLanguage("");
                    break;
            }
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
                                if (data[0].ToUpper() == lbFormulaName.Text.Trim().ToUpper() || lbFormulaName.Text.Trim().ToUpper().Contains(data[0].ToUpper()))
                                {
                                    TemporaryVariables.tempFormulaLOT = data[1];
                                    if (!String.IsNullOrEmpty(data[3]))
                                        totalMaterial = Convert.ToInt32(data[3]);
                                    
                                    CTMessageBox.Show(GlobalStrings.Message_PDF417Scanned, GlobalStrings.MessageBoxTitle_Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    CTMessageBox.Show(GlobalStrings.Message_PDF417NotMatch, GlobalStrings.MessageBoxTitle_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Program.main.openSpecTab();
                                }
                            }
                            else if (countSemiColon >= 3)
                            {
                                if (totalMaterial == 0)
                                {
                                    CTMessageBox.Show(GlobalStrings.Message_NotScanPDF417, GlobalStrings.MessageBoxTitle_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    string caseSensitive = _buffer.ToString().ToLower().Trim();
                                    string result = SubMethods.TrimSpecialCharacters(caseSensitive);
                                    string[] data = result.Split(';');
                                    if (data.Length > 3)
                                    {
                                        DataRow[] foundAuthors = TemporaryVariables.materialDT.Select("mat_name = '" + data[0].ToUpper() + "' and id = '" + data[1] + "'");
                                        if (foundAuthors.Length == 0)
                                        {
                                            TemporaryVariables.materialDT.Rows.Add(data[0].ToUpper(), Convert.ToInt32(data[1]), Convert.ToDouble(data[2]), data[3]);
                                            CustomMaterialDataRow customMaterial = new CustomMaterialDataRow(data[0].ToUpper(), data[2], data[3]);
                                            lbJustConfirm.Text = data[0].ToUpper();
                                            lbConfirmAmount.Text = TemporaryVariables.materialDT.Rows.Count.ToString() + "/" + totalMaterial;
                                            flpMaterialList.Controls.Add(customMaterial);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            CTMessageBox.Show(GlobalStrings.Error_CanNotRecognizeQR, GlobalStrings.MessageBoxTitle_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                for (int i = 0; i < TemporaryVariables.materialDT.Rows.Count; i++)
                {
                    CustomMaterialDataRow customMaterial = new CustomMaterialDataRow(TemporaryVariables.materialDT.Rows[i]["mat_name"].ToString(), TemporaryVariables.materialDT.Rows[i]["weight"].ToString(), TemporaryVariables.materialDT.Rows[i]["lot_no"].ToString());
                    flpMaterialList.Controls.Add(customMaterial);
                }
            }
            totalMaterial = TemporaryVariables.materialDT.Rows.Count;

            lb1.Text = GlobalStrings.Label_ScannedMaterialList;
            lb1.Font = new Font(GlobalStrings.Text_Font, lb1.Font.Size, lb1.Font.Style);
            lb2.Text = GlobalStrings.Label_ScannedMaterialAmount;
            lb2.Font = new Font(GlobalStrings.Text_Font, lb2.Font.Size, lb2.Font.Style);
            lb3.Text = GlobalStrings.Label_ScannedMaterialFormulaName;
            lb3.Font = new Font(GlobalStrings.Text_Font, lb3.Font.Size, lb3.Font.Style);
            lb4.Text = GlobalStrings.Label_ScannedMaterialCode;
            lb4.Font = new Font(GlobalStrings.Text_Font, lb4.Font.Size, lb4.Font.Style);
            btnProceedAutomation.ButtonText = GlobalStrings.btnContinueAutomation_Text;
            btnProceedAutomation.Font = new Font(GlobalStrings.Text_Font, btnProceedAutomation.Font.Size, btnProceedAutomation.Font.Style);
        }

        private void btnProceedAutomation_Click(object sender, EventArgs e)
        {
            if (!SettingsManager.GetSetting(s => s.DeveloperMode))
            {
                if (TemporaryVariables.materialDT.Rows.Count == totalMaterial)
                    Program.main.openAutomationTab();
                else
                {
                    CTMessageBox.Show(GlobalStrings.Message_NotScanAllMaterial, GlobalStrings.MessageBoxTitle_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                Program.main.openAutomationTab();
            }
        }
    }
}
