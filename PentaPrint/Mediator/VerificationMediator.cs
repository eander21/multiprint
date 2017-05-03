using PentaPrint.Devices;
using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PentaPrint.Mediator
{
    class VerificationMediator
    {
        private static VerificationMediator _instance = new VerificationMediator();
        public static VerificationMediator Instance { get { return _instance; } private set { _instance = value; } }
        public Dictionary<String, String> Verifiables { get; set; }
        private static Dictionary<String, Verifiable> verificators;

        public VerificationMediator()
        {
            Verifiables = new Dictionary<String, String>();
            if (verificators == null)
            {
                verificators = new Dictionary<string, Verifiable>();
                var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace.StartsWith("PentaPrint.Model") && !t.IsAbstract && t.GetInterfaces().Contains(typeof(Verifiable)));
                foreach (var type in types)
                {
                    var instance = Activator.CreateInstance(type);
                    verificators.Add(type.Name, (Verifiable)instance);
                }
            }
        }

        public bool VerifyAll()
        {
            foreach(var veriable in Verifiables)
            {
                string error = "";
                if (!verificators[veriable.Key].Verify(veriable.Value, out error))
                {
                    MessageBox.Show("Invalid data in "+ veriable.Key+"\n"+error,"Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }
        

    }
}
