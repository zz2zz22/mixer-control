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
using mixer_control_globalver.View.CustomComponent;
using Spire.Xls;


namespace mixer_control_globalver.View.MainUI
{
    public partial class ChooseSpec : Form
    {
        string message = String.Empty, caption = String.Empty;
        public ChooseSpec()
        {
            InitializeComponent();
        }

        private void ChooseSpec_Load(object sender, EventArgs e)
        {
            if (TemporaryVariables.language == 0 )
            {
                lb5.Text = "Công thức đã chọn:\r\nSelected formula:";
                lb2.Text = "Danh sách công thức:\r\nFormula setting files:";
                lb3.Text = "Danh sách liệu:\r\nMaterials list:";
                lb4.Text = "Danh sách quy trình:\r\nProcess list:";

                btnConfirmChoose.ButtonText = "Tiến hành xác nhận liệu\r\nBegin Material Confirmation";
                btnGetTemplate.ButtonText = "Tải mẫu\r\nDownload template";
            }
            else if (TemporaryVariables.language == 1)
            {
                lb5.Text = "Công thức đã chọn:\r\n已选产品型号:";
                lb2.Text = "Danh sách công thức:\r\n产品型号列表:";
                lb3.Text = "Danh sách liệu:\r\n原料列表:";
                lb4.Text = "Danh sách quy trình:\r\n操作步骤:";

                btnConfirmChoose.ButtonText = "Tiến hành xác nhận liệu\r\n开始材料确认。";
                btnGetTemplate.ButtonText = "Tải mẫu\r\n下载模板";
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
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picbtnChooseDirectory_Click(object sender, EventArgs e)
        {
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
                    TemporaryVariables.InitMaterialDT();
                    TemporaryVariables.InitProcessDT();

                    DataTable matDT = new DataTable();
                    DataTable processDT = new DataTable();
                    Workbook workbook = new Workbook();
                    workbook.LoadFromFile(TemporaryVariables.tempFilePath);
                    Worksheet materialSheet = workbook.Worksheets["material_info"];
                    Worksheet processSheet = workbook.Worksheets["process_info"];

                    if (materialSheet == null || processSheet == null)
                    {
                        if (TemporaryVariables.language == 0)
                        {
                            message = "Dữ liệu excel không đúng kiểu! Vui lòng kiểm tra!\r\nExcel file is not in correct format! Please check again!";
                        }
                        else if (TemporaryVariables.language == 1)
                        {
                            message = "Dữ liệu excel không đúng kiểu! Vui lòng kiểm tra!\r\nEXCEL板式不合格！请检查！";
                        }
                        throw new Exception(message);
                    }
                    matDT = materialSheet.ExportDataTable();
                    processDT = processSheet.ExportDataTable();

                    if (matDT.Rows.Count > 0 && processDT.Rows.Count > 0)
                    {
                        LoadingDialog loadingDialog = new LoadingDialog();
                        Thread backgroundThreadGetData = new Thread(
                            new ThreadStart(() =>
                            {
                                for (int i = 0; i < matDT.Rows.Count; i++)
                                {
                                    Thread.Sleep(20);
                                    if (!String.IsNullOrEmpty(matDT.Rows[i][0].ToString())
                                        && !String.IsNullOrEmpty(matDT.Rows[i][1].ToString()))
                                    {
                                        if (String.IsNullOrEmpty(matDT.Rows[i][3].ToString())
                                        || String.IsNullOrEmpty(matDT.Rows[i][4].ToString()))
                                        {
                                            if (TemporaryVariables.language == 0)
                                            {
                                                message = "Dữ liệu excel không đúng kiểu! Vui lòng kiểm tra!\r\nExcel file is not in correct format! Please check again!";
                                            }
                                            else if (TemporaryVariables.language == 1)
                                            {
                                                message = "Dữ liệu excel không đúng kiểu! Vui lòng kiểm tra!\r\nEXCEL板式不合格！请检查！";
                                            }
                                            throw new Exception(message);
                                        }
                                        TemporaryVariables.materialDT.Rows.Add(matDT.Rows[i][0].ToString(),
                                            matDT.Rows[i][1].ToString(),
                                            matDT.Rows[i][2].ToString(),
                                            matDT.Rows[i][3].ToString(),
                                            matDT.Rows[i][4].ToString(),
                                            false);
                                    }
                                    if (TemporaryVariables.language == 0)
                                    {
                                        message = "Lấy dữ liệu nguyên vật liệu ...\r\nGetting material data ...";
                                    }
                                    else if (TemporaryVariables.language == 1)
                                    {
                                        message = "Lấy dữ liệu nguyên vật liệu ...\r\n获取原料信息 ...";
                                    }
                                    loadingDialog.UpdateProgress(100 * i / matDT.Rows.Count, message);
                                }
                                loadingDialog.UpdateProgress(0, "");

                                for (int j = 0; j < processDT.Rows.Count; j++)
                                {
                                    Thread.Sleep(20);
                                    if (!String.IsNullOrEmpty(processDT.Rows[j][0].ToString())
                                        && !String.IsNullOrEmpty(processDT.Rows[j][7].ToString()))
                                    {
                                        if (String.IsNullOrEmpty(processDT.Rows[j][1].ToString())
                                            || String.IsNullOrEmpty(processDT.Rows[j][2].ToString())
                                            || String.IsNullOrEmpty(processDT.Rows[j][5].ToString())
                                            || String.IsNullOrEmpty(processDT.Rows[j][6].ToString()))
                                        {

                                            if (TemporaryVariables.language == 0)
                                            {
                                                message = "Dữ liệu excel không đúng kiểu! Vui lòng kiểm tra!\r\nExcel file is not in correct format! Please check again!";
                                            }
                                            else if (TemporaryVariables.language == 1)
                                            {
                                                message = "Dữ liệu excel không đúng kiểu! Vui lòng kiểm tra!\r\nEXCEL板式不合格！请检查！";
                                            }
                                            throw new Exception(message);
                                        }

                                        int changeSpeed = 0, changeTime = 0;
                                        bool isVaccum = false;

                                        if (!String.IsNullOrEmpty(processDT.Rows[j][3].ToString()))
                                            changeSpeed = Convert.ToInt32(processDT.Rows[j][3].ToString());

                                        if (!String.IsNullOrEmpty(processDT.Rows[j][4].ToString()))
                                            changeTime = Convert.ToInt32(processDT.Rows[j][4].ToString());


                                        if (processDT.Rows[j][5].ToString().ToLower() == "yes")
                                            isVaccum = true;
                                        else if (processDT.Rows[j][5].ToString().ToLower() == "no")
                                            isVaccum = false;


                                        TemporaryVariables.processDT.Rows.Add(processDT.Rows[j][0].ToString(),
                                            processDT.Rows[j][1].ToString(),
                                            processDT.Rows[j][2].ToString(),
                                            changeSpeed,
                                            changeTime,
                                            isVaccum,
                                            processDT.Rows[j][6].ToString(),
                                            processDT.Rows[j][7].ToString(),
                                            false);
                                    }
                                    if (TemporaryVariables.language == 0)
                                    {
                                        message = "Lấy dữ liệu quy trình ...\r\nGetting process data ...";
                                    }
                                    else if (TemporaryVariables.language == 1)
                                    {
                                        message = "Lấy dữ liệu quy trình ...\r\n获取操作步骤信息...";
                                    }
                                    loadingDialog.UpdateProgress(100 * j / processDT.Rows.Count, message);
                                }
                                loadingDialog.BeginInvoke(new Action(() => loadingDialog.Close()));
                            }));
                        backgroundThreadGetData.Start();
                        loadingDialog.ShowDialog();

                        lbFormulaName.Text = TemporaryVariables.tempFileName;

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
                        
                        dtgvSpecMaterialList.Columns["weight"].Visible = false;
                        dtgvSpecMaterialList.Columns["is_confirmed"].Visible = false;

                        dtgvSpecProcessList.DataSource = TemporaryVariables.processDT;
                        dtgvSpecProcessList.Columns["init_speed"].Visible = false;
                        dtgvSpecProcessList.Columns["init_time"].Visible = false;
                        dtgvSpecProcessList.Columns["change_speed"].Visible = false;
                        dtgvSpecProcessList.Columns["change_time"].Visible = false;
                        dtgvSpecProcessList.Columns["is_vaccum"].Visible = false;
                        dtgvSpecProcessList.Columns["max_temperature"].Visible = false;
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
                        throw new Exception(message); //add chinese
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
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnGetTemplate_Click(object sender, EventArgs e)
        {
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
                    MessageBox.Show(message, caption , MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Lỗi lưu file mẫu", ex.Message);
            }
        }
    }
}
