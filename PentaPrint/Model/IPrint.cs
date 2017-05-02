using PentaPrint.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.Model
{
    interface IPrint : INotifyPropertyChanged, Printable, Verifiable, Resetable, ICloneable
    {
    }
}
