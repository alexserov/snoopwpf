namespace Snoop
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;
    using NativeMethods = Snoop.Infrastructure.NativeMethods;

    public class WindowInfo
    {
        private static readonly Dictionary<IntPtr, bool> windowHandleToValidityMap = new Dictionary<IntPtr, bool>();
        
        private ProcessInfo owningProcessInfo;
        private static readonly int snoopProcessId = Process.GetCurrentProcess().Id;

        public WindowInfo(IntPtr hwnd)
        {
            this.HWnd = hwnd;
        }

        public WindowInfo(IntPtr hwnd, Process owningProcess)
        : this(hwnd)
        {
            this.owningProcessInfo = new ProcessInfo(owningProcess);
        }

        public static void ClearCachedWindowHandleInfo()
        {
            windowHandleToValidityMap.Clear();
        }

        

        public bool IsValidProcess
        {
            get
            {
                return ExtensionLocator.Select(HWnd)!=null!;
            }
        }

        public ProcessInfo OwningProcessInfo => this.owningProcessInfo ??= new ProcessInfo(NativeMethods.GetWindowThreadProcess(this.HWnd));

        public IntPtr HWnd { get; }

        public string Description => $"{this.WindowTitle} - {this.OwningProcessInfo?.Process?.ProcessName ?? string.Empty} [{this.OwningProcessInfo?.Process?.Id}]";

        #region UI Binding sources

        public string WindowTitle
        {
            get
            {
                var processInfo = this.OwningProcessInfo;
                var windowTitle = NativeMethods.GetText(this.HWnd);

                if (string.IsNullOrEmpty(windowTitle))
                {
                    try
                    {
                        windowTitle = processInfo.Process.MainWindowTitle;
                    }
                    catch (InvalidOperationException)
                    {
                        // The process closed while we were trying to evaluate it
                        return string.Empty;
                    }
                }

                return windowTitle;
            }
        }

        public string ProcessName => this.OwningProcessInfo?.Process?.ProcessName;

        public int ProcessId => this.OwningProcessInfo?.Process?.Id ?? -1;

        #endregion

        public string ClassName => NativeMethods.GetClassName(this.HWnd);

        public string TraceInfo => $"{this.Description} [{this.HWnd.ToInt64():X8}] {this.ClassName}";

        public override string ToString()
        {
            return this.Description;
        }
    }
}