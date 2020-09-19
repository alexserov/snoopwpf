namespace Snoop.DataAccess.Interfaces {
    public interface ISO_Visual : ISO_DependencyObject {
        bool IsDescendantOf(ISO_Visual rootVisual);
        ISO_UISurface GetSurface();
    }
}