using PentaPrint.Mediator;
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
    class PrintCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private PrintMediator printMediator;


        public PrintCommand(PrintMediator printMediator)
        {
            this.printMediator = printMediator;
            printMediator.SubscribePrintableChanged(this.PrintableChanged);
        }
        public void PrintableChanged(string key)
        {
            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            var printable = printMediator.GetPrintable((string)parameter);
            if (printable == null || !printable.IsValid())
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            var printable = printMediator.GetPrintable((string)parameter);
            printMediator.Printer.Write(printable);
        }
    }
}
