namespace Snoop.DataAccess.Wpf {
    using System.Windows;
    using System.Windows.Media;
    using Snoop.DataAccess.Interfaces;

    public class SO_Visual : SO_DependencyObject, ISO_Visual {
        public SO_Visual(Visual source) : base(source) { }
    }
}