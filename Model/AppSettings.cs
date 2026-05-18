using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace mixer_control_globalver.Model
{
    public class AppSettings
    {
        //Base settings
        public int Language { get; set; } = 0; //Language setting

        public string FormulaDirectory { get; set; } //Directory for formulas

        public bool SaveReportEnabled { get; set; } = false; //Is save report enabled
        public string ReportDirectory { get; set; } //Directory for reports
        public bool EndReportEnabled { get; set; } = false; //Is end report enabled

        public double OilToleranceMass { get; set; } = 1; //Oil tolerance mass setting

        //Logic settings
        public bool OilSupplyEnabled { get; set; } = false;//Is oil supply enabled

        //Sublogic settings
        public bool ShowHiddenInfo { get; set; } = false; //Should hidden info be shown
        public bool DeveloperMode { get; set; } = false; //Is developer mode enabled
        public bool MultipleOilTestEnabled { get; set; } = false; //Is multiple oil test enabled


        public bool StopMachineBetweenRuns { get; set; } = false; //Should the machine stop between runs
        public bool OpenMixerLidAfterRun { get; set; } = false; //Should the mixer lid open after run
        public bool ShowPowderAlert { get; set; } = false; //Should powder alert be shown
        public bool AlwaysOpenMixerLid { get; set; } = false; //Should the mixer lid always be opened


        public bool SkipPasswordEnabled { get; set; } = false; //Is skip password enabled
        public string SkipStepPassword { get; set; } //Password for skipping steps

        public bool OIlTested { get; set; } = false; //Is oil tested
        public string OIlTestedTime { get; set; } //Time for oil tested

        public bool CheckPowderEnabled { get; set; } = false; //Is check powder enabled

        public bool FlowMeterEnabled { get; set; } = false; //Is flow meter enabled
        public bool EnableSecondaryOilSupply { get; set; } = false; //Is flow meter enabled

        //PLC settings
        public string PlcIp { get; set; } //IP address of the PLC
        public int DatabaseNumber { get; set; } = 0; //DB number for PLC communication
        public int MaxSpeed { get; set; } = 0; //Maximum speed setting
        public double SpindleDiameter { get; set; } = 0; //Diameter of the spindle
        public double SensorDiameter { get; set; } = 0; //Diameter of the sensor
        public double TransmissionRatio { get; set; } = 0; //Transmission ratio setting

        //OIl supply settings
        public string OilSupplyComPort { get; set; } //COM port for oil supply
        public string SecondaryOilSupplyComPort { get; set; } //COM port for secondary oil supply
        public string OilSupplyBaudRate { get; set; } = "9600"; //Baud rate for oil supply communication
        public string OilSupplyDataBits { get; set; } = "8"; //Data bits setting for oil supply communication
        public string OilSupplyParity { get; set; } = "None"; //Parity setting for oil supply communication
        public string OilSupplyStopBits { get; set; } = "One"; //Stop bits setting for oil supply communication
        public int OilSupplyAttempts { get; set; } = 30; //Number of attempts for oil supply communication
        public double VolumnCompensation { get; set; } = 0; //Volume compensation setting for oil supply
        public double VolumnCompareValue { get; set; } = 0.005; //Volume comparison setting for oil supply

        //Led screen settings
        public string LedScreenIp { get; set; } //IP address of the LED screen
        public int LedScreenColor { get; set; } = 2; //Color setting for the LED screen
        public int LedScreenStyle { get; set; } = 9; //Style setting for the LED screen

        //Flow meter settings
        public string FlowMeterComPort { get; set; } //COM port for flow meter

        //Exact match settings
        public bool ExactMatchEnabled { get; set; } = false; //Is exact match enabled

        //OIl pump new IP connector
        public bool OilPumpNewIpConnectorEnabled { get; set; } = false; //Is new IP connector for oil pump enabled
        public string OilPumpIp { get; set; } //IP address for oil pump connection
        public int OilPumpPort { get; set; } //Port for oil pump connection
    }
}
