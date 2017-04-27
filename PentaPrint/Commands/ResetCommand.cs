using PentaPrint.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PentaPrint.Commands
{
    class ResetCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
       
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var resetable = (Resetable)parameter;
            resetable.Reset();
        }
    }
}
