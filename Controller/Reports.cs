using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mixer_control_globalver.Controller
{
    public class Reports
    {
        public string PathMixerReport = Environment.CurrentDirectory + @"\Resources\MixerReportForm.xlsx";

        public void ExportExcelMixerReport(string PathSave, DataTable materialDT)
        {
            ToolSupport toolSupport = new ToolSupport();
            toolSupport.ExportSmoking(materialDT, PathSave, PathMixerReport);
        }
    }
}
