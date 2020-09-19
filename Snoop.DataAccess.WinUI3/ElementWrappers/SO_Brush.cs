namespace Snoop.DataAccess.WinUI3 {
    using System;
    using Microsoft.UI.Xaml.Media;
    using Snoop.DataAccess.Interfaces;

    public class SO_Brush : SO_DependencyObject, ISO_Brush{
        public override Type DataAccessType {
            get { return typeof(ISO_Brush); }
        }

        public SO_Brush(Brush source) : base(source) { }
    }
}