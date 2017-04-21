using PentaPrint.Print;
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
            Printer printer = new Printer();
        }

        private void SetupFields()
        {
            var mainPanel = this.MainContent;

            //ScrollViewer viewer = new ScrollViewer();
            //viewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            //mainPanel.Children.Add(viewer);
            var fields = ParseInputFields(Properties.Settings.Default.InputFields);


            foreach(var group in fields)
            {
                GroupBox groupBox = new GroupBox();
                groupBox.Header = group.Key;
                Grid contentGrid = new Grid();

                groupBox.Content = contentGrid;
                mainPanel.Children.Add(groupBox);

                var nameCol = new ColumnDefinition();
                nameCol.Width = GridLength.Auto;
                contentGrid.ColumnDefinitions.Add(nameCol);
                contentGrid.ColumnDefinitions.Add(new ColumnDefinition());

                var i = 0;
                foreach (var inputField in group.Value)
                {
                    contentGrid.RowDefinitions.Add(new RowDefinition());
                    Label label = new Label();
                    label.Content = inputField.Name;
                    Grid.SetRow(label, i);
                    Grid.SetColumn(label, 0);
                    contentGrid.Children.Add(label);

                    TextBox input = new TextBox();
                    input.Margin = new Thickness(1);
                    Grid.SetRow(input, i);
                    Grid.SetColumn(input, 1);
                    contentGrid.Children.Add(input);
                    i++;
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
