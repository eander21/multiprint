using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PentaPrint.Model
{
    class MainEngineBarcode : Barcode
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

        public override bool Verify(string input)
        {
            throw new NotImplementedException();
        }
    }
}
