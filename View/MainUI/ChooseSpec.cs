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
        public ChooseSpec()
        {
            InitializeComponent();
            if (!String.IsNullOrEmpty(Properties.Settings.Default.folder_directory))
                txbSearchDirectory.Text = Properties.Settings.Default.folder_directory;
            else
            {
                string dirPath = AppDomain.CurrentDomain.BaseDirectory + "\\InputData";
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(dirPath);
                if (dir.Exists == false)
                    dir.Create();

                Properties.Settings.Default.folder_directory = dir.FullName;
                txbSearchDirectory.Text = dir.FullName;
                Properties.Settings.Default.Save();
            }
            LoadItemFilePath(Properties.Settings.Default.folder_directory);
        }

        private void ChooseSpec_Load(object sender, EventArgs e)
        {
            btnRefreshFileList.ButtonText = "Làm mới" + Environment.NewLine + "Refresh";
            btnConfirmChoose.ButtonText = "Tiến hành cân liệu" + Environment.NewLine + "Material Scale";

            TemporaryVariables.resetAllTempVariables();
        }
        private void LoadItemFilePath(string path)
        {
            try
            {
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
                dtgvListSpecification.Columns["file_name"].HeaderText = "Tên tệp" + Environment.NewLine + "File name";
                dtgvListSpecification.Columns["file_path"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải dữ liệu tệp!" + Environment.NewLine + "Load dirctory files failed!" + "\r\n\r\n" + ex.Message, "Lỗi / Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                txbSearchDirectory.Text = dialog.FileName;
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
                        throw new Exception("Dữ liệu excel không đúng kiểu! Vui lòng kiểm tra!" + Environment.NewLine + "Excel file is not in correct format! Please check again!");
                    matDT = materialSheet.ExportDataTable();
                    processDT = processSheet.ExportDataTable();

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
                                    || String.IsNullOrEmpty(matDT.Rows[i][4].ToString())
                                    || String.IsNullOrEmpty(matDT.Rows[i][5].ToString()))
                                    {
                                        throw new Exception("Dữ liệu nguyên liệu ở file excel không đúng hoặc thiếu! Vui lòng kiểm tra!" + Environment.NewLine + "Material data in excel file is not enough or wrong! Please check again!");
                                    }
                                    bool isPacked;
                                    if (matDT.Rows[i][5].ToString().ToLower() == "packed")
                                        isPacked = true;
                                    else isPacked = false;

                                    TemporaryVariables.materialDT.Rows.Add(matDT.Rows[i][0].ToString(),
                                        matDT.Rows[i][1].ToString(),
                                        matDT.Rows[i][2].ToString(),
                                        matDT.Rows[i][3].ToString(),
                                        matDT.Rows[i][4].ToString(),
                                        isPacked,
                                        false);
                                }
                                loadingDialog.UpdateProgress(100 * i / matDT.Rows.Count, "Lấy dữ liệu nguyên vật liệu ..." + Environment.NewLine + "Getting material data ...");
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
                                        || String.IsNullOrEmpty(processDT.Rows[j][6].ToString()))
                                    {
                                        throw new Exception("Dữ liệu quy trình ở file excel không đúng hoặc thiếu! Vui lòng kiểm tra!" + Environment.NewLine + "Process data in excel file is not enough or wrong! Please check again!");
                                    }

                                    int changeSpeed = 0, changeTime = 0, vaccumTime = 0;

                                    if (!String.IsNullOrEmpty(processDT.Rows[j][3].ToString()))
                                        changeSpeed = Convert.ToInt32(processDT.Rows[j][3].ToString());

                                    if (!String.IsNullOrEmpty(processDT.Rows[j][4].ToString()))
                                        changeTime = Convert.ToInt32(processDT.Rows[j][4].ToString());

                                    if (!String.IsNullOrEmpty(processDT.Rows[j][5].ToString()))
                                        vaccumTime = Convert.ToInt32(processDT.Rows[j][5].ToString());

                                    TemporaryVariables.processDT.Rows.Add(processDT.Rows[j][0].ToString(),
                                        processDT.Rows[j][1].ToString(),
                                        processDT.Rows[j][2].ToString(),
                                        changeSpeed,
                                        changeTime,
                                        vaccumTime,
                                        processDT.Rows[j][6].ToString(),
                                        processDT.Rows[j][7].ToString(),
                                        false);
                                }
                                loadingDialog.UpdateProgress(100 * j / processDT.Rows.Count, "Lấy dữ liệu quy trình ..." + Environment.NewLine + "Getting process data ...");
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
                    dtgvSpecMaterialList.Columns["is_packed"].Visible = false;
                    dtgvSpecMaterialList.Columns["mat_name"].HeaderText = "Tên nguyên liệu" + Environment.NewLine + "Material name";
                    dtgvSpecMaterialList.Columns["weight"].Visible = false;
                    dtgvSpecMaterialList.Columns["is_scaled"].Visible = false;

                    dtgvSpecProcessList.DataSource = TemporaryVariables.processDT;
                    dtgvSpecProcessList.Columns["init_speed"].Visible = false;
                    dtgvSpecProcessList.Columns["init_time"].Visible = false;
                    dtgvSpecProcessList.Columns["change_speed"].Visible = false;
                    dtgvSpecProcessList.Columns["change_time"].Visible = false;
                    dtgvSpecProcessList.Columns["vaccum_time"].Visible = false;
                    dtgvSpecProcessList.Columns["max_temperature"].Visible = false;
                    dtgvSpecProcessList.Columns["is_finished"].Visible = false;
                    dtgvSpecProcessList.Columns["process_no"].HeaderText = "Số bước" + Environment.NewLine + "Step No.";
                    dtgvSpecProcessList.Columns["description"].HeaderText = "Mô tả" + Environment.NewLine + "Description";
                }
                catch (Exception ex)
                {
                    TemporaryVariables.resetAllTempVariables();
                    dtgvSpecMaterialList.DataSource = null;
                    dtgvSpecProcessList.DataSource = null;
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "Excel Load error", ex.Message);
                    MessageBox.Show("Lỗi khi tải dữ liệu excel!" + Environment.NewLine + "Load Excel data failed!" + "\r\n\r\n" + ex.Message, "Lỗi / Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefreshFileList_Click(object sender, EventArgs e)
        {
            LoadItemFilePath(Properties.Settings.Default.folder_directory);
        }

        private void btnConfirmChoose_Click(object sender, EventArgs e)
        {
            AppIntro.main.openScaleTab();
        }
    }
}
