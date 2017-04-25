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
        public string Port { get; set; } 
        public int Baud { get; set; }
        public Printer()
        {
            Port = Properties.Settings.Default.PrinterPort;
            Baud = Properties.Settings.Default.PrinterBaud;
            Serial = new SerialPort(Port);
            Serial.BaudRate = Baud;
            try
            {
                Serial.Open();
                CanExecuteChanged?.Invoke(this, null);
            } catch (Exception e)
            {
                Console.WriteLine("Could not open serial " + Port + " at " + Baud);
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
            if(parameter is string)
            {
                Write((string)parameter);
            } else if (parameter is Printable)
            {
                Write((Printable)parameter);
            }
        }

        ~Printer()
        {
            if(Serial!= null && Serial.IsOpen)
            {
                Serial.Close();
                CanExecuteChanged?.Invoke(this, null);
            }
            SaveSettings();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.PrinterPort = Port;
            Properties.Settings.Default.PrinterBaud = Baud;
            Properties.Settings.Default.Save();
        }
    }
}
