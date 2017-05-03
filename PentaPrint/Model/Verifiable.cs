using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.Model
{
    interface Verifiable
    {
        bool Verify(string input, out string errorText);
    }
}
