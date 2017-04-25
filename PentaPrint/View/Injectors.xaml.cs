using PentaPrint.Model;
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
    /// Interaction logic for MainEngine.xaml
    /// </summary>
    public partial class Injectors : UserControl
    {
        private InjectorsViewModel viewModel = new InjectorsViewModel();
        public Injectors()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
