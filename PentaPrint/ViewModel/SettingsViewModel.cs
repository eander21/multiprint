using PentaPrint.Commands;
using PentaPrint.Devices;
using PentaPrint.Mediator;
using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PentaPrint.ViewModel
{
    class SettingsViewModel : ObservableObject, Saveable
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

        public ICommand SaveAll { get; private set; }
        #endregion

        public SettingsViewModel()
        {
            PrinterSettings = GlobalSettings.Instance.PrinterSettings;

            AvailablePorts = new List<String>(SerialPort.GetPortNames());
            CurrentPort = Properties.Settings.Default.PrinterPort;

            AvailableBaudrates = GetBaudRates();
            CurrentBaud = Properties.Settings.Default.PrinterBaud;

            SaveAll = new SaveCommand(this);
        }

        private List<String> GetFields()
        {
            var fields = new List<String>();
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace.Equals("PentaPrint.View.Field"));
            foreach(var type in types)
            {
                fields.Add(type.Name);
            }
            return fields;
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

        public void Save()
        {
            PrinterSettings.BaudRate = CurrentBaud;
            PrinterSettings.ComPort = CurrentPort;
            PrinterSettings.Save();
        }
    }
}
