using ExcelDataReader;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

class SubMethods
{
    public static DataTable ImportExceltoDatatable(string filePath)
    {
        using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
        {

            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateReader(stream);

            var conf = new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = a => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            };

            DataSet dataSet = excelDataReader.AsDataSet(conf);

            return dataSet.Tables["process_info"];
        }
    }
    public static string ReturnCleanASCII(string s)
    {
        s = Regex.Replace(s, @"[\u0000-\u0008\u000A-\u001F\u0100-\uFFFF]", "");
        s = Regex.Replace(s, @"[^\t\r\n -~]", "");
        return s;
    }
}