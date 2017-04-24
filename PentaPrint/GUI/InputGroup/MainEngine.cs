using PentaPrint.GUI.Input;
using PentaPrint.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace PentaPrint.GUI.InputGroup
{
    class MainEngine : InputGroup
    {
        MainEngineBarcode Barcode { get; set; }

        public MainEngine() : base()
        {
            inputFields = new List<InputField>();
            InputField partnumber = new InputField();
            partnumber.Name = "Partnumber";
            partnumber.Settings.Add(InputFieldSetting.Persistant);
            inputFields.Add(partnumber);

            InputField serialnumber = new InputField();
            serialnumber.Name = "Serialnumber";
            inputFields.Add(serialnumber);
        }

        protected override string getGroupName()
        {
            return "Main Engine";
        }
    }
}
