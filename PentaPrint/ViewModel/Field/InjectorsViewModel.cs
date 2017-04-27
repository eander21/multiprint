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
        private InjectorDataMatrix _dataMatrix;
        public InjectorDataMatrix DataMatrix
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
        }
    }
}
