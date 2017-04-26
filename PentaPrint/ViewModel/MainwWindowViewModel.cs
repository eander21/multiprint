using PentaPrint.Commands;
using PentaPrint.Devices;
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
        Printer printer = new Printer();
        public ICommand PrintAll { get; private set; }
        public ICommand OpenWindow { get; private set; }

        public MainwWindowViewModel()
        {
            PrintAll = printer;
            OpenWindow = new OpenWindow();
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
