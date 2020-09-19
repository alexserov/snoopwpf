namespace Snoop.DataAccess.Wpf {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using Snoop.DataAccess.Interfaces;

    public class SO_ResourceDictionary : SnoopObjectBase, ISO_ResourceDictionary {
        public override Type DataAccessType {
            get { return typeof(ISO_ResourceDictionary); }
        }
        readonly ResourceDictionary source;
        public SO_ResourceDictionary(ResourceDictionary source) : base(source) { this.source = source; }
        public ICollection GetKeys() { return this.source.Keys; }
        public ICollection GetValues() { return this.source.Values; }

        public object GetValue(object key) { return this.source[key]; }
        public ICollection<ISO_ResourceDictionary> GetMergedDictionaries() { throw new NotImplementedException();}
    }
}