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

namespace PentaPrint.Model
{
    class MEP1DataMatrix : DataMatrix, IDataErrorInfo
    {
        //Regex used to parse resolveroffset from Siemens2D
        String _resolverOffsetPattern;
        //Regex used to parse final partnumber and serialnumber from lasermarking
        String _laserMarkingPattern;

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
        }

        public override string GetPrint()
        {
            var assembly = Assembly.GetExecutingAssembly();

            String[] test = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var resourceName = "PentaPrint.Model.DataMatrix.MEP1Template.txt";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();
                result = result.Replace(@"|PARTNUMBER|", _partnumber); //TODO Replace with proper get-methods
                result = result.Replace(@"|SERIALNUMBER|", _serialnumber);
                result = result.Replace(@"|RESOLVEROFFSET|", _resolveroffset);
                return result;
            }
        }
        public override bool IsValid()
        {
            return !HasDupes() && !HasErrorMessage();
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
                        if(_scannerInput != null)
                        {
                            foreach (Match match in Regex.Matches(_scannerInput, _resolverOffsetPattern))
                            {
                                Group group = match.Groups["resolv"];
                                foreach (Capture capture in group.Captures)
                                {
                                    _resolveroffset = capture.Value;
                                    RaisePropertyChangedEvent("ResolverOffset");
                                    _scannerInput = "";
                                    RaisePropertyChangedEvent("ScannerInput");
                                    break;
                                }
                            }
                            foreach (Match match in Regex.Matches(_scannerInput, _laserMarkingPattern))
                            {
                                Group group = match.Groups["partnumber"];
                                foreach (Capture capture in group.Captures)
                                {
                                    _partnumber = capture.Value;
                                    RaisePropertyChangedEvent("PartNumber");
                                }
                            }
                            foreach (Match match in Regex.Matches(_scannerInput, _laserMarkingPattern))
                            {
                                Group group = match.Groups["serialnumber"];
                                foreach (Capture capture in group.Captures)
                                {
                                    _serialnumber = capture.Value;
                                    RaisePropertyChangedEvent("SerialNumber");
                                    _scannerInput = "";
                                    RaisePropertyChangedEvent("ScannerInput");
                                }
                            }
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
    }
}
