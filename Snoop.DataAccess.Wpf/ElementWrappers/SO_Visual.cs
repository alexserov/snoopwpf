namespace Snoop.DataAccess.Wpf {
    using System;
    using System.Windows;
    using System.Windows.Media;
    using Snoop.DataAccess.Interfaces;

    public class SO_Visual : SO_DependencyObject, ISO_Visual {
        public override Type DataAccessType {
            get { return typeof(ISO_Visual); }
        }
        readonly Visual source;
        public SO_Visual(Visual source) : base(source) { this.source = source; }

        public bool IsDescendantOf(ISO_Visual rootVisual) {
            var visual = rootVisual.UW<Visual>();
            if (visual == null)
                return false;
            return this.source.OnUI(x=>x.IsDescendantOf(visual));
        }

        public ISO_UISurface GetSurface(){ return new VisualUISurface(this.source); }
    }
}