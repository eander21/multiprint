using PentaPrint.Settings;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
        }

        private void SetupFields()
        {
            var mainPanel = this.MainContent;
            var fields = ParseInputFields(Properties.Settings.Default.InputFields);
            foreach(var group in fields)
            {
                GroupBox groupBox = new GroupBox();
                groupBox.Header = group.Key;
                StackPanel groupContent = new StackPanel();
                groupBox.Content = groupContent;
                mainPanel.Children.Add(groupBox);

                foreach(var inputField in group.Value)
                {
                    StackPanel fieldItem = new StackPanel();
                    fieldItem.Orientation = Orientation.Horizontal;
                    groupContent.Children.Add(fieldItem);

                    Label label = new Label();
                    label.Content = inputField.Name;
                    fieldItem.Children.Add(label);

                    TextBox input = new TextBox();
                    fieldItem.Children.Add(input);
                }

            }
        }

        private void ParseAndInsertInputField(string field, Dictionary<String, List<InputField>> fields)
        {
            var split = field.Split('|');
            if (split.Length < 2)
                throw new ArgumentException("Error parsing input fields from settings", field);
            var group = split[0];
            var name = split[1];
            List<InputFieldSetting> settings=null;
            if (split.Length > 2)
            {
                settings = new List<InputFieldSetting>();
                var settingsStrings = split[2].Split(',');
                foreach(var settingsString in settingsStrings)
                {
                    try
                    {
                        var setting = Enum.Parse(typeof(InputFieldSetting), settingsString, true);
                        settings.Add((InputFieldSetting)setting);
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
            InputField inputField = new InputField();
            inputField.Name = name;
            inputField.Settings = settings;

            List<InputField> items =null;
            try
            {
                items = fields[group];
            }
            catch (Exception)
            {
                items = new List<InputField>();
                fields.Add(group, items);
            }
           

            items.Add(inputField);

        }

        private Dictionary<String, List<InputField>> ParseInputFields(StringCollection inputFields)
        {
            var fields = new Dictionary<String, List<InputField>>();
            foreach (string field in inputFields)
            {
                ParseAndInsertInputField(field, fields);
            }
            return fields;
        }
    }
}
