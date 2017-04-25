using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PentaPrint.Devices
{
    class Printer : ICommand
    {
        private SerialPort Serial { get; set; }
        private string port;
        private int baud;
        public Printer()
        {
            port = Properties.Settings.Default.PrinterPort;
            baud = Properties.Settings.Default.PrinterBaud;
            Serial = new SerialPort(port);
            Serial.BaudRate = baud;
            try
            {
                Serial.Open();
            } catch (Exception e)
            {
                Console.WriteLine("Could not open serial " + port + " at " + baud);
                Console.WriteLine(e);
            }
        }

        public event EventHandler CanExecuteChanged;

        public void Write(String str)
        {
            Serial.Write(str);
        }

        public void Write(Printable print)
        {
            Serial.Write(print.GetPrint());
        }

        public bool CanExecute(object parameter)
        {
            return Serial != null && Serial.IsOpen;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        ~Printer()
        {
            if(Serial!= null && Serial.IsOpen)
                Serial.Close();
            SaveSettings();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.PrinterPort = port;
            Properties.Settings.Default.PrinterBaud = baud;
            Properties.Settings.Default.Save();
        }
    }
}
