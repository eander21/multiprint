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
                        if (!String.IsNullOrEmpty(Partnumber) && !Regex.IsMatch(Partnumber, @"^\d{8}$"))
                        {
                            errorMessage = "Partnumber needs to be 8 digits long";
                        }
                        break;
                    case "Serialnumber":
                        if (!String.IsNullOrEmpty(Serialnumber) && !Regex.IsMatch(Serialnumber, @"^\d{8}$")) 
                        {
                            errorMessage = "Serialnumber is required";
                        }
                        break;
                }
                return errorMessage;
            }
        }

        #endregion

        public override string GetPrint()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "PentaPrint.Model.Barcode.MainEngineTemplate.txt";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var template = reader.ReadToEnd();
                var result = template.Replace(@"|PARTNUMBER|", Partnumber);
                result = template.Replace(@"|SERIALNUMBER|", Serialnumber);
                return result;
            }
        }
        public override bool IsValid()
        {
            return String.IsNullOrEmpty(Error);
        }

        public override bool Verify(string input)
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            Partnumber = "";
            Serialnumber = "";
        }
    }
}
