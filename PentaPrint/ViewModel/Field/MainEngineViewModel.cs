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
        private IPrint _barcode;
        public IPrint Barcode
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
            PrintMediator.Instance.SubscribePrintableChanged(PrintableChanged);
        }

        public void PrintableChanged(string str)
        {
            if (str.Equals("MainEngine"))
                Barcode = PrintMediator.Instance.GetPrintable(str);
        }

    }
}
