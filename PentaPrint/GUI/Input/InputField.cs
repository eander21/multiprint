using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.GUI.Input
{
    class InputField : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public List<InputFieldSetting> Settings { get; set; } = new List<InputFieldSetting>();

        private string value;
        public string Value {
            get { return value; }
            set
            {
                this.value = value;
                ValueChanged("Value");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void ValueChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
