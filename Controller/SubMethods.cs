using mixer_control_globalver.Controller.LogFile;
using mixer_control_globalver.View.CustomControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ExcelDataReader;

class SubMethods
{
    public static DataTable ImportExceltoDatatable(string filePath)
    {
        try
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
        catch (Exception ex)
        {
            SystemLog.Output(SystemLog.MSG_TYPE.Err, "Excel export error", ex.Message);
            return null;
        }
    }
    public static string ReturnCleanASCII(string s)
    {
        s = Regex.Replace(s, @"[\u0000-\u0008\u000A-\u001F\u0100-\uFFFF]", "");
        s = Regex.Replace(s, @"[^\t\r\n -~]", "");
        return s;
    }

    //public static double ConvertString2Double (string value)
    //{

    //}

    public static void FuelSetting(SerialPort serialPort, double numberReal)
    {
        double actualNumber = Math.Round(numberReal, 2) * 100;
        int number = Convert.ToInt32(actualNumber); // Số nguyên muốn chuyển đổi
        string hexString = number.ToString("X6"); // Chuyển đổi số nguyên sang Hex

        // Đảm bảo chiều dài của chuỗi hex là chẵn
        if (hexString.Length % 2 != 0)
        {
            hexString = "0" + hexString;
        }

        // Tạo mảng byte để lưu trữ kết quả
        byte[] byteArray = new byte[hexString.Length / 2];

        // Tách chuỗi hex thành từng byte
        for (int i = 0; i < byteArray.Length; i++)
        {
            byteArray[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
        }
        List<byte> byteList = new List<byte>(byteArray);

        // Thêm 3 byte vào phía trước mảng
        byteList.InsertRange(0, new byte[] { 0x5A, 0x01, 0X05 });

        // Tính toán byte checksum
        byteArray = byteList.ToArray();
        int sum = 0;
        foreach (byte b in byteArray)
        {
            sum += b;
        }

        // Lấy byte cuối cùng của kết quả tổng
        byte checksum = (byte)(sum & 0xFF);

        // Thêm 2 byte vào phía sau mảng
        byteList.AddRange(new byte[] { checksum, 0xA5 });

        // Chuyển lại List<byte> thành mảng byte
        byteArray = byteList.ToArray();

        SendCommand(serialPort, byteArray); // Cài đặt lượng xăng định mức
        string hexString2 = "";

        foreach (byte b in byteArray)
        {
            hexString2 += "0x" + b.ToString("X2") + " ";
        }
    }

    public static bool CheckConnectStatus(SerialPort serialPort, byte[] command)
    {
        try
        {
            if (serialPort.IsOpen)
                return true;
        }
        catch (Exception ex)
        {
            CTMessageBox.Show("Lỗi đọc/ ghi cổng COM: " + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return false;
    }

    public static void SendCommand(SerialPort serialPort, byte[] command)
    {
        try
        {
            // Gửi lệnh
            serialPort.Write(command, 0, command.Length);
            Thread.Sleep(100);
            // Đọc phản hồi từ máy bơm xăng
            byte[] buffer = new byte[command.Length];
            //var a = serialPort.ReadExisting();
            //serialPort.Read(buffer, 0, buffer.Length);
            //Console.WriteLine("Phản hồi từ máy bơm xăng: " + BitConverter.ToString(buffer));
        }
        catch
        {
            CTMessageBox.Show("Lỗi đọc/ ghi cổng COM", "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //Console.WriteLine("Lỗi: " + ex.Message);
        }
    }

    public static void BackupUserSettings()
    {
        try
        {
            string userConfigPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            string backupPath = userConfigPath + ".bak";
            File.Copy(userConfigPath, backupPath, true);
        }
        catch (Exception ex)
        {
            SystemLog.Output(SystemLog.MSG_TYPE.Err, "Create user.config backup error", ex.Message);
        }
    }

    public static void RestoreUserSettings(string userConfigPath)
    {
        try
        {
            string backupPath = userConfigPath + ".bak";
            if (File.Exists(backupPath))
            {
                File.Copy(backupPath, userConfigPath, true);
                Application.Restart();
                Environment.Exit(0);// Reload settings to apply restored values
            }
        }
        catch (Exception ex)
        {
            SystemLog.Output(SystemLog.MSG_TYPE.Err, "Restore user.config error", ex.Message);
        }
    }
}