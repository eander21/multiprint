using PentaPrint.Devices;
using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaPrint.Mediator
{
    class PrintMediator
    {
        private static PrintMediator _instance = new PrintMediator();
        public static PrintMediator Instance { get { return _instance; } private set { _instance = value; } }
        private Dictionary<String, IPrint> _printables;

        public ObservableCollection<PrintGroup> History { get; set; }
        //private GlobalSettings globalSettings = GlobalSettings.Instance;
        public Printer Printer { get; set; }
        public event Action<string> PrintableChanged;

        public PrintMediator()
        {
            _printables = new Dictionary<string, IPrint>();
            Printer = new Printer();
            History = new ObservableCollection<PrintGroup>();

        }

        public void SetupTestPrintGroups()
        {
            PrintGroup pg = new PrintGroup();
            var mainEngine = new MainEngineBarcode();
            mainEngine.Partnumber = "12345678";
            mainEngine.Serialnumber = "12345678";
            pg.Printables.Add("MainEngine", mainEngine);
            var injectors = new InjectorDataMatrix(mainEngine);
            injectors.Injector1 = "6A1AIB5H";
            injectors.Injector2 = "6A1AIB5H";
            injectors.Injector3 = "6A1AIB5H";
            injectors.Injector4 = "6A1AIB5H";
            injectors.Injector5 = "6A1AIB5H";
            pg.Printables.Add("Injectors", injectors);
            pg.Header = mainEngine.ToString();
            History.Add(pg);
        }

        internal void SetCurrentPrintGroup(PrintGroup printGroup)
        {
            _printables = printGroup.GetPrintablesClone();
            foreach(var printable in _printables)
            {
                PrintableChanged.Invoke(printable.Key);
            }
            //throw new NotImplementedException();
        }

        public void ClearPrintables()
        {
            _printables.Clear();
        }
        public Dictionary<String, IPrint> GetAllPrintables()
        {
            return _printables;
        }
        public IPrint GetPrintable(String key)
        {
            return _printables[key];
        }
        public void AddPrintable(String key, IPrint value)
        {
            value.PropertyChanged += PropertyChanged;
            _printables.Add(key, value);
        }

        private void PropertyChanged(object obj, PropertyChangedEventArgs args)
        {
            PrintableChanged.Invoke(args.PropertyName);
        }

        public void PushToHistory()
        {
            var printGroup = new PrintGroup();
            foreach(var print in _printables)
            {
                printGroup.Printables.Add(print.Key, (IPrint)print.Value.Clone());
            }
            History.Add(printGroup);
        }

        public void ResetAllPrintables()
        {
            foreach (var print in _printables)
            {
                print.Value.Reset();
            }
        }
        public void SubscribePrintableChanged(Action<string> ev)
        {
            PrintableChanged += ev;
        }

    }
}
