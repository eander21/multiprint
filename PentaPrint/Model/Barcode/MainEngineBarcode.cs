using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PentaPrint.Model
{
    class MainEngineBarcode : Barcode, IDataErrorInfo
    {
        #region Members
        private string _partnumber;
        public String Partnumber
        {
            get
            {
                return _partnumber;
            }
            set
            {
                _partnumber = value;
                RaisePropertyChangedEvent("Partnumber");
            }
        }
        private string _serialnumber = "";
        public String Serialnumber
        {
            get
            {
                return _serialnumber;
            }
            set
            {
                _serialnumber = value;
                RaisePropertyChangedEvent("Serialnumber");
            }
        }
        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { return String.Empty; }
        }

        public string this[string columnName]
        {
            get
            {
                String errorMessage = String.Empty;
                switch (columnName)
                {
                    case "Partnumber":
                        if (!String.IsNullOrEmpty(Partnumber) && !Regex.IsMatch(Partnumber, @"^\d+$"))
                        {
                            errorMessage = "Partnumber should contain only digits";
                        }
                        break;
                    case "Serialnumber":
                        if (!String.IsNullOrEmpty(Serialnumber) && !Regex.IsMatch(Serialnumber, @"^\d+$")) 
                        {
                            errorMessage = "Serialnumber should contain only digits";
                        }
                        break;
                }
                return errorMessage;
            }
        }

        #endregion
        public override string ToString()
        {
            return "Partnumber: " + Partnumber + ", Serialnumber: " + Serialnumber;
        }
        public override string GetPrint()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "PentaPrint.Model.Barcode.MainEngineTemplate.txt";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var template = reader.ReadToEnd();
                var result = template.Replace(@"|PARTNUMBER|", Partnumber);
                result = result.Replace(@"|SERIALNUMBER|", Serialnumber);
                return result;
            }
        }
        public override bool IsValid()
        {
            if (String.IsNullOrEmpty(Partnumber) || String.IsNullOrEmpty(Serialnumber))
                return false;

            return Regex.IsMatch(Partnumber, @"^\d+$") && Regex.IsMatch(Serialnumber, @"^\d+$");
        }

        public override bool Verify(string input, out string errorText)
        {
            if (String.IsNullOrEmpty(input))
            {
                errorText = "ERROR ERROR, MY ROBOT BALLS";
                return false;
            }
            else if(input[0]!='P' && input[0] != 'T')
            {
                errorText = "Barcode does not start with P (Partnumber) or T (Serialnumber)";
                return false;
            }
            else if (!Regex.IsMatch(input,@"[PT]\d+"))
            {
                errorText = "Barcode does not contain only numerical characters";
                return false;
            }

            errorText = null;
            return true;
        }

        public override void Reset()
        {
            //Partnumber = ""; //Partnumber is persistant
            Serialnumber = "";
            RaisePropertyChangedEvent("Serialnumber");
        }

        public override object Clone()
        {
            MainEngineBarcode clone = new MainEngineBarcode();
            clone.Partnumber = this.Partnumber;
            clone.Serialnumber = this.Serialnumber;
            return clone;
        }
    }
}
