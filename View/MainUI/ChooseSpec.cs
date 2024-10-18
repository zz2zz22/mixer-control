using Microsoft.WindowsAPICodePack.Dialogs;
using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.LogFile;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomComponent;
using mixer_control_globalver.View.CustomControls;
using mixer_control_globalver.View.SideUI;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;

namespace mixer_control_globalver.View.MainUI
{
    public partial class ChooseSpec : Form
    {
        //Fields
        string message = String.Empty, caption = String.Empty;
        public static bool isConfirmed;

        public ChooseSpec()
        {
            InitializeComponent();
        }
        #region Methods
        //Load list cac file cong thuc
        private void LoadItemFilePath(string path)
        {
            try
            {
                dtgvListSpecification.DataSource = null;
                DataTable dt = new DataTable();
                dt.Columns.Add("file_name", typeof(string));
                dt.Columns.Add("file_path", typeof(string));
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
                if (dir.Exists == false)
                    dir.Create();
                else
                {
                    var files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".xls", StringComparison.OrdinalIgnoreCase));
                    foreach (string fileName in files)
                    {
                        dt.Rows.Add(Path.GetFileNameWithoutExtension(fileName), fileName);
                    }
                }

                dtgvListSpecification.DataSource = dt;
                if (Settings.Default.language == 0)
                {
                    dtgvListSpecification.Columns["file_name"].HeaderText = "Tên tệp";
                }
                else if (Settings.Default.language == 1)
                {
                    dtgvListSpecification.Columns["file_name"].HeaderText = "产品型号名称";
                }
                else if (Settings.Default.language == 2)
                {
                    dtgvListSpecification.Columns["file_name"].HeaderText = "File name";
                }
                dtgvListSpecification.Columns["file_path"].Visible = false;
            }
            catch (Exception ex)
            {
                if (Settings.Default.language == 0)
                {
                    message = "Không thể tải dữ liệu tệp!" + "\r\n\r\n" + ex.Message;
                    caption = "Lỗi";
                }
                else if (Settings.Default.language == 1)
                {
                    message = "Load dirctory files failed!" + "\r\n\r\n" + ex.Message;
                    caption = "Error";
                }
                else if (Settings.Default.language == 2)
                {
                    message = "上传产品型号失败！" + "\r\n\r\n" + ex.Message;
                    caption = "错误";
                }
                SystemLog.Output(SystemLog.MSG_TYPE.Err, caption, message);
                CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        private void ChooseSpec_Load(object sender, EventArgs e)
        {
            if (Settings.Default.language == 0)
            {
                lb1.Text = "Công thức đã chọn:";
                lb2.Text = "Danh sách công thức:";

                btnConfirmChoose.ButtonText = "Tiến hành xác nhận liệu";
                btnCheckProcess.ButtonText = "Xem quy trình";
            }
            else if (Settings.Default.language == 1)
            {
                lb1.Text = "选定的配方:";
                lb2.Text = "产品型号列表:";

                btnConfirmChoose.ButtonText = "开始材料确认。";
                btnCheckProcess.ButtonText = "检查流程步骤";
            }
            else if (Settings.Default.language == 2)
            {
                lb1.Text = "Selected Formula:";
                lb2.Text = "Formula setting files:";

                btnConfirmChoose.ButtonText = "Begin Material Confirmation";
                btnCheckProcess.ButtonText = "Check process step";
            }

            Properties.Settings.Default.isEndReport = true;
            Properties.Settings.Default.Save();
            try
            {
                if (String.IsNullOrEmpty(Properties.Settings.Default.folder_directory))
                {
                    string dirPath = AppDomain.CurrentDomain.BaseDirectory + "\\InputData";
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(dirPath);
                    if (dir.Exists == false)
                        dir.Create();

                    Properties.Settings.Default.folder_directory = dir.FullName;
                    Properties.Settings.Default.Save();
                }
                LoadItemFilePath(Properties.Settings.Default.folder_directory);

                TemporaryVariables.resetAllTempVariables();
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Nor, "Error load file", ex.Message);
            }
        }

        private void saveFileLocationPassFormClosed(object sender, EventArgs e)
        {
            ((Form)sender).FormClosed -= saveFileLocationPassFormClosed;
            if (isConfirmed)
            {
                isConfirmed = false;
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.InitialDirectory = Properties.Settings.Default.folder_directory;
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    Properties.Settings.Default.folder_directory = dialog.FileName;
                    Properties.Settings.Default.Save();
                }
                LoadItemFilePath(Properties.Settings.Default.folder_directory);
                SubMethods.BackupUserSettings();
            }
        }

        private void picbtnChooseDirectory_Click(object sender, EventArgs e)
        {
            PasswordConfirm passwordConfirm = new PasswordConfirm();
            passwordConfirm.FormClosed += saveFileLocationPassFormClosed;
            passwordConfirm.ShowDialog();
        }

        private void dtgvListSpecification_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool isSuccess = false;
            LoadingDialog loading = new LoadingDialog();
            Thread backgroundThreadSaveData = new Thread(
                    new ThreadStart(() =>
                    {
                        if (dtgvListSpecification.SelectedCells.Count > 0)
                        {
                            try
                            {
                                int selectedrowindex = dtgvListSpecification.SelectedCells[0].RowIndex;
                                DataGridViewRow selectedRow = dtgvListSpecification.Rows[selectedrowindex];
                                TemporaryVariables.tempFileName = Convert.ToString(selectedRow.Cells["file_name"].Value);
                                TemporaryVariables.tempFilePath = Convert.ToString(selectedRow.Cells["file_path"].Value);

                                //Save variable to Datatable
                                TemporaryVariables.InitMaterialDT();
                                TemporaryVariables.InitProcessDT();
                                DataTable processDT = SubMethods.ImportExceltoDatatable(TemporaryVariables.tempFilePath);
                                if (processDT != null)
                                {
                                    if (processDT.Rows.Count > 0)
                                    {
                                        for (int j = 0; j < processDT.Rows.Count; j++)
                                        {
                                            if (!String.IsNullOrEmpty(processDT.Rows[j][0].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][1].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][2].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][5].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][6].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][7].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][8].ToString()))
                                            {
                                                int changeSpeed = 0, changeTime = 0, totalPowder = 0, remainPowder = 0;
                                                double oilMass = 0, oilWeight = 0;
                                                bool isVaccum = false, isSkipAnnounce = false, isOilFeed = false;

                                                if (!String.IsNullOrEmpty(processDT.Rows[j][3].ToString()))
                                                    changeSpeed = Convert.ToInt32(processDT.Rows[j][3].ToString());

                                                if (!String.IsNullOrEmpty(processDT.Rows[j][4].ToString()))
                                                    changeTime = Convert.ToInt32(processDT.Rows[j][4].ToString());

                                                if (processDT.Rows[j][5].ToString().ToLower() == "yes")
                                                    isVaccum = true;

                                                if (processDT.Rows[j][7].ToString().ToLower() == "yes")
                                                    isSkipAnnounce = true;

                                                if (processDT.Rows[j][8].ToString().ToLower() == "yes")
                                                    isOilFeed = true;

                                                if (Settings.Default.isOilFeed && isOilFeed)
                                                {
                                                    if (!string.IsNullOrEmpty(processDT.Rows[j][10].ToString()) && !string.IsNullOrEmpty(processDT.Rows[j][9].ToString()))
                                                    {
                                                        oilMass = double.Parse(processDT.Rows[j][10].ToString(), CultureInfo.InvariantCulture);
                                                        oilWeight = double.Parse(processDT.Rows[j][9].ToString(), CultureInfo.InvariantCulture);
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("Oil mass or oil weight data is empty, please check the formula!");
                                                    }
                                                }

                                                if (Settings.Default.isAlertPowder)
                                                {
                                                    if (!String.IsNullOrEmpty(processDT.Rows[j][13].ToString()) && !String.IsNullOrEmpty(processDT.Rows[j][14].ToString()))
                                                    {
                                                        //Edit to read total powder bags
                                                        totalPowder = Convert.ToInt32(processDT.Rows[j][13].ToString());
                                                        remainPowder = Convert.ToInt32(processDT.Rows[j][14].ToString());
                                                    }
                                                    else
                                                    {
                                                        totalPowder = 0;
                                                        remainPowder = 0;
                                                    }
                                                }

                                                string stepDesc = processDT.Rows[j][12].ToString();
                                                string oilType = processDT.Rows[j][11].ToString();

                                                TemporaryVariables.processDT.Rows.Add(processDT.Rows[j][0].ToString(),
                                                processDT.Rows[j][1].ToString(),
                                                processDT.Rows[j][2].ToString(),
                                                changeSpeed,
                                                changeTime,
                                                isVaccum,
                                                processDT.Rows[j][6].ToString(),
                                                isSkipAnnounce,
                                                stepDesc,
                                                false,
                                                isOilFeed,
                                                oilMass,
                                                oilWeight,
                                                oilType,
                                                totalPowder,
                                                remainPowder);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    string exMessage;
                                    switch (Settings.Default.language)
                                    {
                                        case 0:
                                            exMessage = "Không thể mở file hoặc mất kết nối đến server, vui lòng kiểm tra file hoặc kết nối internet!";
                                            break;
                                        case 1:
                                            exMessage = "无法打开文件或与服务器的连接丢失，请检查文件或互联网连接！";
                                            break;
                                        case 2:
                                            exMessage = "Unable to open file or lost connection to server, please check file or internet connection!";
                                            break;
                                        default:
                                            exMessage = "Không thể mở file hoặc mất kết nối đến server, vui lòng kiểm tra file hoặc kết nối internet!";
                                            break;
                                    }
                                    throw new Exception(exMessage);
                                }
                                isSuccess = true;
                                loading.BeginInvoke(new Action(() => loading.Close()));
                            }
                            catch (Exception ex)
                            {
                                isSuccess = false;
                                loading.BeginInvoke(new Action(() => loading.Close()));
                                if (Settings.Default.language == 0)
                                {
                                    message = "Lỗi khi tải dữ liệu excel!" + "\r\n\r\n" + ex.Message;
                                    caption = "Lỗi";
                                }
                                else if (Settings.Default.language == 1)
                                {
                                    message = "下载EXCEL失败!" + "\r\n\r\n" + ex.Message;
                                    caption = "错误";
                                }
                                else if (Settings.Default.language == 2)
                                {
                                    message = "Load Excel data failed!" + "\r\n\r\n" + ex.Message;
                                    caption = "Error";
                                }
                                TemporaryVariables.resetAllTempVariables();

                                SystemLog.Output(SystemLog.MSG_TYPE.Err, caption, message);
                                CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }));
            if (backgroundThreadSaveData.IsAlive)
                backgroundThreadSaveData.Join();
            backgroundThreadSaveData.Start();
            loading.ShowDialog();
            if (isSuccess)
                lbFormulaName.Text = TemporaryVariables.tempFileName;
            else
            {
                lbFormulaName.Text = "";
            }
        }

        private void btnRefreshFileList_Click(object sender, EventArgs e)
        {
            LoadItemFilePath(Properties.Settings.Default.folder_directory);
        }

        private void btnConfirmChoose_Click(object sender, EventArgs e)
        {
            Program.main.openScaleTab();
        }

        private void btnImportTemplate_Click(object sender, EventArgs e)
        {
            PasswordConfirm passwordConfirm = new PasswordConfirm();
            passwordConfirm.FormClosed += importExcelPassFormClosed;
            passwordConfirm.ShowDialog();
        }

        private void importExcelPassFormClosed(object sender, EventArgs e)
        {
            ((Form)sender).FormClosed -= importExcelPassFormClosed;
            if (isConfirmed)
            {
                isConfirmed = false;
                try
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    if (Settings.Default.language == 0)
                    {
                        fileDialog.Title = "Nhập file công thức";
                    }
                    else if (Settings.Default.language == 1)
                    {
                        fileDialog.Title = "导入公式文件";
                    }
                    else if (Settings.Default.language == 2)
                    {
                        fileDialog.Title = "Import formula";
                    }
                    fileDialog.DefaultExt = "Excel";
                    fileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    fileDialog.CheckPathExists = true;
                    fileDialog.Multiselect = true;
                    fileDialog.InitialDirectory = "C:\\";

                    if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        foreach (string _file in fileDialog.FileNames)
                        {
                            FileInfo d = new FileInfo(_file);
                            if (File.Exists(Path.Combine(Properties.Settings.Default.folder_directory + "\\" + d.Name)))
                            {
                                File.Delete(Path.Combine(Properties.Settings.Default.folder_directory + "\\" + d.Name));
                            }
                            File.Move(_file, Path.Combine(Properties.Settings.Default.folder_directory + "\\" + d.Name));
                        }
                        if (Settings.Default.language == 0)
                        {
                            message = "Thêm công thức thành công!";
                            caption = "Thông tin";
                        }
                        else if (Settings.Default.language == 1)
                        {
                            message = "更多成功秘诀！";
                            caption = "信息";
                        }
                        else if (Settings.Default.language == 2)
                        {
                            message = "Successfully import formula !";
                            caption = "Information";
                        }
                        CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadItemFilePath(Properties.Settings.Default.folder_directory);
                    }
                }
                catch (Exception ex)
                {
                    if (Settings.Default.language == 0)
                    {
                        message = "Thêm công thức thất bại !" + "\r\n\r\n" + ex.Message;
                        caption = "Lỗi";
                    }
                    else if (Settings.Default.language == 1)
                    {
                        message = "更多失败的食谱！" + "\r\n\r\n" + ex.Message;
                        caption = "错误";
                    }
                    else if (Settings.Default.language == 2)
                    {
                        message = "Failed to import formula!" + "\r\n\r\n" + ex.Message;
                        caption = "Error";
                    }
                    LoadItemFilePath(Properties.Settings.Default.folder_directory);
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, caption, message);
                    CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txbSearchFormula_TextChanged(object sender, EventArgs e)
        {
            if (dtgvListSpecification.Rows.Count > 0)
            {
                string searchForText = txbSearchFormula.Text.Trim();
                CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dtgvListSpecification.DataSource];
                currencyManager1.SuspendBinding();
                for (int i = 0; i < dtgvListSpecification.Rows.Count; i++)
                {
                    if (dtgvListSpecification.Rows[i].Cells[0].Value.ToString().ToLower().Contains(searchForText))
                    {
                        dtgvListSpecification.Rows[i].Selected = true;
                        dtgvListSpecification.Rows[i].Visible = true;
                    }
                    else
                    {
                        dtgvListSpecification.Rows[i].Visible = false;
                        dtgvListSpecification.Rows[i].Selected = false;
                    }
                }
                currencyManager1.ResumeBinding();
            }
        }

        private void btnTestOilFeed_Click(object sender, EventArgs e)
        {
            if (!Settings.Default.isOilTested || Settings.Default.isTestOilMultiple)
            {
                if (Settings.Default.isOilFeed)
                {
                    OilFeederTest oilFeederTest = new OilFeederTest();
                    oilFeederTest.ShowDialog();
                }
                else
                {
                    switch (Settings.Default.language)
                    {
                        case 0:
                            message = "Máy không được kích hoạt chế độ cấp dầu!";
                            caption = "Cảnh báo";
                            break;
                        case 1:
                            message = "机器未激活供油模式！";
                            caption = "警报";
                            break;
                        case 2:
                            message = "The machine does not activate oil supply mode!";
                            caption = "Alert";
                            break;
                        default:
                            message = "Máy không được kích hoạt chế độ cấp dầu!";
                            caption = "Cảnh báo";
                            break;
                    }
                    CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                switch (Settings.Default.language)
                {
                    case 0:
                        message = "Máy đã được test cấp dầu!";
                        caption = "Cảnh báo";
                        break;
                    case 1:
                        message = "本机已经过油泵测试！";
                        caption = "警报";
                        break;
                    case 2:
                        message = "The machine has been tested for oil pump!";
                        caption = "Alert";
                        break;
                    default:
                        message = "Máy đã được test cấp dầu!";
                        caption = "Cảnh báo";
                        break;
                }
                CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCheckProcess_Click(object sender, EventArgs e)
        {
            if (TemporaryVariables.processDT != null && TemporaryVariables.processDT.Rows.Count > 0)
            {
                CheckFormulaProcess checkFormula = new CheckFormulaProcess();
                checkFormula.ShowDialog();
            }
            else
            {
                if (Settings.Default.language == 0)
                {
                    message = "Chưa chọn công thức hoặc công thức không có dữ liệu !";
                    caption = "Lỗi";
                }
                else if (Settings.Default.language == 1)
                {
                    message = "未选择公式或公式没有数据！";
                    caption = "错误";
                }
                else if (Settings.Default.language == 2)
                {
                    message = "No formula has been selected or the formula has no data!";
                    caption = "Error";
                }
                CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
