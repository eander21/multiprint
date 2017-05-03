using PentaPrint.Model;
using PentaPrint.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PentaPrint.View.Verification
{
    /// <summary>
    /// Interaction logic for MainEngine.xaml
    /// 
    /// Not using MVVM because the lack of time
    /// </summary>
    public partial class Injectors : UserControl, IDataErrorInfo
    {
        public string Value { get; set; }
        private Verifiable mainEngine;
        public Injectors()
        {
            InitializeComponent();
            DataContext = this;
            mainEngine = new InjectorDataMatrix();
        }


        public string Error
        {
            get
            {
                return "";
            }
        }

        public string this[string name]
        {
            get
            {
                string error = null;

                if (name == "Value")
                {
                    if (!String.IsNullOrEmpty(Value))
                    {
                        mainEngine.Verify(Value, out error);
                    }
                }
                return error;
            }
        }
    }
}
