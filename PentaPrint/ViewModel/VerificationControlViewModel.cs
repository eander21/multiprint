using PentaPrint.Commands;
using PentaPrint.Mediator;
using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PentaPrint.ViewModel
{
    class VerificationControlViewModel
    {
        private VerificationMediator verificationMediator = VerificationMediator.Instance;


        public VerificationControlViewModel()
        {
            SetupVerficators();
        }

        private static void SetupVerficators()
        {

        }

        private bool VerifyPrintable(string key)
        {

            return false;
        }

        public string Key { get; set; }
        public string Value
        {
            get
            {
                try
                {
                    return verificationMediator.Verifiables[Key];
                } catch
                {
                    verificationMediator.Verifiables.Add(Key, "");
                    return verificationMediator.Verifiables[Key];
                }
            }
            set
            {
                verificationMediator.Verifiables[Key] = value;
            }
        }
    }
}
