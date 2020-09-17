namespace Snoop.DataAccess.Interfaces {
    using System.Drawing;

    public interface ISO_UIElement : ISO_Visual {
        (double Width, double Height) RenderSize { get; set; }
    }
}