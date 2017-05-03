using PentaPrint.Mediator;
using PentaPrint.Model;
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
    class VerifyPrint : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private VerificationMediator mediator;

        private bool verifyAll;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        public void PrintableChanged(string key)
        {
            RaiseCanExecuteChanged();
        }

        public VerifyPrint(VerificationMediator mediator, bool all=false)
        {
            this.mediator = mediator;
            verifyAll = all;
        }
       
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var input = (string)parameter;
            if (verifyAll)
                mediator.VerifyAll();
        }
    }
}
