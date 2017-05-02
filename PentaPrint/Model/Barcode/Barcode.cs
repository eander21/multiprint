using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.Model
{
    abstract class Barcode : ObservableObject, IPrint
    {
        public abstract object Clone();
        public abstract string GetPrint();
        public abstract bool IsValid();
        public abstract void Reset();
        public abstract bool Verify(string input);
    }
}
