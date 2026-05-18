using ExcelDataReader;
using mixer_control_globalver.Controller.LogFile;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

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

    public static bool CheckDoubleIsInRange(double value, double range)
    {
        return Math.Abs(value) <= range;
    }

    //public static double ConvertString2Double (string value)
    //{

    //}
    public static bool AreDoublesNearlyEqual(double a, double b, double epsilon)
    {
        return Math.Abs(a - b) < epsilon;
    }

    public static void FuelSetting(SerialPort serialPort, DeviceIPConnection device, double numberReal)
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
        if(!SettingsManager.GetSetting(s => s.OilPumpNewIpConnectorEnabled))
            SendCommand(serialPort, byteArray); // Cài đặt lượng xăng định mức
        else
            device.Write(byteArray, 0, byteArray.Length);
        string hexString2 = "";

        foreach (byte b in byteArray)
        {
            hexString2 += "0x" + b.ToString("X2") + " ";
        }
    }

    public static bool CheckConnectStatus(SerialPort serialPort)
    {
        try
        {
            if (serialPort.IsOpen)
                return true;
        }
        catch (Exception ex)
        {
            CTMessageBox.Show("Serialport check connection error : " + ex.Message, "Serialport Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            SystemLog.Output(SystemLog.MSG_TYPE.Err, "Serialport Error", "Serialport check connection error : " + ex.Message);
        }
        return false;
    }

    public static string TrimSpecialCharacters(string input)
    {
        // Check if the string is not null or empty
        if (string.IsNullOrEmpty(input))
            return input;

        // Remove 's' from the beginning if present
        if (input.StartsWith("s"))
            input = input.Substring(1);

        // We'll look for an 'e' at the end of the string
        // First, find the last occurrence of 'e'
        int lastEPosition = input.LastIndexOf('e');

        // Check if this 'e' is at the end or near the end (followed by other characters)
        if (lastEPosition >= 0)
        {
            // Only trim if this 'e' is at a position where it could be the ending 'e'
            // We can assume an 'e' is the ending one if it's not followed by any of the 
            // expected characters in our data format (letters, numbers, dash, semicolon)
            bool isEndingE = true;

            // If it's not the very last character, check what follows it
            if (lastEPosition < input.Length - 1)
            {
                // Get a small sample of what comes after 'e' (up to 3 chars)
                string afterE = input.Substring(lastEPosition + 1,
                    Math.Min(3, input.Length - lastEPosition - 1));

                // If what follows looks like it could be part of our normal pattern, 
                // then this is probably not our ending 'e'
                if (afterE.All(c => char.IsLetterOrDigit(c) || c == '-' || c == ';'))
                {
                    isEndingE = false;
                }
            }

            if (isEndingE)
            {
                input = input.Substring(0, lastEPosition);
            }
        }

        return input;
    }

    public static void SendCommand(SerialPort serialPort, byte[] command)
    {
        try
        {
            // Gửi lệnh
            serialPort.Write(command, 0, command.Length);
            //Thread.Sleep(100);
            //byte[] buffer = new byte[command.Length];
            //var a = serialPort.ReadExisting();
            //serialPort.Read(buffer, 0, buffer.Length);
            //Console.WriteLine("Phản hồi từ máy bơm xăng: " + BitConverter.ToString(buffer));
        }
        catch
        {
            CTMessageBox.Show("Failed to send command to serial port : " + command.ToString(), "Serialport Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            SystemLog.Output(SystemLog.MSG_TYPE.Err, "Serialport Error", "Failed to send command to serial port : " + command.ToString());
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

    public static void SetLanguage(string cultureCode)
    {
        if (!String.IsNullOrEmpty(cultureCode))
        {
            CultureInfo newCulture = new CultureInfo(cultureCode);

            // ✅ UI culture = controls display language (labels, strings)
            Thread.CurrentThread.CurrentUICulture = newCulture;

            // ✅ Number formatting stays ALWAYS invariant (dot as decimal)
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }
        else
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }
    }
}