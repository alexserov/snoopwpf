namespace Snoop.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;
    using JetBrains.Annotations;
    using Snoop.Data;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;
    using Snoop.Infrastructure.Helpers;
    using Snoop.Windows;
    
    public class SnoopManager
    {
        static Func<IExtension, SnoopMainBaseWindow> GetInstanceCreator(SnoopStartTarget startTarget)
        {
            switch (startTarget)
            {
                case SnoopStartTarget.SnoopUI:
                    return x => new SnoopUI(x);

                case SnoopStartTarget.Zoomer:
                    return x => new Zoomer(x);

                default:
                    throw new ArgumentOutOfRangeException(nameof(startTarget), startTarget, null);
            }
        }
        

        public static void CreateSnoopWindow(IExtension extension, TransientSettingsData settingsData, SnoopStartTarget target) {
            var snoopWindow = GetInstanceCreator(target)(extension);

            var targetWindowOnSameDispatcher = WindowHelper.GetVisibleWindow(extension, settingsData.TargetWindowHandle);

            snoopWindow.Title = TryGetWindowOrMainWindowTitle(targetWindowOnSameDispatcher);

            if (string.IsNullOrEmpty(snoopWindow.Title))
            {
                snoopWindow.Title = "Snoop";
            }
            else
            {
                snoopWindow.Title += " - Snoop";
            }

            snoopWindow.Inspect();

            if (targetWindowOnSameDispatcher != null)
            {
                snoopWindow.Target = targetWindowOnSameDispatcher;
            }

            Dispatcher.Run();
        }

        static string TryGetWindowOrMainWindowTitle(ISO_Window targetWindow) {
            return targetWindow != null ? targetWindow.GetTitle() : string.Empty;
        }
    }
}