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
    public partial class MainEngine : UserControl, IDataErrorInfo
    {
        public string Partnumber { get; set; }
        public string Serialnumber { get; set; }
        private Verifiable mainEngine;
        public MainEngine()
        {
            InitializeComponent();
            DataContext = this;
            mainEngine = new MainEngineBarcode();
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
                string error = String.Empty;

                if (name == "Partnumber")
                {
                    if (!String.IsNullOrEmpty(Partnumber))
                    {
                        mainEngine.Verify(Partnumber, out error);
                        return error;
                    }
                }
                if (name == "Serialnumber")
                {
                    if (!String.IsNullOrEmpty(Serialnumber))
                    {
                        mainEngine.Verify(Serialnumber, out error);
                        return error;
                    }
                }
                return error;
            }
        }
    }
}
