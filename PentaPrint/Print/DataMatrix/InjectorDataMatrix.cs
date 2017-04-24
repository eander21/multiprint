using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.Print
{
    class InjectorDataMatrix : DataMatrix
    {
        List<String> Injectors { get; set; }

        public override string GetPrint()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "PentaPrint.Print.DataMatrix.InjectorTemplate.txt";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();
                result = result.Replace(@"|FULLCODE|", GetFullCode());
                result = result.Replace(@"|SERIALNUMBER|", GetSerialNumber());
                result = result.Replace(@"|CLASSIFICATIONS|", GetClassifications());
                return result;
            }
        }

        public override bool Verify(string input)
        {
            throw new NotImplementedException();
        }

        private string GetFullCode()
        {
            throw new NotImplementedException();
        }
        private string GetSerialNumber()
        {
            throw new NotImplementedException();
        }
        private string GetClassifications()
        {
            throw new NotImplementedException();
        }
    }
}
