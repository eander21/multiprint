using PentaPrint.View;
using PentaPrint.Model;
using PentaPrint.View.Input;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PentaPrint.Devices;
using PentaPrint.ViewModel;
using System.Text.RegularExpressions;

namespace PentaPrint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainwWindowViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainwWindowViewModel();
            DataContext = viewModel;
            SetupFields();
            SetupMenu();
        }

        private void SetupMenu()
        {
            var menu = this.PrintMenu;
            foreach (string item in Properties.Settings.Default.InputFields)
            {
                var menuItem = new MenuItem();
                menuItem.Header = "Print: " + SplitCamel(item);
                menu.Items.Add(menuItem);
            }
        }
        private string SplitCamel(string input)
        {
            var split = Regex.Replace(input, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})", " ");
            return Regex.Replace(split, "^ ", "_");
        }

        private void SetupFields()
        {

            var inputPanel = this.InputContent;
            var verifyPanel = this.VerifyContent;

            inputPanel.Children.Clear();
            verifyPanel.Children.Clear();

            setupInputFields(inputPanel);

            //setupVerifyFields(verifyPanel);
        }

        private void setupVerifyFields(StackPanel verifyPanel)
        {
            throw new NotImplementedException();
        }

        private void setupInputFields(StackPanel mainPanel)
        {
            var fields = ParseInputFields(Properties.Settings.Default.InputFields);

            foreach (var group in fields)
            {
                mainPanel.Children.Add(group);
            }
        }

        /// <summary>
        /// Sets up the input fields from the configuration file
        /// </summary>
        /// <param name="inputFields"></param>
        /// <returns></returns>
        private List<UIElement> ParseInputFields(StringCollection inputFields)
        {
            List<UIElement> result = new List<UIElement>();
            foreach (string field in inputFields)
            {
                var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace.Equals("PentaPrint.View.Field"));
                foreach (var type in types)
                {
                    if (type.Name.Equals(field))
                    {
                        var instance = Activator.CreateInstance(type);
                        result.Add((UIElement)instance);
                    }
                }
            }
            return result;
        }

    }
}
