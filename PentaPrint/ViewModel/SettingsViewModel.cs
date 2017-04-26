using PentaPrint.Devices;
using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.ViewModel
{
    class SettingsViewModel : ObservableObject
    {
        #region Members
        private SerialPrinterSettings _printerSettings;
        public SerialPrinterSettings PrinterSettings
        {
            get
            {
                return _printerSettings;
            }
            set
            {
                _printerSettings = value;
                RaisePropertyChangedEvent("PrinterSettings");
            }
        }

        public List<int> AvailableBaudrates { get; private set; }
        public int CurrentBaud { get; set; }
        public List<String> AvailablePorts { get; private set; }
        public string CurrentPort { get; set; }

        public List<String> AvailableFields { get; private set; }
        public List<String> CurrentFields { get; private set; }
        #endregion

        public SettingsViewModel()
        {
            AvailablePorts = new List<String>(SerialPort.GetPortNames());
            CurrentPort = Properties.Settings.Default.PrinterPort;

            AvailableBaudrates = GetBaudRates();
            CurrentBaud = Properties.Settings.Default.PrinterBaud;
        }

        private List<int> GetBaudRates()
        {
            var result = new List<int>();
            result.Add(110);
            result.Add(300);
            result.Add(600);
            result.Add(1200);
            result.Add(2400);
            result.Add(4800);
            result.Add(9600);
            result.Add(14400);
            result.Add(19200);
            result.Add(28800);
            result.Add(38400);
            result.Add(56000);
            result.Add(57600);
            result.Add(115200);
            result.Add(128000);
            result.Add(153600);
            result.Add(230400);
            result.Add(256000);
            result.Add(460800);
            result.Add(921600);
            return result;
        }
    }
}
