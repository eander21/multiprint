using PentaPrint.Mediator;
using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.ViewModel.Field
{
    class MEP1ViewModel : FieldViewModel
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

        public MEP1ViewModel()
        {
            DataMatrix = new MEP1DataMatrix();
            PrintMediator.Instance.AddPrintable("MEP1", DataMatrix);
            PrintMediator.Instance.SubscribePrintableChanged(PrintableChanged);
        }

        public void PrintableChanged(string str)
        {
            if (str.Equals("MEP1"))
                DataMatrix = PrintMediator.Instance.GetPrintable(str);
        }

    }
}
