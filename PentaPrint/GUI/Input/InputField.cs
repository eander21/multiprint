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

        private Guid guid;
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

        public InputField()
        {
            guid = Guid.NewGuid();
        }

        public string getUID()
        {
            return "InputField." + Name +"."+ guid.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void ValueChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
