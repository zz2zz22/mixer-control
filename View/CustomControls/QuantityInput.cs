using mixer_control_globalver.Controller;
using mixer_control_globalver.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace mixer_control_globalver.View.CustomControls
{
    public partial class QuantityInput : Form
    {
        string dataIn;
        string message = String.Empty, caption = String.Empty;
        double returnValue, downTolerance, upTolerance;
        bool isExitApplication = false;
        bool isOkWeight = false;
        public QuantityInput(string matCode, double up, double down)
        {
            InitializeComponent();
            upTolerance = up;
            downTolerance = down;
            lbMaterialCode.Text = matCode;
            lbTolerance.Text = down.ToString() + " - " + up.ToString();
        }
        private void CloseSerialPort()
        {
            isExitApplication = true;
            Thread.Sleep(serialPort1.ReadTimeout); //Wait for reading threads to finish
            serialPort1.Close();
            isExitApplication = false;
        }
        private void ConnectScale()
        {
            if (!String.IsNullOrEmpty(Properties.Settings.Default.comPort))
            {
                serialPort1.PortName = Properties.Settings.Default.comPort;
                cbComPort.Text = Properties.Settings.Default.comPort;
            }
            else
            {
                serialPort1.PortName = cbComPort.Text;
                Properties.Settings.Default.comPort = cbComPort.Text;
                Properties.Settings.Default.Save();
            }
            serialPort1.BaudRate = 9600;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");
            serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
            serialPort1.ReadTimeout = 100;
            serialPort1.Open();
        }
        private void showData(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(dataIn))
            {
                if (double.TryParse(dataIn, out returnValue))
                {
                    returnValue = Convert.ToDouble(dataIn);
                    if (downTolerance <= returnValue && returnValue <= upTolerance)
                    {
                        isOkWeight = true;
                        panelWeight.BackColor = Color.Yellow;
                        lbScaleRT.ForeColor = Color.Black;
                    }
                    else
                    {
                        isOkWeight = false;
                        panelWeight.BackColor = Color.Black;
                        lbScaleRT.ForeColor = Color.White;
                    }
                    lbScaleRT.Text = returnValue.ToString();
                }
            }
        }
        private void inputWeight()
        {
            if (isOkWeight)
            {
                TemporaryVariables.isInputQuantity = true;
                TemporaryVariables.inputQuantity = returnValue;
                if (serialPort1.IsOpen)
                    CloseSerialPort();
                this.Close();
            }
        }

        private void QuantityInput_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cbComPort.Items.AddRange(ports);
            ConnectScale();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!isExitApplication)
            {
                dataIn = serialPort1.ReadLine().Replace("kg", "").Trim();
                //Problem about scale latency --> Config scale mode --> HOLD mode 0 --> Kinda ok when test multiple time ( must wait for COM port to finalizing conneection )
                this.BeginInvoke(new EventHandler(showData));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
                CloseSerialPort();
            this.Close();
        }

        private void cbComPort_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen && !String.IsNullOrEmpty(Properties.Settings.Default.comPort))
            {
                CloseSerialPort();
                Properties.Settings.Default.comPort = String.Empty;
                Properties.Settings.Default.Save();

                ConnectScale();
            }
            else
            {
                ConnectScale();
            }

        }

        private void btnFinishAll_Click(object sender, EventArgs e)
        {
            inputWeight();
        }
    }
}
