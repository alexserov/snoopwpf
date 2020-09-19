namespace Snoop.DataAccess.Wpf {
    using System;
    using System.Windows.Media;
    using Snoop.DataAccess.Interfaces;

    public class SO_Brush : SO_DependencyObject, ISO_Brush{
        public override Type DataAccessType {
            get { return typeof(ISO_Brush); }
        }

        public SO_Brush(Brush source) : base(source) { }
    }
}