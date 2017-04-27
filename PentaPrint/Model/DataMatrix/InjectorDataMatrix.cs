using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.Model
{
    class InjectorDataMatrix : DataMatrix
    {
        #region Injector members
        private string _injector1;
        public string Injector1
        {
            get
            {
                return _injector1;
            }
            set
            {
                _injector1 = value;
                RaisePropertyChangedEvent("Injector1");
            }
        }
        private string _injector2;
        public string Injector2
        {
            get
            {
                return _injector2;
            }
            set
            {
                _injector2 = value;
                RaisePropertyChangedEvent("Injector2");
            }
        }
        private string _injector3;
        public string Injector3
        {
            get
            {
                return _injector3;
            }
            set
            {
                _injector3 = value;
                RaisePropertyChangedEvent("Injector3");
            }
        }
        private string _injector4;
        public string Injector4
        {
            get
            {
                return _injector4;
            }
            set
            {
                _injector4 = value;
                RaisePropertyChangedEvent("Injector4");
            }
        }
        private string _injector5;
        public string Injector5
        {
            get
            {
                return _injector5;
            }
            set
            {
                _injector5 = value;
                RaisePropertyChangedEvent("Injector5");
            }
        }
        #endregion

        public override string GetPrint()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "PentaPrint.Model.DataMatrix.InjectorTemplate.txt";
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
        public override bool IsValid()
        {
            return true;
            //throw new NotImplementedException();
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
