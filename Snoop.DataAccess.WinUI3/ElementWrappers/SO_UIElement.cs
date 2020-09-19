namespace Snoop.DataAccess.WinUI3 {
    using System;
    using System.Linq;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Media;
    using Snoop.DataAccess.Interfaces;

    public class SO_UIElement : SO_DependencyObject, ISO_UIElement {
        public override Type DataAccessType {
            get { return typeof(ISO_UIElement); }
        }

        readonly UIElement source;
        public SO_UIElement(UIElement source) : base(source) { this.source = source; }

        public bool IsDescendantOf(ISO_Visual rootVisual) {
            var visual = rootVisual.UW<UIElement>();
            if (visual == null)
                return false;
            return this.source.OnUI(x => DAS_TreeHelper.GetParents(x).Contains(visual));
        }

        public ISO_UISurface GetSurface() { return new VisualUISurface(this.source); }

        public (double Width, double Height) GetRenderSize() { return this.source.OnUI(x => (x.RenderSize.Width, x.RenderSize.Height)); }

    }
}