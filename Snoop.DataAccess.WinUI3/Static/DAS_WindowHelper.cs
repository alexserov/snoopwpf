namespace Snoop.DataAccess.WinUI3
{
    using System;
    using Windows.UI.Core;
    using Microsoft.UI.Xaml;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class DAS_WindowHelper : DataAccess, IDAS_WindowHelper {
        public ISO_Window GetVisibleWindow(long hwnd) {
            var result = GetVisibleWindow(new IntPtr(hwnd));
            return result == null ? null : new SO_Window(result);
        }

        public static Window GetVisibleWindow(IntPtr ptr) {
            return WindowLocator.GetWindow(); 
            
        }
    }

}