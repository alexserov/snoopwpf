namespace Snoop.DataAccess.WinUI3 {
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Windows.Devices.Input;
    using Windows.Foundation;
    using Windows.UI.Core;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Media;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;

    public class DAS_Mouse : DataAccess, IDAS_Mouse {
        Window wnd;

        public DAS_Mouse() {
            wnd = WindowLocator.GetWindow(); 
            
        }

        public ISnoopObject DirectlyOver {
            get {
                if (this.wnd == null)
                    return SnoopObjectBase.Create(null);
                return SnoopObjectBase.Create(this.wnd.OnUI<UIElement>(x => {
                    Snoop.Infrastructure.POINT cp = default;
                    if (Snoop.Infrastructure.NativeMethods.GetCursorPos(ref cp)) {
                        var children = VisualTreeHelper.FindElementsInHostCoordinates(new Point((cp.X - x.Bounds.Left), cp.Y - x.Bounds.Top), x.Content);
                        return children.FirstOrDefault() ?? x.Content;
                    }

                    return this.wnd.Content;
                }));
            }
        }
    }
}