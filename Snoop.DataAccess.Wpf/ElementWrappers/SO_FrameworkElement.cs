namespace Snoop.DataAccess.Wpf {
    using System;
    using System.Windows;
    using Snoop.DataAccess.Interfaces;

    public class SO_FrameworkElement : SO_UIElement, ISO_FrameworkElement {
        public override Type DataAccessType {
            get { return typeof(ISO_FrameworkElement); }
        }
        readonly FrameworkElement source;
        public SO_FrameworkElement(FrameworkElement source) : base(source) { this.source = source; }

        public ISO_DependencyObject GetTemplatedParent() { return (ISO_DependencyObject)Create(this.source.OnUI(x => x.TemplatedParent)); }

        public ISO_ResourceDictionary GetResources() { return (ISO_ResourceDictionary)Create(this.source.OnUI(x => x.Resources)); }

        public double GetActualHeight() { return this.source.OnUI(x => x.ActualHeight); }

        public double GetActualWidth() { return this.source.OnUI(x => x.ActualWidth); }

    }

}