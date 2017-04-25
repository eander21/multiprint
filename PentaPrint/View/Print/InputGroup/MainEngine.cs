using PentaPrint.View.Input;
using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Controls;

namespace PentaPrint.View.InputGroup
{
    class MainEngine : InputGroup
    {
        private MainEngineBarcode barcode = new MainEngineBarcode();

        protected override string getGroupName()
        {
            return "Main Engine";
        }

        public override void AttachControls(IAddChild parent)
        {
            Grid contentGrid = createAndAttachGrid(parent);

            createAndAttachLabel(contentGrid, "Partnumber", 0);
            TextBox partNumber = createAndAttachTextbox(contentGrid, "Partnumber", 0);
            BindValue(barcode, partNumber, "Partnumber");

            createAndAttachLabel(contentGrid, "Serialnumber", 1);
            TextBox serialNumber = createAndAttachTextbox(contentGrid, "Serialnumber", 1);
            BindValue(barcode, serialNumber, "Serialnumber");
        }

    }
}
