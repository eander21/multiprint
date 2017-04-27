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
        public ICommand PrintAll { get; private set; }
        public ICommand OpenDialog { get; private set; }

        public MainwWindowViewModel()
        {
            PrintAll = new PrintAll(printMediator);
            OpenDialog = new OpenDialog();
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
