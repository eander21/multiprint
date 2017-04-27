using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.Model
{
    abstract class DataMatrix : ObservableObject, IPrint
    {
        public abstract string GetPrint();
        public abstract bool IsValid();
        public abstract bool Verify(string input);
    }
}
