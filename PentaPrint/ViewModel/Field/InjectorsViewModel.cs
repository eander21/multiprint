using PentaPrint.Mediator;
using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.ViewModel.Field
{
    class InjectorsViewModel : FieldViewModel
    {
        #region Members
        private IPrint _dataMatrix;
        public IPrint DataMatrix
        {
            get
            {
                return _dataMatrix;
            }
            set
            {
                _dataMatrix = value;
                RaisePropertyChangedEvent("DataMatrix");
            }
        }
        #endregion
        public InjectorsViewModel()
        {
            DataMatrix = new InjectorDataMatrix();
            PrintMediator.Instance.AddPrintable("Injectors", DataMatrix);
            PrintMediator.Instance.SubscribePrintableChanged(PrintableChanged);
        }

        public void PrintableChanged(string str)
        {
            if (str.Equals("Injectors"))
                DataMatrix = PrintMediator.Instance.GetPrintable(str);
        }
    }
}
