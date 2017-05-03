using PentaPrint.ViewModel;
using System;
using System.Collections.Generic;
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

namespace PentaPrint.View
{
    /// <summary>
    /// Interaction logic for VerificationControl.xaml
    /// </summary>
    public partial class VerificationControl : UserControl
    {
        public static readonly DependencyProperty _labelContent =
        DependencyProperty.Register("LabelContent", typeof(string), typeof(VerificationControl));
        public string LabelContent
        {
            get
            {
                return this.GetValue(_labelContent) as string;
            }
            set
            {
                this.SetValue(_labelContent, value);
                viewModel.Key = value;
            }
        }
        private VerificationControlViewModel viewModel;
        public VerificationControl()
        {
            InitializeComponent();
            viewModel = new VerificationControlViewModel();
            DataContext = viewModel;
        }
    }
}
