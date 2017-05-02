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
    class PrintAll : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private PrintMediator printMediator;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        public void PrintableChanged(string key)
        {
            RaiseCanExecuteChanged();
        }

        public PrintAll(PrintMediator printMediator)
        {
            this.printMediator = printMediator;
            printMediator.SubscribePrintableChanged(this.PrintableChanged);
        }
       
        public bool CanExecute(object parameter)
        {
            if (!GlobalSettings.Instance.PrintValidation)
                return true;

            foreach (var print in printMediator.GetAllPrintables())
            {
                if (!print.Value.IsValid())
                    return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            foreach(var print in printMediator.GetAllPrintables())
            {
                printMediator.Printer.Write(print.Value);
            }
        }
    }
}
