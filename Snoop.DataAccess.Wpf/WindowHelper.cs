namespace Snoop.DataAccess.Wpf
{
    using System;
    using System.Windows;
    using System.Windows.Interop;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class WindowHelper : DataAccessBase, IWindowHelper {
        public IWindowInstance GetVisibleWindow(long hwnd) {
            var ptr = new IntPtr(hwnd);
            if (ptr == IntPtr.Zero
            ) {
                return null;
            }

            var hwndSource = HwndSource.FromHwnd(ptr);

            if (hwndSource != null
                && (hwndSource.Dispatcher is null || hwndSource.CheckAccess())
                && hwndSource.RootVisual is Window window
                && window.Visibility == Visibility.Visible) {
                return new WindowInstance(window);
            }

            return null;
        }
    }

}