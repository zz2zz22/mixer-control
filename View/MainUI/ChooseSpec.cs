using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.WindowsAPICodePack.Dialogs;
using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.LogFile;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomComponent;
using mixer_control_globalver.View.CustomControls;
using Spire.Xls;


namespace mixer_control_globalver.View.MainUI
{
    public partial class ChooseSpec : Form
    {
        string message = String.Empty, caption = String.Empty;
        string loadMsg = String.Empty;
        public static bool isConfirmed;
        public ChooseSpec()
        {
            InitializeComponent();
        }

        private void ChooseSpec_Load(object sender, EventArgs e)
        {
            if (TemporaryVariables.language == 0)
            {
                lb2.Text = "Danh sách công thức:\r\nFormula setting files:";
                lb3.Text = "Danh sách liệu:\r\nMaterials list:";
                lb4.Text = "Danh sách quy trình:\r\nProcess list:";

                btnConfirmChoose.ButtonText = "Tiến hành xác nhận liệu\r\nBegin Material Confirmation";
                btnGetTemplate.ButtonText = "Tải mẫu\r\nDownload template";
                btnImportTemplate.ButtonText = "Nhập công thức\r\nImport formula";
            }
            else if (TemporaryVariables.language == 1)
            {
                lb2.Text = "Danh sách công thức:\r\n产品型号列表:";
                lb3.Text = "Danh sách liệu:\r\n原料列表:";
                lb4.Text = "Danh sách quy trình:\r\n操作步骤:";

                btnConfirmChoose.ButtonText = "Tiến hành xác nhận liệu\r\n开始材料确认。";
                btnGetTemplate.ButtonText = "Tải mẫu\r\n下载模板";
                btnImportTemplate.ButtonText = "Nhập công thức\r\n输入公式";
            }
            else if (Settings.Default.language == 2)
            {
                lb2.Text = "Formula setting files:";
                lb3.Text = "Materials list:";
                lb4.Text = "Process list:";

                btnConfirmChoose.ButtonText = "Begin Material Confirmation";
                btnGetTemplate.ButtonText = "Download template";
                btnImportTemplate.ButtonText = "Import formula";
            }
            else if (Settings.Default.language == 3)
            {
                lb2.Text = "Danh sách công thức:";
                lb3.Text = "Danh sách liệu:";
                lb4.Text = "Danh sách quy trình:";

                btnConfirmChoose.ButtonText = "Tiến hành xác nhận liệu";
                btnGetTemplate.ButtonText = "Tải mẫu";
                btnImportTemplate.ButtonText = "Nhập công thức";
            }
            else if (Settings.Default.language == 4)
            {
                lb2.Text = "产品型号列表:";
                lb3.Text = "原料列表:";
                lb4.Text = "操作步骤:";

                btnConfirmChoose.ButtonText = "开始材料确认。";
                btnGetTemplate.ButtonText = "下载模板";
                btnImportTemplate.ButtonText = "输入公式";
            }

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
                if (TemporaryVariables.language == 0)
                {
                    dtgvListSpecification.Columns["file_name"].HeaderText = "Tên tệp\r\nFile name";
                }
                else if (TemporaryVariables.language == 1)
                {
                    dtgvListSpecification.Columns["file_name"].HeaderText = "Tên tệp\r\n产品型号名称";
                }
                else if (Settings.Default.language == 2)
                {
                    dtgvListSpecification.Columns["file_name"].HeaderText = "File name";
                }
                else if (Settings.Default.language == 3)
                {
                    dtgvListSpecification.Columns["file_name"].HeaderText = "Tên tệp";
                }
                else if (Settings.Default.language == 4)
                {
                    dtgvListSpecification.Columns["file_name"].HeaderText = "产品型号名称";
                }
                dtgvListSpecification.Columns["file_path"].Visible = false;
            }
            catch (Exception ex)
            {
                if (TemporaryVariables.language == 0)
                {
                    message = "Không thể tải dữ liệu tệp!\r\nLoad dirctory files failed!" + "\r\n\r\n" + ex.Message;
                    caption = "Lỗi / Error";
                }
                else if (TemporaryVariables.language == 1)
                {
                    message = "Không thể tải dữ liệu tệp!\r\n上传产品型号失败！" + "\r\n\r\n" + ex.Message;
                    caption = "Lỗi / 错误";
                }
                else if (Settings.Default.language == 2)
                {
                    message = "Load dirctory files failed!" + "\r\n\r\n" + ex.Message;
                    caption = "Error";
                }
                else if (Settings.Default.language == 3)
                {
                    message = "Không thể tải dữ liệu tệp!" + "\r\n\r\n" + ex.Message;
                    caption = "Lỗi";
                }
                else if (Settings.Default.language == 4)
                {
                    message = "上传产品型号失败！" + "\r\n\r\n" + ex.Message;
                    caption = "错误";
                }
                CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dtgvListSpecification.SelectedCells.Count > 0)
            {
                try
                {
                    int selectedrowindex = dtgvListSpecification.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dtgvListSpecification.Rows[selectedrowindex];
                    TemporaryVariables.tempFileName = Convert.ToString(selectedRow.Cells["file_name"].Value);
                    TemporaryVariables.tempFilePath = Convert.ToString(selectedRow.Cells["file_path"].Value);

                    //Save variable to Datatable
                    Worksheet materialSheet = null, processSheet = null;
                    DataTable matDT = new DataTable();
                    DataTable processDT = new DataTable();

                    TemporaryVariables.InitMaterialDT();
                    TemporaryVariables.InitProcessDT();
                    Workbook workbook = new Workbook();
                    workbook.LoadFromFile(TemporaryVariables.tempFilePath);
                    materialSheet = workbook.Worksheets["material_info"];
                    processSheet = workbook.Worksheets["process_info"];

                    if (materialSheet != null && processSheet != null)
                    {
                        matDT = materialSheet.ExportDataTable(materialSheet.AllocatedRange, true, true);
                        processDT = processSheet.ExportDataTable(processSheet.AllocatedRange, true, true);

                        if (matDT.Rows.Count > 0 && processDT.Rows.Count > 0)
                        {
                            LoadingDialog loadingDialog = new LoadingDialog();
                            Thread backgroundThreadGetData = new Thread(
                                new ThreadStart(() =>
                                {
                                    try
                                    {
                                        for (int i = 0; i < matDT.Rows.Count; i++)
                                        {
                                            if (!String.IsNullOrEmpty(matDT.Rows[i][0].ToString())
                                            && !String.IsNullOrEmpty(matDT.Rows[i][1].ToString())
                                            && !String.IsNullOrEmpty(matDT.Rows[i][3].ToString())
                                            && !String.IsNullOrEmpty(matDT.Rows[i][4].ToString()))
                                            {
                                                TemporaryVariables.materialDT.Rows.Add(matDT.Rows[i][0].ToString(),
                                                    matDT.Rows[i][1].ToString(),
                                                    matDT.Rows[i][2].ToString(),
                                                    matDT.Rows[i][3].ToString(),
                                                    matDT.Rows[i][4].ToString(),
                                                    false);
                                            }
                                            if (TemporaryVariables.language == 0)
                                            {
                                                loadMsg = "Lấy dữ liệu nguyên vật liệu ...\r\nGetting material data ...";
                                            }
                                            else if (TemporaryVariables.language == 1)
                                            {
                                                loadMsg = "Lấy dữ liệu nguyên vật liệu ...\r\n获取原料信息 ...";
                                            }
                                            else if (Settings.Default.language == 2)
                                            {
                                                loadMsg = "Getting material data ...";
                                            }
                                            else if (Settings.Default.language == 3)
                                            {
                                                loadMsg = "Lấy dữ liệu nguyên vật liệu ...";
                                            }
                                            else if (Settings.Default.language == 4)
                                            {
                                                loadMsg = "获取原料信息 ...";
                                            }
                                            loadingDialog.UpdateProgress(100 * i / matDT.Rows.Count, loadMsg);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        if (TemporaryVariables.language == 0)
                                        {
                                            message = "Tải dữ liệu nguyên liệu không thành công!\r\nFailed to load material data!";
                                            caption = "Lỗi / Error";
                                        }
                                        else if (TemporaryVariables.language == 1)
                                        {
                                            message = "Tải dữ liệu nguyên liệu không thành công!\r\n加载材质数据失败！";
                                            caption = "Lỗi / 错误";
                                        }
                                        else if (Settings.Default.language == 2)
                                        {
                                            message = "Failed to load material data!";
                                            caption = "Error";
                                        }
                                        else if (Settings.Default.language == 3)
                                        {
                                            message = "Tải dữ liệu nguyên liệu không thành công!";
                                            caption = "Lỗi";
                                        }
                                        else if (Settings.Default.language == 4)
                                        {
                                            message = "加载材质数据失败！";
                                            caption = "错误";
                                        }
                                        CTMessageBox.Show(message + ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    for (int j = 0; j < processDT.Rows.Count; j++)
                                    {
                                        try
                                        {
                                            if (!String.IsNullOrEmpty(processDT.Rows[j][0].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][1].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][2].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][5].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][6].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][7].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][8].ToString()))
                                            {
                                                int changeSpeed = 0, changeTime = 0;
                                                bool isVaccum = false, isSkipAnnounce = false;

                                                if (!String.IsNullOrEmpty(processDT.Rows[j][3].ToString()))
                                                    changeSpeed = Convert.ToInt32(processDT.Rows[j][3].ToString());

                                                if (!String.IsNullOrEmpty(processDT.Rows[j][4].ToString()))
                                                    changeTime = Convert.ToInt32(processDT.Rows[j][4].ToString());

                                                if (processDT.Rows[j][5].ToString().ToLower() == "yes")
                                                    isVaccum = true;
                                                else if (processDT.Rows[j][5].ToString().ToLower() == "no")
                                                    isVaccum = false;

                                                if (processDT.Rows[j][7].ToString().ToLower() == "yes")
                                                    isSkipAnnounce = true;
                                                else if (processDT.Rows[j][7].ToString().ToLower() == "no")
                                                    isSkipAnnounce = false;

                                                TemporaryVariables.processDT.Rows.Add(processDT.Rows[j][0].ToString(),
                                                    processDT.Rows[j][1].ToString(),
                                                    processDT.Rows[j][2].ToString(),
                                                    changeSpeed,
                                                    changeTime,
                                                    isVaccum,
                                                    processDT.Rows[j][6].ToString(),
                                                    isSkipAnnounce,
                                                    processDT.Rows[j][8].ToString(),
                                                    false);
                                            }
                                            if (TemporaryVariables.language == 0)
                                            {
                                                loadMsg = "Lấy dữ liệu quy trình ...\r\nGetting process data ...";
                                            }
                                            else if (TemporaryVariables.language == 1)
                                            {
                                                loadMsg = "Lấy dữ liệu quy trình ...\r\n获取操作步骤信息...";
                                            }
                                            else if (Settings.Default.language == 2)
                                            {
                                                loadMsg = "Getting process data ...";
                                            }
                                            else if (Settings.Default.language == 3)
                                            {
                                                loadMsg = "Lấy dữ liệu quy trình ...";
                                            }
                                            else if (Settings.Default.language == 4)
                                            {
                                                loadMsg = "获取操作步骤信息...";
                                            }

                                            loadingDialog.UpdateProgress(100 * j / processDT.Rows.Count, loadMsg);
                                        }
                                        catch (Exception)
                                        {
                                            if (TemporaryVariables.language == 0)
                                            {
                                                message = "Tải dữ liệu các bước không thành công!\r\nFailed to load process data!";
                                                caption = "Lỗi / Error";
                                            }
                                            else if (TemporaryVariables.language == 1)
                                            {
                                                message = "Tải dữ liệu các bước không thành công!\r\n下载数据步骤失败！";
                                                caption = "Lỗi / 错误";
                                            }
                                            else if (Settings.Default.language == 2)
                                            {
                                                message = "Failed to load process data!";
                                                caption = "Error";
                                            }
                                            else if (Settings.Default.language == 3)
                                            {
                                                message = "Tải dữ liệu các bước không thành công!";
                                                caption = "Lỗi";
                                            }
                                            else if (Settings.Default.language == 4)
                                            {
                                                message = "下载数据步骤失败！";
                                                caption = "错误";
                                            }
                                        }
                                    }
                                    while (!this.IsHandleCreated)
                                        System.Threading.Thread.Sleep(100);
                                    loadingDialog.BeginInvoke(new Action(() => loadingDialog.Close()));
                                }));
                            backgroundThreadGetData.Start();
                            loadingDialog.ShowDialog();

                            dtgvSpecMaterialList.DataSource = TemporaryVariables.materialDT;
                            dtgvSpecMaterialList.Columns["mat_no"].Visible = false;
                            dtgvSpecMaterialList.Columns["lot_no"].Visible = false;
                            dtgvSpecMaterialList.Columns["tolerance"].Visible = false;
                            if (TemporaryVariables.language == 0)
                            {
                                dtgvSpecMaterialList.Columns["mat_name"].HeaderText = "Tên nguyên liệu\r\nMaterial name";
                            }
                            else if (TemporaryVariables.language == 1)
                            {
                                dtgvSpecMaterialList.Columns["mat_name"].HeaderText = "Tên nguyên liệu\r\n原料名称";
                            }
                            else if (Settings.Default.language == 2)
                            {
                                dtgvSpecMaterialList.Columns["mat_name"].HeaderText = "Material name";
                            }
                            else if (Settings.Default.language == 3)
                            {
                                dtgvSpecMaterialList.Columns["mat_name"].HeaderText = "Tên nguyên liệu";
                            }
                            else if (Settings.Default.language == 4)
                            {
                                dtgvSpecMaterialList.Columns["mat_name"].HeaderText = "原料名称";
                            }

                            dtgvSpecMaterialList.Columns["weight"].Visible = false;
                            dtgvSpecMaterialList.Columns["is_confirmed"].Visible = false;

                            dtgvSpecProcessList.DataSource = TemporaryVariables.processDT;
                            dtgvSpecProcessList.Columns["init_speed"].Visible = false;
                            dtgvSpecProcessList.Columns["init_time"].Visible = false;
                            dtgvSpecProcessList.Columns["change_speed"].Visible = false;
                            dtgvSpecProcessList.Columns["change_time"].Visible = false;
                            dtgvSpecProcessList.Columns["is_vaccum"].Visible = false;
                            dtgvSpecProcessList.Columns["max_temperature"].Visible = false;
                            dtgvSpecProcessList.Columns["is_skip_announce"].Visible = false;
                            dtgvSpecProcessList.Columns["is_finished"].Visible = false;
                            if (TemporaryVariables.language == 0)
                            {
                                dtgvSpecProcessList.Columns["process_no"].HeaderText = "Số bước\r\nStep No.";
                                dtgvSpecProcessList.Columns["description"].HeaderText = "Mô tả\r\nDescription";
                            }
                            else if (TemporaryVariables.language == 1)
                            {
                                dtgvSpecProcessList.Columns["process_no"].HeaderText = "Số bước\r\n序号";
                                dtgvSpecProcessList.Columns["description"].HeaderText = "Mô tả\r\n描述";
                            }
                            else if (Settings.Default.language == 2)
                            {
                                dtgvSpecProcessList.Columns["process_no"].HeaderText = "Step No.";
                                dtgvSpecProcessList.Columns["description"].HeaderText = "Description";
                            }
                            else if (Settings.Default.language == 3)
                            {
                                dtgvSpecProcessList.Columns["process_no"].HeaderText = "Số bước";
                                dtgvSpecProcessList.Columns["description"].HeaderText = "Mô tả";
                            }
                            else if (Settings.Default.language == 4)
                            {
                                dtgvSpecProcessList.Columns["process_no"].HeaderText = "序号";
                                dtgvSpecProcessList.Columns["description"].HeaderText = "描述";
                            }

                        }
                        else
                        {
                            if (TemporaryVariables.language == 0)
                            {
                                message = "Dữ liệu excel trống. Vui lòng kiểm tra!\r\nExcel file is empty. Please check again!";
                            }
                            else if (TemporaryVariables.language == 1)
                            {
                                message = "Dữ liệu excel trống. Vui lòng kiểm tra!\r\nExcel 文件为空。请再检查一次！";
                            }
                            else if (Settings.Default.language == 2)
                            {
                                message = "Excel file is empty. Please check again!";
                            }
                            else if (Settings.Default.language == 3)
                            {
                                message = "Dữ liệu excel trống. Vui lòng kiểm tra!";
                            }
                            else if (Settings.Default.language == 4)
                            {
                                message = "Excel 文件为空。请再检查一次！";
                            }
                            throw new Exception(message); //add chinese
                        }
                    }
                }
                catch (Exception ex)
                {
                    TemporaryVariables.resetAllTempVariables();
                    dtgvSpecMaterialList.DataSource = null;
                    dtgvSpecProcessList.DataSource = null;
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "Excel Load error", ex.Message);
                    if (TemporaryVariables.language == 0)
                    {
                        message = "Lỗi khi tải dữ liệu excel!\r\nLoad Excel data failed!" + "\r\n\r\n" + ex.Message;
                        caption = "Lỗi / Error";
                    }
                    else if (TemporaryVariables.language == 1)
                    {
                        message = "Lỗi khi tải dữ liệu excel!\r\n下载EXCEL失败!" + "\r\n\r\n" + ex.Message;
                        caption = "Lỗi / 错误";
                    }
                    else if (Settings.Default.language == 2)
                    {
                        message = "Load Excel data failed!" + "\r\n\r\n" + ex.Message;
                        caption = "Error";
                    }
                    else if (Settings.Default.language == 3)
                    {
                        message = "Lỗi khi tải dữ liệu excel!" + "\r\n\r\n" + ex.Message;
                        caption = "Lỗi";
                    }
                    else if (Settings.Default.language == 4)
                    {
                        message = "下载EXCEL失败!" + "\r\n\r\n" + ex.Message;
                        caption = "错误";
                    }
                    CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void saveExcelPassFormClosed(object sender, EventArgs e)
        {
            ((Form)sender).FormClosed -= saveExcelPassFormClosed;
            if (isConfirmed)
            {
                isConfirmed = false;
                try
                {
                    System.Windows.Forms.SaveFileDialog saveFileDialog = new SaveFileDialog();
                    string pathsave = "";
                    if (TemporaryVariables.language == 0)
                    {
                        saveFileDialog.Title = "Lưu file Excel mẫu / Save Excel template";
                    }
                    else if (TemporaryVariables.language == 1)
                    {
                        saveFileDialog.Title = "Lưu file Excel mẫu / 保存 Excel 模板";
                    }
                    else if (Settings.Default.language == 2)
                    {
                        saveFileDialog.Title = "Save Excel template";
                    }
                    else if (Settings.Default.language == 3)
                    {
                        saveFileDialog.Title = "Lưu file Excel mẫu";
                    }
                    else if (Settings.Default.language == 4)
                    {
                        saveFileDialog.Title = "保存 Excel 模板";
                    }
                    saveFileDialog.DefaultExt = "Excel";
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveFileDialog.CheckPathExists = true;

                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        pathsave = saveFileDialog.FileName;
                        saveFileDialog.RestoreDirectory = true;
                        using (System.IO.FileStream fs = new System.IO.FileStream(pathsave, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                        {
                            byte[] data = null; //Add resource
                            if (TemporaryVariables.language == 0)
                            {
                                data = Properties.Resources.data_template_English_;
                            }
                            else
                            {
                                data = Properties.Resources.data_template_Chinese_;
                            }
                            fs.Write(data, 0, data.Length);
                        }
                        if (TemporaryVariables.language == 0)
                        {
                            message = "Lưu tệp Excel mẫu thành công !\r\nSuccessfully save Excel template !";
                            caption = "Thông tin / Information";
                        }
                        else if (TemporaryVariables.language == 1)
                        {
                            message = "Lưu tệp Excel mẫu thành công !\r\nEXCEL模板保存成功！";
                            caption = "Thông tin / 信息";
                        }
                        else if (Settings.Default.language == 2)
                        {
                            message = "Successfully save Excel template !";
                            caption = "Information";
                        }
                        else if (Settings.Default.language == 3)
                        {
                            message = "Lưu tệp Excel mẫu thành công !";
                            caption = "Thông tin";
                        }
                        else if (Settings.Default.language == 4)
                        {
                            message = "EXCEL模板保存成功！";
                            caption = "信息";
                        }
                        CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    if (TemporaryVariables.language == 0)
                    {
                        message = "Lưu tệp Excel mẫu thất bại !\r\nFailed to save Excel template !" + "\r\n\r\n" + ex.Message;
                        caption = "Lỗi / Error";
                    }
                    else if (TemporaryVariables.language == 1)
                    {
                        message = "Lưu tệp Excel mẫu thất bại !\r\nEXCEL模板保存失败！" + "\r\n\r\n" + ex.Message;
                        caption = "Lỗi / 错误";
                    }
                    else if (Settings.Default.language == 2)
                    {
                        message = "Failed to save Excel template !" + "\r\n\r\n" + ex.Message;
                        caption = "Error";
                    }
                    else if (Settings.Default.language == 3)
                    {
                        message = "Lưu tệp Excel mẫu thất bại !" + "\r\n\r\n" + ex.Message;
                        caption = "Lỗi";
                    }
                    else if (Settings.Default.language == 4)
                    {
                        message = "EXCEL模板保存失败！" + "\r\n\r\n" + ex.Message;
                        caption = "错误";
                    }
                    CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "Lỗi lưu file mẫu", ex.Message);
                }
            }
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
                    if (TemporaryVariables.language == 0)
                    {
                        fileDialog.Title = "Nhập file công thức / Import formula";
                    }
                    else if (TemporaryVariables.language == 1)
                    {
                        fileDialog.Title = "Nhập file công thức / 导入公式文件";
                    }
                    else if (Settings.Default.language == 2)
                    {
                        fileDialog.Title = "Import formula";
                    }
                    else if (Settings.Default.language == 3)
                    {
                        fileDialog.Title = "Nhập file công thức";
                    }
                    else if (Settings.Default.language == 4)
                    {
                        fileDialog.Title = "导入公式文件";
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
                        if (TemporaryVariables.language == 0)
                        {
                            message = "Thêm công thức thành công!\r\nSuccessfully import formula !";
                            caption = "Thông tin / Information";
                        }
                        else if (TemporaryVariables.language == 1)
                        {
                            message = "Thêm công thức thành công!\r\n更多成功秘诀！";
                            caption = "Thông tin / 信息";
                        }
                        else if (Settings.Default.language == 2)
                        {
                            message = "Successfully import formula !";
                            caption = "Information";
                        }
                        else if (Settings.Default.language == 3)
                        {
                            message = "Thêm công thức thành công!";
                            caption = "Thông tin";
                        }
                        else if (Settings.Default.language == 4)
                        {
                            message = "更多成功秘诀！";
                            caption = "信息";
                        }
                        CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadItemFilePath(Properties.Settings.Default.folder_directory);
                    }
                }
                catch (Exception ex)
                {
                    if (TemporaryVariables.language == 0)
                    {
                        message = "Thêm công thức thất bại !\r\nFailed to import formula!" + "\r\n\r\n" + ex.Message;
                        caption = "Lỗi / Error";
                    }
                    else if (TemporaryVariables.language == 1)
                    {
                        message = "Thêm công thức thất bại !\r\n更多失败的食谱！" + "\r\n\r\n" + ex.Message;
                        caption = "Lỗi / 错误";
                    }
                    else if (Settings.Default.language == 2)
                    {
                        message = "Failed to import formula!" + "\r\n\r\n" + ex.Message;
                        caption = "Error";
                    }
                    else if (Settings.Default.language == 3)
                    {
                        message = "Thêm công thức thất bại !" + "\r\n\r\n" + ex.Message;
                        caption = "Lỗi";
                    }
                    else if (Settings.Default.language == 4)
                    {
                        message = "更多失败的食谱！" + "\r\n\r\n" + ex.Message;
                        caption = "错误";
                    }
                    CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "Lỗi lưu file mẫu", ex.Message);
                }
            }
        }

        private void btnGetTemplate_Click(object sender, EventArgs e)
        {
            PasswordConfirm passwordConfirm = new PasswordConfirm();
            passwordConfirm.FormClosed += saveExcelPassFormClosed;
            passwordConfirm.ShowDialog();
        }
    }
}
