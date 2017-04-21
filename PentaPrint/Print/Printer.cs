using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.Print
{
    class Printer
    {
        private SerialPort Serial { get; set; }
        public Printer()
        {
            var port = Properties.Settings.Default.PrinterPort;
            Serial = new SerialPort(port);
            Serial.BaudRate = Properties.Settings.Default.PrinterBaud;
            Serial.Open();
        }

        ~Printer()
        {
            Serial.Close();
        }
    }
}
