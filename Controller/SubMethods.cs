using ExcelDataReader;
using System.Data;
using System.IO;

class SubMethods
{
    public static DataTable ImportExceltoDatatable(string filePath)
    {
        using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
        {

            IExcelDataReader excelDataReader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream);

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
}