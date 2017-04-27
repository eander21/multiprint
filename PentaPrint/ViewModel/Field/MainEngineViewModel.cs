using PentaPrint.Mediator;
using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.ViewModel.Field
{
    class MainEngineViewModel : FieldViewModel
    {
        #region Members
        private MainEngineBarcode _barcode;
        public MainEngineBarcode Barcode
        {
            get
            {
                return _barcode;
            }
            set
            {
                _barcode = value;
                RaisePropertyChangedEvent("Barcode");
            }
        }
        #endregion

        public MainEngineViewModel ()
        {
            Barcode = new MainEngineBarcode();
            PrintMediator.Instance.AddPrintable("MainEngine", Barcode);
        }

    }
}
