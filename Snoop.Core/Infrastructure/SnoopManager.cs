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

    [Serializable]
    public class SnoopCrossAppDomainInjector : MarshalByRefObject
    {
        public SnoopCrossAppDomainInjector()
        {
            SnoopModes.MultipleAppDomainMode = true;

            // We have to do this in the constructor because we might not be able to cast correctly.
            // Not being able to cast might be the case if the app domain we should run in uses shadow copies for it's assemblies.
            this.RunInCurrentAppDomain(Environment.GetEnvironmentVariable("Snoop.SettingsFile"));
        }

        private void RunInCurrentAppDomain(string settingsFile)
        {
            var settingsData = TransientSettingsData.LoadCurrent(settingsFile);
            new SnoopManager().RunInCurrentAppDomain(settingsData);
        }
    }

    public class SnoopManager
    {
        static Func<Extension, SnoopMainBaseWindow> GetInstanceCreator(SnoopStartTarget startTarget)
        {
            switch (startTarget)
            {
                case SnoopStartTarget.SnoopUI:
                    return (Extension x) => new SnoopUI(x);

                case SnoopStartTarget.Zoomer:
                    return (Extension x) => new Zoomer(x);

                default:
                    throw new ArgumentOutOfRangeException(nameof(startTarget), startTarget, null);
            }
        }

        public static SnoopMainBaseWindow CreateSnoopWindow(Extension extension, TransientSettingsData settingsData, SnoopStartTarget target) {
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

            return snoopWindow;
        }

        static string TryGetWindowOrMainWindowTitle(IWindowInstance targetWindow) {
            return targetWindow != null ? targetWindow.Title : string.Empty;
        }
        

        private static Visual GetRootVisual(Dispatcher dispatcher)
        {
            return PresentationSource.CurrentSources
                .OfType<PresentationSource>()
                .FirstOrDefault(x => x.Dispatcher == dispatcher)
                ?.RootVisual;
        }

        private static void DispatchOut(object o)
        {
            var dispatchOutParameters = (DispatchOutParameters)o;

            foreach (var dispatcher in dispatchOutParameters.Dispatchers)
            {
                // launch a snoop ui on each dispatcher
                dispatcher.Invoke(
                    (Action)(() =>
                    {
                        var snoopInstance = dispatchOutParameters.InstanceCreator(dispatchOutParameters.SettingsData, dispatcher);

                        if (snoopInstance.Target is null)
                        {
                            snoopInstance.Target = GetRootVisual(dispatcher);
                        }
                    }));
            }
        }

        private class DispatchOutParameters
        {
            public DispatchOutParameters(TransientSettingsData settingsData, Func<TransientSettingsData, Dispatcher, SnoopMainBaseWindow> instanceCreator, List<Dispatcher> dispatchers)
            {
                this.SettingsData = settingsData;
                this.InstanceCreator = instanceCreator;
                this.Dispatchers = dispatchers;
            }

            public TransientSettingsData SettingsData { get; }

            public Func<TransientSettingsData, Dispatcher, SnoopMainBaseWindow> InstanceCreator { get; }

            public List<Dispatcher> Dispatchers { get; }
        }
    }
}