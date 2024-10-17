
using ClosedXML.Excel;
using System;
using System.Data;

namespace mixer_control_globalver.Controller
{
    public class ToolSupport
    {
        public void ExportSmoking(DataTable materialDT, string pathSave, string pathForm)
        {
            try
            {
                XLWorkbook xlWorkBook = new XLWorkbook(pathForm);
                var xlWorkSheet = xlWorkBook.Worksheet(1);
                xlWorkSheet.Range("A1").Value = "MIXER REPORT - " + DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss"); // Thêm ngày vào title
                xlWorkSheet.Range("B2").Value = TemporaryVariables.tempFileName;
                xlWorkSheet.Range("B3").Value = TemporaryVariables.tempFormulaLOT;
                xlWorkSheet.Range("I3").Value = Properties.Settings.Default.isStopBetweenStep ? "YES" : "NO";
                xlWorkSheet.Range("J3").Value = Properties.Settings.Default.isSkipOpenLid ? "YES" : "NO";
                xlWorkSheet.Range("K3").Value = Properties.Settings.Default.isOilFeed ? "YES" : "NO";
                for (int i = 0; i < materialDT.Rows.Count; i++)
                {
                    int row = 9 + i;
                    xlWorkSheet.Range("A" + row).Value = materialDT.Rows[i]["mat_name"].ToString();
                    xlWorkSheet.Range("B" + row).Value = materialDT.Rows[i]["weight"].ToString();
                    xlWorkSheet.Range("C" + row).Value = materialDT.Rows[i]["lot_no"].ToString();
                }
                xlWorkBook.SaveAs(pathSave, false);
                xlWorkBook.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
