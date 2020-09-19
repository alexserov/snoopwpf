namespace Snoop.DataAccess.WinUI3 {
    using System;
    using Microsoft.UI.Xaml;
    using Snoop.DataAccess.Interfaces;

    public class SO_DependencyObject : SnoopObjectBase, ISO_DependencyObject {
        public override Type DataAccessType {
            get { return typeof(ISO_DependencyObject); }
        }
        public SO_DependencyObject(DependencyObject source) : base(source) { }
    }
}