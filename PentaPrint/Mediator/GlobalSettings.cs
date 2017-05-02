using PentaPrint.Devices;
using PentaPrint.Model;
using PentaPrint.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.Mediator
{
    class GlobalSettings : ObservableObject
    {
        #region Members
        private static GlobalSettings _instance;
        public static GlobalSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GlobalSettings();
                return _instance;
            }
        }
        private SerialPrinterSettings printerSettings = new SerialPrinterSettings();
        public SerialPrinterSettings PrinterSettings
        {
            get
            {
                return printerSettings;
            }
            set
            {
                printerSettings = value;
                printerSettingsChanged(PrinterSettings);
                RaisePropertyChangedEvent("PrinterSettings");
            }
        }

        private Boolean _printValidation;
        public Boolean PrintValidation
        {
            get
            {
                return _printValidation;
            }
            private set
            {
                _printValidation = value;
                //Properties.Settings.Default.PrintValidation = _printValidation;
                Properties.Settings.Default.Save();
            }
        }

        public delegate void PrinterSettingsChanged(SerialPrinterSettings settings);
        private event PrinterSettingsChanged printerSettingsChanged;
        #endregion

        private GlobalSettings(){
            PrintValidation = Properties.Settings.Default.PrintValidation;
        }

        public void SignalChangePrinter()
        {
            printerSettingsChanged(PrinterSettings);
        }
        

        public void SubscribePrinterSettings(PrinterSettingsChanged func)
        {
            printerSettingsChanged += func;
        }
    }
}
