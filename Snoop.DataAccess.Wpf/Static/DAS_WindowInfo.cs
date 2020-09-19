namespace Snoop.DataAccess.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class DAS_WindowInfo : IDAS_WindowInfo
    {
        // we have to match "HwndWrapper[{0};{1};{2}]" which is used at https://referencesource.microsoft.com/#WindowsBase/Shared/MS/Win32/HwndWrapper.cs,2a8e13c293bb3f8c
        private static readonly Regex windowClassNameRegex = new Regex(@"^HwndWrapper\[.*;.*;.*\]$", RegexOptions.Compiled);

        public DAS_WindowInfo()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; }

        public bool GetIsValidProcess(IntPtr hwnd)
        {
            var isValid = false;
            try
            {
                if (hwnd == IntPtr.Zero)
                {
                    return false;
                }

                // else determine the process validity and cache it.
                if (Process.GetCurrentProcess().Id == Server.Current.OwnerPId)
                {
                    isValid = false;

                    // the above line stops the user from snooping on snoop, since we assume that ... that isn't their goal.
                    // to get around this, the user can bring up two snoops and use the second snoop ... to snoop the first snoop.
                    // well, that let's you snoop the app chooser. in order to snoop the main snoop ui, you have to bring up three snoops.
                    // in this case, bring up two snoops, as before, and then bring up the third snoop, using it to snoop the first snoop.
                    // since the second snoop inserted itself into the first snoop's process, you can now spy the main snoop ui from the
                    // second snoop (bring up another main snoop ui to do so). pretty tricky, huh! and useful!
                }
                else
                {
                    // WPF-Windows have a defined class name
                    if (windowClassNameRegex.IsMatch(NativeMethods.GetClassName(hwnd)))
                    {
                        isValid = true;
                    }

                    if (isValid == false)
                    {
                        // a process is valid to snoop if it contains a dependency on PresentationFramework, PresentationCore, or milcore (wpfgfx).
                        // this includes the files:
                        // PresentationFramework.dll, PresentationFramework.ni.dll
                        // PresentationCore.dll, PresentationCore.ni.dll
                        // wpfgfx_v0300.dll (WPF 3.0/3.5 Full)
                        // wpfgrx_v0400.dll (WPF 4.0 Full)
                        // wpfgfx_cor3.dll (WPF 3.0/3.1 Core)

                        // note: sometimes PresentationFramework.dll doesn't show up in the list of modules.
                        // so, it makes sense to also check for the unmanaged milcore component (wpfgfx_vxxxx.dll).
                        // see for more info: http://snoopwpf.codeplex.com/Thread/View.aspx?ThreadId=236335

                        // sometimes the module names aren't always the same case. compare case insensitive.
                        // see for more info: http://snoopwpf.codeplex.com/workitem/6090
                        foreach (var module in NativeMethods.GetModulesFromWindowHandle(hwnd).ToList())
                        {
                            if (module.szModule.StartsWith("PresentationFramework", StringComparison.OrdinalIgnoreCase)
                                || module.szModule.StartsWith("PresentationCore", StringComparison.OrdinalIgnoreCase)
                                || module.szModule.StartsWith("wpfgfx", StringComparison.OrdinalIgnoreCase))
                            {
                                isValid = true;
                                break;
                            }
                        }
                    }
                }

            }
            catch
            {
                // ignored
            }

            return isValid;
        }
    }
}