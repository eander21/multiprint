using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.Print
{
    abstract class Barcode : Printable, Verifiable
    {
        public abstract string GetPrint();
        public abstract bool Verify(string input);
    }
}
