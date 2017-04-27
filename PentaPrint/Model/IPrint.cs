using PentaPrint.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.Model
{
    interface IPrint : Printable, Verifiable, Resetable
    {
    }
}
