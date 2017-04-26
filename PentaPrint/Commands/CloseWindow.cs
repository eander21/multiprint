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
    class CloseWindow : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public CloseWindow()
        {
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Window window = (Window)parameter;
            window.Close();
        }
    }
}
