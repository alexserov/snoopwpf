namespace Snoop.DataAccess.Wpf
{
    using System;
    using System.Windows;
    using System.Windows.Interop;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class WindowHelperStatic : DataAccessBase, IDAS_WindowHelperStatic {
        public ISO_Window GetVisibleWindow(long hwnd) {
            var result = GetVisibleWindow(new IntPtr(hwnd));
            return result == null ? null : new SO_Window(result);
        }

        public static Window GetVisibleWindow(IntPtr ptr) {
            if (ptr == IntPtr.Zero
            ) {
                return null;
            }

            var hwndSource = HwndSource.FromHwnd(ptr);

            if (hwndSource != null
                && (hwndSource.Dispatcher is null || hwndSource.CheckAccess())
                && hwndSource.RootVisual is Window window
                && window.Visibility == Visibility.Visible) {
                return window;
            }

            return null;
        }
    }

}