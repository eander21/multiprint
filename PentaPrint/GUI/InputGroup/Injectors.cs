using PentaPrint.Print;
using PentaPrint.GUI.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace PentaPrint.GUI.InputGroup
{
    class Injectors : InputGroup
    {
        InjectorDataMatrix DataMatrix { get; set; }
        private static int N_INJECTORS = 5;

        public Injectors() : base()
        {
            inputFields = new List<InputField>();
            for (var i = 1; i <= N_INJECTORS; i++)
            {
                InputField inputField = new InputField();
                inputField.Name = "Injector" + i;
                inputFields.Add(inputField);
            }
        }

        protected override string getGroupName()
        {
            return "Injectors";
        }
    }
}
