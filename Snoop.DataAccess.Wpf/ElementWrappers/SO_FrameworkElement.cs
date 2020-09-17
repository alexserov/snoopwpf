namespace Snoop.DataAccess.Wpf {
    using System.Windows;
    using Snoop.DataAccess.Interfaces;

    public class SO_FrameworkElement : SO_Visual, ISO_FrameworkElement{
        readonly FrameworkElement source;
        public SO_FrameworkElement(FrameworkElement source) : base(source) { this.source = source; }
        public ISO_DependencyObject TemplatedParent {
            get { return (ISO_DependencyObject)SnoopObjectBase.Create(this.source.TemplatedParent); }
        }
    }
}