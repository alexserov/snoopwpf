namespace Snoop.Infrastructure.Helpers
{
    using System;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Threading;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public static class WindowHelper
    {
        public static ISO_Window GetVisibleWindow(Extension extension, long hwnd) {
            return extension.Get<IDAS_WindowHelper>().GetVisibleWindow(hwnd);
        }
    }
}