using PentaPrint.Mediator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PentaPrint.Model
{
    class MEP1DataMatrix : DataMatrix, IDataErrorInfo
    {
        //Regex used to parse resolveroffset from Siemens2D
        String _resolverOffsetPattern;
        //Regex used to parse final partnumber and serialnumber from lasermarking
        String _laserMarkingPattern;
        //Dictionary used to map a final partnumber to a variant (ERAD or EFAD)
        Dictionary<String, String> variantMap = new Dictionary<String, String>();
        //Dictionary used to map a final partnumber to a certification-code
        Dictionary<String, String> certificationCodeMap = new Dictionary<String, String>();
        //Print automatically if all conditions are fulfilled
        Boolean _autoPrint = false;
        Boolean displayPopup = true;
        Boolean _previouslyPrinted = false;
        Queue<string> printHistory = new Queue<string>();

        #region Members
        private string _scannerInput;
        public String ScannerInput
        {
            get
            {
                return _scannerInput;
            }
            set
            {
                _scannerInput = value;
                displayPopup = true;
                RaisePropertyChangedEvent("ScannerInput");
            }
        }
        private string _partnumber;
        public String PartNumber
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
        public String SerialNumber
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
        private string _resolveroffset = "";
        public String ResolverOffset
        {
            get
            {
                return _resolveroffset;
            }
            set
            {
                _resolveroffset = value;
                RaisePropertyChangedEvent("ResolverOffset");
            }
        }
        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { return String.Empty; }
        }
        #endregion
        private PrintMediator printMediator = PrintMediator.Instance;
        
        public MEP1DataMatrix()
        {
            //TODO Add error handling if properties are missing
            _resolverOffsetPattern = Properties.Settings.Default.ResolverOffsetPattern;
            _laserMarkingPattern = Properties.Settings.Default.LaserMarkingPattern;
            _autoPrint = Properties.Settings.Default.EnableAutoprint;
            SetupVariants();
            SetupCertificationCodeMap();
        }

        public override string GetPrint()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "PentaPrint.Model.DataMatrix.MEP1Template.txt";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();
                result = result.Replace(@"|VARIANT|", GetVariant());
                result = result.Replace(@"|DMCINFO|", GetDMCInfo());
                result = result.Replace(@"|SERIALNUMBER|", _serialnumber);
                result = result.Replace(@"|SUPPLIERID|", GetSupplierId());
                result = result.Replace(@"|CERTIFICATIONCODE|", GetCertificationCode());
                result = result.Replace(@"|ORIGIN|", GetOrigin());
                result = result.Replace(@"|PARTNUMBER|", _partnumber);                                
                return result;
            }
        }

        public override bool Verify(string input, out string errorText)
        {
            if (String.IsNullOrEmpty(input))
            {
                errorText = "ERROR ERROR, MY ROBOT BALLS";
                return false;
            }
            if (!input.StartsWith("SP"))
            {
                errorText = "DataMatrix does not start with SP";
                return false;
            }
            if (!Regex.IsMatch(input, @".*%<$"))
            {
                errorText = "DataMatrix does not include valid end sequence (%<)";
                return false;
            }
            if (!Regex.IsMatch(input, @"^SP\d+"))
            {
                errorText = "DataMatrix does not include a valid serial number";
                return false;
            }
            if (!Regex.IsMatch(input, @"^SP\d+%##"))
            {
                errorText = "DataMatrix does not include a valid serial number separator (%##)";
                return false;
            }
            if (!Regex.IsMatch(input, @"^SP\d+%##(.{8}){5}%<$"))
            {
                errorText = "The Injector-part of the DataMatrix does not contain correct amount of characters";
                return false;
            }

            errorText = null;
            return true;
        }

        public override void Reset()
        {
            
        }

        private bool IsValid(string injector, bool parse=false)
        {
            return false;
        }

        private string ParseInjector(string injector)
        {
            return null;
        }

        #region IDataErrorInfo Members

        public string this[string columnName]
        {
            get
            {
                String errorMessage = String.Empty;
                switch (columnName)
                {
                    case "ScannerInput":
                        if(!String.IsNullOrEmpty(_scannerInput))
                        {
                            foreach (Match match in Regex.Matches(_scannerInput, _resolverOffsetPattern))
                            {
                                Group group = match.Groups["resolv"];
                                foreach (Capture capture in group.Captures)
                                {
                                    ResolverOffset = capture.Value;
                                    ScannerInput = "";
                                    AutoPrint();
                                    break;
                                }
                            }
                            foreach (Match match in Regex.Matches(_scannerInput, _laserMarkingPattern))
                            {
                                Group group = match.Groups["partnumber"];
                                foreach (Capture capture in group.Captures)
                                {
                                    PartNumber = capture.Value;                                    
                                }
                            }
                            foreach (Match match in Regex.Matches(_scannerInput, _laserMarkingPattern))
                            {
                                Group group = match.Groups["serialnumber"];
                                foreach (Capture capture in group.Captures)
                                {
                                    SerialNumber = capture.Value;
                                    ScannerInput = "";                                    
                                }
                            }
                            AutoPrint();
                            break;
                        }                        
                        break;
                }                
                return errorMessage;
            }
        }

        private bool HasDupes()
        {
            return false;
        }
        private bool HasErrorMessage()
        {
            return false;
        }

        private string CheckDupes(string injector)
        {
            return null;
        }

        private string GetErrorMessage(string inputInjector)
        {
            return null;
        }

        #endregion

        //TODO clone members of MEP1DataMatrix
        public override object Clone()
        {
            MEP1DataMatrix clone = new MEP1DataMatrix();
            return clone;
        }

        private String GetDMCInfo()
        {
            return "P" + _partnumber + "#T" + SerialNumber + "#V" + GetSupplierId() + "#Z" + _resolveroffset + "#";
        }

        private String GetSupplierId()
        {
            return Properties.Settings.Default.SupplierId;
        }

        private String GetCertificationCode()
        {
            try
            {
                return certificationCodeMap[_partnumber];
            }
            catch (KeyNotFoundException kex)
            {
                MessageBox.Show("Error: Could not map partnumber: " + _partnumber + " to a cerification code."
                    , "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return "";
            }
        }

        private String GetVariant()
        {
            try
            {
                return variantMap[_partnumber];
            }
            catch (KeyNotFoundException kex){
                return "";
            }
        }

        private String GetOrigin()
        {
            return Properties.Settings.Default.Origin;
        }

        private void SetupVariants()
        {
            System.Collections.Specialized.StringCollection variantList = Properties.Settings.Default.VariantMap;
            foreach(String entry in variantList)
            {
                String[] split = entry.Split(';');
                if(split.Length == 2)
                {
                    variantMap.Add(split[0], split[1]);
                }
            }
        }

        private void SetupCertificationCodeMap()
        {
            System.Collections.Specialized.StringCollection certificationList = Properties.Settings.Default.CertificationCodeMap;
            foreach (String entry in certificationList)
            {
                String[] split = entry.Split(';');
                if (split.Length == 2)
                {
                    certificationCodeMap.Add(split[0], split[1]);
                }
            }
        }

        /// <summary>
        /// Print automatically if all conditions are fulfilled
        /// </summary>
        private void AutoPrint()
        {
            if (_autoPrint && AllFieldsSet())
            {
                this.printMediator.Printer.Execute(this.GetPrint());
                PartNumber = null;
                SerialNumber = null;
                ResolverOffset = null;
                ScannerInput = null;
            }            
        }

        public override bool IsValid()
        {
            return AllFieldsSet();
        }

        /// <summary>
        /// Check that all parameters are set
        /// </summary>
        /// <returns></returns>
        private Boolean AllFieldsSet()
        {
            if(String.IsNullOrEmpty(PartNumber) || String.IsNullOrEmpty(SerialNumber) || String.IsNullOrEmpty(ResolverOffset))
            {
                return false;
            }
            return true; ;
        }

    }
}
