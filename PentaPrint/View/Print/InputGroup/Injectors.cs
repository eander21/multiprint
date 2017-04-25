using PentaPrint.Model;
using PentaPrint.View.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace PentaPrint.View.InputGroup
{
    class Injectors : InputGroup
    {
        InjectorDataMatrix DataMatrix { get; set; }

        protected override string getGroupName()
        {
            return "Injectors";
        }

        public override void AttachControls(IAddChild parent)
        {
            Grid contentGrid = createAndAttachGrid(parent);

            for(var i = 0; i < 5; i++)
            {
                createAndAttachLabel(contentGrid, "Injector "+(i+1), i);
                TextBox injector = createAndAttachTextbox(contentGrid, "Injector", i);
                BindValue(DataMatrix, injector, "Injector"+i);
            }
        }
    }
}
