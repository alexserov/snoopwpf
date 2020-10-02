namespace Snoop.DataAccess.WinUI3 {
    using System;
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
                    var coreWindow = CoreWindow.GetForCurrentThread();
                    var pointerPosition = coreWindow.PointerPosition;
                    pointerPosition = new Point(pointerPosition.X - coreWindow.Bounds.Left, pointerPosition.Y - coreWindow.Bounds.Top);
                    var children = VisualTreeHelper.FindElementsInHostCoordinates(pointerPosition, x.Content);
                    return children.FirstOrDefault() ?? x.Content;
                }));
            }
        }
    }
}