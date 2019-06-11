using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PentaPrint.Exception
{
    class PrinterException : System.Exception
    {
        public PrinterException() { }

        public PrinterException(string msg) : base(msg) { }
    }
}
