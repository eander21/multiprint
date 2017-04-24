using PentaPrint.GUI.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace PentaPrint.GUI.InputGroup
{
    abstract class InputGroup
    {
        protected string GROUP_NAME = null;
        protected abstract string getGroupName();
        protected List<InputField> inputFields;

        public InputGroup()
        {
            GROUP_NAME = getGroupName();
        }

        public void AttachControls(IAddChild parent)
        {
            GroupBox groupBox = new GroupBox();
            groupBox.Header = GROUP_NAME;
            Grid contentGrid = new Grid();

            groupBox.Content = contentGrid;
            parent.AddChild(groupBox);

            var nameCol = new ColumnDefinition();
            nameCol.Width = GridLength.Auto;
            contentGrid.ColumnDefinitions.Add(nameCol);
            contentGrid.ColumnDefinitions.Add(new ColumnDefinition());

            var row = 0;
            foreach (var inputField in inputFields)
            {
                contentGrid.RowDefinitions.Add(new RowDefinition());
                Label label = new Label();
                label.Content = inputField.Name;
                Grid.SetRow(label, row);
                Grid.SetColumn(label, 0);
                contentGrid.Children.Add(label);

                TextBox input = new TextBox();
                input.Margin = new Thickness(1);
                Grid.SetRow(input, row);
                Grid.SetColumn(input, 1);
                BindValue(inputField, input, inputField.Name);
                contentGrid.Children.Add(input);
                row++;
            }
        }

        private void BindValue(InputField field, TextBox fe, string bindingName)
        {
            Binding binding = new Binding(bindingName);
            BindingOperations.SetBinding(fe, TextBox.TextProperty, binding);
        }
    }
}
