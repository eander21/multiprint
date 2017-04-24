using PentaPrint.GUI;
using PentaPrint.Print;
using PentaPrint.GUI.Input;
using PentaPrint.GUI.InputGroup;
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

namespace PentaPrint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupFields();
            Printer printer = new Printer();
        }

        private void SetupFields()
        {
            var inputPanel = this.InputContent;
            MainEngineBarcode m = new MainEngineBarcode();
            m.GetPrint();
            var verifyPanel = this.VerifyContent;

            setupInputFields(inputPanel);
            setupVerifyFields(verifyPanel);
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
                group.AttachControls(mainPanel);
            }
        }

        private List<InputGroup> ParseInputFields(StringCollection inputFields)
        {
            List<InputGroup> result = new List<InputGroup>();
            foreach (string field in inputFields)
            {
                var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace.StartsWith("PentaPrint.GUI.InputGroup") && !t.IsAbstract);
                foreach(var type in types)
                {
                    if (type.Name.Equals(field))
                    {
                        var instance = Activator.CreateInstance(type);
                        result.Add((InputGroup)instance);
                    }
                }
            }
            return result;
        }
    }
}
