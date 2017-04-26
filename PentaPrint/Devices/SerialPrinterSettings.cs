using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.Devices
{
    class SerialPrinterSettings : ObservableObject
    {
        #region Members
        private string _port;
        public string ComPort
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
                RaisePropertyChangedEvent("ComPort");
            }
        }
        private int _baud;
        public int BaudRate
        {
            get
            {
                return _baud;
            }
            set
            {
                _baud = value;
                RaisePropertyChangedEvent("BaudRate");
            }
        }
        #endregion

        public SerialPrinterSettings()
        {
            ComPort = Properties.Settings.Default.PrinterPort;
            BaudRate = Properties.Settings.Default.PrinterBaud;
        }

        public void Save()
        {
            Properties.Settings.Default.PrinterPort = ComPort;
            Properties.Settings.Default.PrinterBaud = BaudRate;
            Properties.Settings.Default.Save();
        }
    }
}
