namespace Snoop.DataAccess.Wpf {
    using System;
    using System.Windows;
    using Snoop.DataAccess.Interfaces;

    public class SO_DependencyObject : SnoopObjectBase, ISO_DependencyObject {
        public override Type DataAccessType {
            get { return typeof(ISO_DependencyObject); }
        }
        public SO_DependencyObject(DependencyObject source) : base(source) { }
    }
}