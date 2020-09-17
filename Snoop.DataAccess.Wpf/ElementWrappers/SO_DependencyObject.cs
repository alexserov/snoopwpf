namespace Snoop.DataAccess.Wpf {
    using System.Windows;
    using Snoop.DataAccess.Interfaces;

    public class SO_DependencyObject : SnoopObjectBase, ISO_DependencyObject {
        public SO_DependencyObject(DependencyObject source) : base(source) { }
    }
}