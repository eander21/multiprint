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
    class SaveCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Saveable saveable;

        public SaveCommand(Saveable save)
        {
            saveable = save;
        }
       
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var success = saveable.Save();
            if (success)
            {
                //MessageBox.Show("Saved Successfully","Success",MessageBoxButton.OK,MessageBoxImage.Information);
            }else
            {
                MessageBox.Show("Failed to save", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
