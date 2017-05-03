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
        private static int HISTORY_DEPTH = 5;
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
            _printables = new Dictionary<string, IPrint>();
            foreach(var printable in printGroup.Printables)
            {
                AddPrintable(printable.Key, (IPrint)printable.Value.Clone());
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
        public IPrint GetPrintableClone(String key)
        {
            return (IPrint)_printables[key].Clone();
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

        public void PushAllToHistory()
        {
            var printGroup = new PrintGroup();
            foreach(var print in _printables)
            {
                printGroup.Printables.Add(print.Key, (IPrint)print.Value.Clone());
            }
            printGroup.UpdateHeader();
            AddToHistory(printGroup);
        }
        public void PushToHistory(string key)
        {
            var printGroup = new PrintGroup();
            printGroup.Printables.Add(key, (IPrint)_printables[key].Clone());
            printGroup.UpdateHeader();
            AddToHistory(printGroup);
        }

        public void AddToHistory(PrintGroup printGroup)
        {
            History.Add(printGroup);
            if (History.Count > HISTORY_DEPTH)
            {
                History.RemoveAt(0);
            }
        }

        public void ResetPrintable(string key)
        {
            _printables[key].Reset();
            PrintableChanged.Invoke(key);
        }

        public void ResetAllPrintables()
        {
            foreach (var print in _printables)
            {
                print.Value.Reset();
                PrintableChanged.Invoke(print.Key);
            }
        }
        public void SubscribePrintableChanged(Action<string> ev)
        {
            PrintableChanged += ev;
        }

    }
}
