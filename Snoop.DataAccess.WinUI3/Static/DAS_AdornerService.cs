namespace Snoop.DataAccess.WinUI3 {
    using System.Numerics;
    using Windows.UI;
    using Microsoft.UI;
    using Microsoft.UI.Composition;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Hosting;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;
    #pragma warning disable CS8305

    public class DAS_AdornerService : DataAccess, IDAS_AdornerService{
        ISnoopObject highlightedElement;
        readonly Window wnd;
        ShapeVisual shape;
        CompositionRectangleGeometry rectGeo;
        FrameworkElement target;

        public DAS_AdornerService() {
            wnd = WindowLocator.GetWindow();
            this.wnd.OnUI(x => {
                Compositor compositor = x.Compositor;
                shape = compositor.CreateShapeVisual();
                var rect = compositor.CreateSpriteShape();
                rect.StrokeBrush = compositor.CreateColorBrush(Colors.Red);
                rect.StrokeThickness = 5;
                rectGeo = compositor.CreateRectangleGeometry();
                rect.Geometry = rectGeo;
                shape.Shapes.Add(rect);
            });
        }

        public ISnoopObject HighlightedElement {
            get { return this.highlightedElement; }
            set {
                if(Equals(this.highlightedElement, value))
                    return;
                var oldValue = highlightedElement; 
                this.highlightedElement = value;
                OnHighlightedElementChanged(oldValue, value);
            }
        }

        void OnHighlightedElementChanged(ISnoopObject oldValue, ISnoopObject newValue) {
            (oldValue?.Source as FrameworkElement).OnUI(x => {
                if(x==null)
                    return;
                ElementCompositionPreview.SetElementChildVisual(x, null);
                x.SizeChanged -= OnHighlightedElementSizeChanged;
                this.target = null;
            });
            (newValue?.Source as FrameworkElement).OnUI(x => {
                if(x==null)
                    return;
                ElementCompositionPreview.SetElementChildVisual(x, shape);
                x.SizeChanged += OnHighlightedElementSizeChanged;
                this.target = x;
                HandleResize();
            });
        }

        void HandleResize() {
            if(this.target==null)
                return;
            this.shape.Size = this.target.ActualSize;
            this.rectGeo.Size = this.shape.Size;
        }

        void OnHighlightedElementSizeChanged(object sender, SizeChangedEventArgs e) {
            this.HandleResize();
        }
    }
}