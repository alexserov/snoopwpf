namespace Snoop.DataAccess.WinUI3 {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.UI.Xaml;
    using Snoop.DataAccess.Interfaces;

    public class SO_ResourceDictionary : SnoopObjectBase, ISO_ResourceDictionary {
        public override Type DataAccessType {
            get { return typeof(ISO_ResourceDictionary); }
        }
        readonly ResourceDictionary source;
        public SO_ResourceDictionary(ResourceDictionary source) : base(source) { this.source = source; }
        public ICollection GetKeys() { return this.source.Keys.ToList(); }
        public ICollection GetValues() { return this.source.Values.ToList(); }

        public object GetValue(object key) { return this.source[key]; }
        public ICollection<ISO_ResourceDictionary> GetMergedDictionaries() { throw new NotImplementedException();}
    }
}