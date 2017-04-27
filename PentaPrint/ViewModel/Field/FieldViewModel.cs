using PentaPrint.Commands;
using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PentaPrint.ViewModel.Field
{
    abstract class FieldViewModel : ObservableObject
    {
        public ICommand Reset { get; set; }
        public FieldViewModel()
        {
            Reset = new ResetCommand();
        }
    }
}
