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
    class OpenDialog : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace.Equals("PentaPrint.View"));
            foreach (var type in types)
            {
                if (type.Name.Equals((string)parameter))
                {
                    var window = (Window)Activator.CreateInstance(type);
                    window.ShowDialog();
                }
            }
        }
    }
}
