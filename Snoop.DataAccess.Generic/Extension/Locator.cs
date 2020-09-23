namespace Snoop.DataAccess.Generic {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Snoop.DataAccess.Sessions;

    public abstract class Locator {
        protected abstract Regex WindowClassNameRegex { get; }
        protected abstract IEnumerable<String> ValidModuleNames { get; }
        public abstract string ExtensionPath { get; }

        public bool GetIsValidProcess(IntPtr hwnd) {
            try {
                if (hwnd == IntPtr.Zero) {
                    return false;
                }

                var wp = NativeMethods.GetWindowThreadProcess(hwnd);
                if (wp.Id == Process.GetCurrentProcess().Id)
                    return false;
                if (WindowClassNameRegex.IsMatch(NativeMethods.GetClassName(hwnd))) {
                    return true;
                }

                foreach (var module in NativeMethods.GetModulesFromWindowHandle(hwnd).ToList()) {
                    foreach (var validModule in ValidModuleNames) {
                        if (module.szModule.StartsWith(validModule, StringComparison.OrdinalIgnoreCase))
                            return true;
                    }
                }

            } catch { }

            return false;
        }
    }
}