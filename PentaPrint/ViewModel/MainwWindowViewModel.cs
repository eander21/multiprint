using PentaPrint.Commands;
using PentaPrint.Devices;
using PentaPrint.Mediator;
using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PentaPrint.ViewModel
{
    class MainwWindowViewModel
    {
        PrintMediator printMediator = PrintMediator.Instance;
        public DelegateCommand PrintAll { get; private set; }
        public ICommand OpenDialog { get; private set; }

        public MainwWindowViewModel()
        {
            PrintAll = new DelegateCommand(
            (s) => PrintAllPrintables()
            , () => ValidateAllPrintables());
            printMediator.SubscribePrintableChanged(PrintAll.RaiseCanExecuteChanged);
            OpenDialog = new OpenDialog();
        }

        public void PrintAllPrintables()
        {
            foreach (var print in printMediator.GetAllPrintables())
            {
                printMediator.Printer.Write(print.Value);
            }
        }

        public bool ValidateAllPrintables()
        {
            foreach (var print in printMediator.GetAllPrintables())
            {
                if (!print.Value.IsValid())
                    return false;
            }
            return true;
        }

        public void BindValue(IPrint print, TextBox textBox, string bindingName)
        {
            Binding binding = new Binding(bindingName);
            binding.Source = print;
            binding.Mode = BindingMode.TwoWay;
            textBox.SetBinding(TextBox.TextProperty, binding);
        }
    }
}
