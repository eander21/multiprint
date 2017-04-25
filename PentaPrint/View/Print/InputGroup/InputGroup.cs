using PentaPrint.Model;
using PentaPrint.View.Input;
using PentaPrint.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace PentaPrint.View.InputGroup
{
    abstract class InputGroup
    {
        protected string GROUP_NAME = null;
        protected abstract string getGroupName();
        protected PrintViewModel viewModel = new PrintViewModel();

        public InputGroup()
        {
            GROUP_NAME = getGroupName();
        }

        public abstract void AttachControls(IAddChild parent);

        public void BindValue(IPrint print, TextBox textBox, string bindingName)
        {
            Binding binding = new Binding(bindingName);
            binding.Source = print;
            binding.Mode = BindingMode.TwoWay;
            textBox.SetBinding(TextBox.TextProperty, binding);
        }

        protected Grid createAndAttachGrid(IAddChild parent)
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
            return contentGrid;
        }

        protected void createAndAttachLabel(Grid contentGrid, string name, int row)
        {
            contentGrid.RowDefinitions.Add(new RowDefinition());
            Label label = new Label();
            label.Content = name;
            Grid.SetRow(label, row);
            Grid.SetColumn(label, 0);
            contentGrid.Children.Add(label);
        }

        protected TextBox createAndAttachTextbox(Grid contentGrid, string name, int row)
        {
            TextBox input = new TextBox();
            input.Margin = new Thickness(1);

            Grid.SetRow(input, row);
            Grid.SetColumn(input, 1);
            contentGrid.Children.Add(input);
            return input;
        }


    }
}
