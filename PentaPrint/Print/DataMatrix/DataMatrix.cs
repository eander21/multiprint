using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.Print
{
    abstract class DataMatrix : Printable, Verifiable
    {
        public string GetPrint()
        {
            throw new NotImplementedException();
        }
        public abstract bool Verify(string input);
    }
}
