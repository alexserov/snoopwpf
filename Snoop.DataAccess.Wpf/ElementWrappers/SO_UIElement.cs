namespace Snoop.DataAccess.Wpf {
    using System;
    using System.Windows;
    using System.Windows.Media;
    using Snoop.DataAccess.Interfaces;

    public class SO_UIElement : SO_Visual, ISO_UIElement {
        public override Type DataAccessType {
            get { return typeof(ISO_UIElement); }
        }
        readonly UIElement source;
        public SO_UIElement(UIElement source) : base(source) { this.source = source; }

        public (double Width, double Height) GetRenderSize() { return this.source.OnUI(x => (x.RenderSize.Width, x.RenderSize.Height)); }

    }
}