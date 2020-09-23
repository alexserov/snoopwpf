namespace Snoop.DataAccess.Wpf
{
    using System;
    using System.Windows;
    using System.Windows.Interop;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class DAS_WindowHelper : DataAccess, IDAS_WindowHelper {
        public ISO_Window GetVisibleWindow(long hwnd) {
            var result = GetVisibleWindow(new IntPtr(hwnd));
            return result == null ? null : new SO_Window(result);
        }

        public static Window GetVisibleWindow(IntPtr ptr) {
            if (ptr == IntPtr.Zero
            ) {
                return null;
            }

            return HwndSource.FromHwnd(ptr).OnUI(x => {
                if (x != null
                    && (x.Dispatcher is null || x.CheckAccess())
                    && x.RootVisual is Window window
                    && window.Visibility == Visibility.Visible) {
                    return window;
                }

                return null;    
            });
        }
    }

}