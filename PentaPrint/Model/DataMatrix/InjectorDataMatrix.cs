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
    class InjectorDataMatrix : DataMatrix, IDataErrorInfo
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
        private PrintMediator printMediator = PrintMediator.Instance;
        private MainEngineBarcode mainEngine;
        private Dictionary<char, short> convertionTable;
        private static string VALID_INJ_CHARS = "[1-8A-IK-PR-Z]";

        public InjectorDataMatrix()
        {
            try
            {
                mainEngine = (MainEngineBarcode)printMediator.GetPrintable("MainEngine");
            } catch
            {
                mainEngine = null;
            }
            setupConvertionTable();
        }

        public InjectorDataMatrix(MainEngineBarcode mainEngine)
        {
            this.mainEngine = mainEngine;
            setupConvertionTable();
        }

        private void setupConvertionTable()
        {
            convertionTable = new Dictionary<char, short>();
            convertionTable.Add('A', 0);
            convertionTable.Add('B', 1);
            convertionTable.Add('C', 2);
            convertionTable.Add('D', 3);
            convertionTable.Add('E', 4);
            convertionTable.Add('F', 5);
            convertionTable.Add('G', 6);
            convertionTable.Add('H', 7);
            convertionTable.Add('I', 8);

            convertionTable.Add('K', 9);
            convertionTable.Add('L', 10);
            convertionTable.Add('M', 11);
            convertionTable.Add('N', 12);
            convertionTable.Add('O', 13);
            convertionTable.Add('P', 14);

            convertionTable.Add('R', 15);
            convertionTable.Add('S', 16);
            convertionTable.Add('T', 17);
            convertionTable.Add('U', 18);
            convertionTable.Add('V', 19);
            convertionTable.Add('W', 20);
            convertionTable.Add('X', 21);
            convertionTable.Add('Y', 22);
            convertionTable.Add('Z', 23);
            convertionTable.Add('1', 24);
            convertionTable.Add('2', 25);
            convertionTable.Add('3', 26);
            convertionTable.Add('4', 27);
            convertionTable.Add('5', 28);
            convertionTable.Add('6', 29);
            convertionTable.Add('7', 30);
            convertionTable.Add('8', 31);
        }

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
            if (String.IsNullOrEmpty(Injector1) ||
                String.IsNullOrEmpty(Injector2) ||
                String.IsNullOrEmpty(Injector3) ||
                String.IsNullOrEmpty(Injector4) ||
                String.IsNullOrEmpty(Injector5))
                return false;

            return (IsValid(Injector1) &&
                IsValid(Injector2) &&
                IsValid(Injector3) &&
                IsValid(Injector4) &&
                IsValid(Injector5));
            //throw new NotImplementedException();
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
            if (!Regex.IsMatch(input, @"^SP\d+%##("+VALID_INJ_CHARS+ "{8}){5}%<$"))
            {
                errorText = "The Injector-part of the DataMatrix contains invalid characters";
                return false;
            }


            List<String> injectors = new List<string>();
            foreach(Match match in Regex.Matches(input, @"^SP\d+%##(?<inj>.{8}){5}%<$"))
            {
                Group group = match.Groups["inj"];
                foreach(Capture capture in group.Captures)
                {
                    injectors.Add(capture.Value);
                }
            }

            int inj = 1;
            foreach(string injector in injectors)
            {
                if (!IsValid(injector))
                {
                    errorText = "Injector "+inj+" contains checksum-error";
                    return false;
                }
                inj++;
            }


            errorText = null;
            return true;
        }

        private string GetFullCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SP");
            sb.Append(GetSerialNumber());
            sb.Append("%##");
            sb.Append(Injector1);
            sb.Append(Injector2);
            sb.Append(Injector3);
            sb.Append(Injector4);
            sb.Append(Injector5);
            sb.Append("%<");
            return sb.ToString();
        }
        private string GetSerialNumber()
        {
            return mainEngine.Serialnumber;
        }
        private string GetClassifications()
        {
            if (String.IsNullOrEmpty(Injector1) ||
                String.IsNullOrEmpty(Injector2) ||
                String.IsNullOrEmpty(Injector3) ||
                String.IsNullOrEmpty(Injector4) ||
                String.IsNullOrEmpty(Injector5))
                return "ERROR";

            StringBuilder sb = new StringBuilder();
            sb.Append(Injector1[0] + " ");
            sb.Append(Injector2[0] + " ");
            sb.Append(Injector3[0] + " ");
            sb.Append(Injector4[0] + " ");
            sb.Append(Injector5[0]);
            return sb.ToString();
        }

        public override void Reset()
        {
            Injector1 = "";
            Injector2 = "";
            Injector3 = "";
            Injector4 = "";
            Injector5 = "";
        }

        private bool IsValid(string injector)
        {
            //injector = "6A1AIB5H";
            try
            {
                //Convert String to decimal
                var shorts = GetShorts(injector);
                //Get 5bit values and concatenate
                var concat = Concatenate(shorts);
                //shift to 8 shorts by bit-size 5,7,5,7,5,4,2,5 (According to Injector Quantity Adjustment) and Correct for Special signing according to Bosch
                var iqa = GetShiftedIQA(concat);
                //Calculate the Checksum
                var checksum = GetChecksum(iqa);

                return checksum == iqa[5];
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not evaluate validity.", e);
                return false;
            }

            //return injector !=null && injector.Length==8;
        }

        private short GetChecksum(List<short> iqa)
        {
            short sum = (short)iqa.Sum(s => s);
            sum -= (short)iqa[0]; //Subtract classification
            sum -= (short)iqa[5]; //Subtract current checksum
            short highNibble = (byte)((0xF0 & sum) >>4);
            short lowNibble = (byte)(0x0F & sum);


            var checksum = (short)((int)BoschSign(highNibble,4) + (int)BoschSign(lowNibble,4) +1);
            checksum = BoschSign(checksum, 4);

            return checksum;
        }

        private short BoschSign(short item, int bits)
        {
            short result = item;
            if(item >> bits-1 == 1)
            {
                int minus = 0;
                for(var i = 0; i < 16 - bits; i++)
                {
                    minus+= 1 <<(16-1-i);
                }
                result = (short)((int)item | minus) ;
            }
            
            return result;
        }

        private List<short> GetShiftedIQA(long concat)
        {
            var result = new List<short>();
            //Get  5,7,5,7,5,4,2,5 
            /*
                00000000 00000000 00000000 11111000 00000000 00000000 00000000 00000000   0xF800000000, 35
                00000000 00000000 00000000 00000111 11110000 00000000 00000000 00000000   0x7F0000000, 28
                00000000 00000000 00000000 00000000 00001111 10000000 00000000 00000000   0xF800000, 23
                00000000 00000000 00000000 00000000 00000000 01111111 00000000 00000000   0x7F0000, 16
                00000000 00000000 00000000 00000000 00000000 00000000 11111000 00000000   0xF800, 11
                00000000 00000000 00000000 00000000 00000000 00000000 00000111 10000000   0x780, 7
                00000000 00000000 00000000 00000000 00000000 00000000 00000000 01100000   0x60, 5
                00000000 00000000 00000000 00000000 00000000 00000000 00000000 00011111   0x1F, 0
            */
            result.Add(BoschSign(Convert.ToSByte((concat & 0xF800000000) >> 35),5)); //Classification
            result.Add(BoschSign(Convert.ToSByte((concat & 0x7F0000000) >> 28),7)); //EM
            result.Add(BoschSign(Convert.ToSByte((concat & 0xF800000) >> 23), 5)); //LI
            result.Add(BoschSign(Convert.ToSByte((concat & 0x7F0000) >> 16), 7)); //FL
            result.Add(BoschSign(Convert.ToSByte((concat & 0xF800) >> 11), 5));//PI
            result.Add(BoschSign(Convert.ToSByte((concat & 0x780) >> 7), 4));//Checksum
            result.Add(Convert.ToSByte((concat & 0x60) >> 5)); //QS (No sign correction)
            result.Add(BoschSign(Convert.ToSByte((concat & 0x1F)), 5)); //IVA

            return result;
        }

        private long Concatenate(List<short> shorts)
        {
            long result = 0;
            int i = 1;
            foreach(var part in shorts)
            {
                result += part;
                if(i<shorts.Count)
                    result = result << 5;
                i++;
            }
            return result;
        }

        private List<short> GetShorts(string injector)
        {
            var result = new List<short>();
            foreach(var ch in injector)
            {
                result.Add(convertionTable[ch]);
            }
            return result;
        }

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
                    case "Injector1":
                        if (!String.IsNullOrEmpty(Injector1))
                        {
                            errorMessage = GetErrorMessage(Injector1);
                        }
                        break;
                    case "Injector2":
                        if (!String.IsNullOrEmpty(Injector2) && !IsValid(Injector2))
                        {
                            errorMessage = GetErrorMessage(Injector2);
                        }
                        break;
                    case "Injector3":
                        if (!String.IsNullOrEmpty(Injector3) && !IsValid(Injector3))
                        {
                            errorMessage = GetErrorMessage(Injector3);
                        }
                        break;
                    case "Injector4":
                        if (!String.IsNullOrEmpty(Injector4) && !IsValid(Injector4))
                        {
                            errorMessage = GetErrorMessage(Injector4);
                        }
                        break;
                    case "Injector5":
                        if (!String.IsNullOrEmpty(Injector5) && !IsValid(Injector5))
                        {
                            errorMessage = GetErrorMessage(Injector5);
                        }
                        break;

                }
                return errorMessage;
            }
        }

        private string GetErrorMessage(string inputInjector)
        {
            if (inputInjector.Length != 8)
                return "Invalid Length";
            if (!Regex.IsMatch(inputInjector, "^"+VALID_INJ_CHARS+"{8}$"))
                return "Invalid characters";
            if (!IsValid(inputInjector))
                return "Checksum error";
            return String.Empty;
        }

        #endregion

        public override object Clone()
        {
            InjectorDataMatrix clone = new InjectorDataMatrix();
            clone.Injector1 = this.Injector1;
            clone.Injector2 = this.Injector2;
            clone.Injector3 = this.Injector3;
            clone.Injector4 = this.Injector4;
            clone.Injector5 = this.Injector5;
            return clone;
        }
    }
}
