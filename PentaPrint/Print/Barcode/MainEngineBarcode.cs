using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PentaPrint.Print
{
    class MainEngineBarcode : Barcode
    {
        String Partnumber { get; set; }
        String Serialnumber { get; set; }

        public override string GetPrint()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "PentaPrint.Print.Barcode.MainEngineTemplate.txt";
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
