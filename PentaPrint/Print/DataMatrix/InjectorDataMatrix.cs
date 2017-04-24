using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.Print
{
    class InjectorDataMatrix : DataMatrix
    {
        List<String> Injectors { get; set; }

        

        public override bool Verify(string input)
        {
            throw new NotImplementedException();
        }
    }
}
