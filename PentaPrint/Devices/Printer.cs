using PentaPrint.Mediator;
using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PentaPrint.Devices
{
    class Printer : ObservableObject, ICommand
    {
        #region Members
        private SerialPort Serial { get; set; }
        private SerialPrinterSettings _settings;
        public SerialPrinterSettings Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                _settings = value;
                RaisePropertyChangedEvent("Settings");
            }
        }
        #endregion

        public Printer()
        {
            Settings = GlobalSettings.Instance.PrinterSettings;
            GlobalSettings.Instance.SubscribePrinterSettings(SettingsChanged);

            InitializePrinter();
        }

        private void InitializePrinter()
        {
            DisposePrinter();

            Serial = new SerialPort(_settings.ComPort);
            Serial.BaudRate = _settings.BaudRate;
            try
            {
                Serial.Open();
                CanExecuteChanged?.Invoke(this, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not open serial " + _settings.ComPort + " at " + _settings.BaudRate);
                Console.WriteLine(e);
            }
        }

        private void DisposePrinter()
        {
            if (Serial != null && Serial.IsOpen) {
                Serial.Close();
                Serial = null;
                CanExecuteChanged?.Invoke(this, null);
            }
        }

        public event EventHandler CanExecuteChanged;

        public void Write(String str)
        {
            Serial.Write(str);
        }

        public void Write(Printable print)
        {
            Serial.Write(print.GetPrint());
        }

        public bool CanExecute(object parameter)
        {
            return Serial != null && Serial.IsOpen;
        }

        public void Execute(object parameter)
        {
            if(parameter is string)
            {
                Write((string)parameter);
            } else if (parameter is Printable)
            {
                Write((Printable)parameter);
            }
        }

        ~Printer()
        {
            DisposePrinter();
            Settings.Save();
        }

        public void SettingsChanged(SerialPrinterSettings settings)
        {
            Settings = settings;
            InitializePrinter();
        }


    }
}
