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
    class SettingsMediator : ObservableObject
    {
        #region Members
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
                RaisePropertyChangedEvent("PrinterSettings");
            }
        }
        #endregion

    }
}
