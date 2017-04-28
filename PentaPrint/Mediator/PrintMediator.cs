﻿using PentaPrint.Devices;
using PentaPrint.Model;
using System;
using System.Collections.Generic;
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
        //private GlobalSettings globalSettings = GlobalSettings.Instance;
        public Printer Printer { get; set; }
        public event Action<string> PrintableChanged;

        public PrintMediator()
        {
            _printables = new Dictionary<string, IPrint>();
            Printer = new Printer();
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
