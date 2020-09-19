namespace Snoop.DataAccess.Wpf {
    using System;
    using System.Windows.Media.Media3D;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class SO_Visual3D : SO_DependencyObject, ISO_Visual3D{
        public override Type DataAccessType {
            get { return typeof(ISO_Visual3D); }
        }
        public SO_Visual3D(Visual3D source) : base(source) { }
    }
}