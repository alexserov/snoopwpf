// (c) Copyright Cory Plotts.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace Snoop.Infrastructure
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Interop;
    using Snoop.Data;
    using Snoop.Infrastructure.Helpers;
    using Rectangle = System.Drawing.Rectangle;

    public static class SnoopWindowUtils
    {

        public static void LoadWindowPlacement(Window window, WINDOWPLACEMENT? windowPlacement)
        {
            if (windowPlacement.HasValue == false
                || IsVisibleOnAnyScreen(windowPlacement.Value.NormalPosition) == false)
            {
                return;
            }

            try
            {
                // load the window placement details from the user settings.
                var wp = windowPlacement.Value;
                wp.Length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
                wp.Flags = 0;
                wp.ShowCmd = wp.ShowCmd == NativeMethods.SW_SHOWMINIMIZED ? NativeMethods.SW_SHOWNORMAL : wp.ShowCmd;
                var hwnd = new WindowInteropHelper(window).Handle;
                NativeMethods.SetWindowPlacement(hwnd, ref wp);
            }
            catch
            {
            }
        }

        public static void SaveWindowPlacement(Window window, Action<WINDOWPLACEMENT> saveAction)
        {
            WINDOWPLACEMENT windowPlacement;
            var hwnd = new WindowInteropHelper(window).Handle;
            NativeMethods.GetWindowPlacement(hwnd, out windowPlacement);

            saveAction(windowPlacement);
        }

        private static bool IsVisibleOnAnyScreen(RECT rect)
        {
            var rectangle = new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);

            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                if (screen.WorkingArea.IntersectsWith(rectangle))
                {
                    return true;
                }
            }

            return false;
        }
    }
}