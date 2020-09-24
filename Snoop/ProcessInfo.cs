namespace Snoop
{
    using System;
    using System.Diagnostics;
    using System.IO.MemoryMappedFiles;
    using System.Windows.Input;
    using Snoop.Data;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;
    using Snoop.Infrastructure;
    using Snoop.Properties;
    using NativeMethods = Snoop.Infrastructure.NativeMethods;

    public class ProcessInfo
    {
        private bool? isOwningProcess64Bit;
        private bool? isOwningProcessElevated;
        bool attachLocal;

        public ProcessInfo(int processId)
            : this(Process.GetProcessById(processId))
        {
        }

        public ProcessInfo(Process process) : this(process, false) { }

        public ProcessInfo(Process process, bool attachLocal)
        {
            this.Process = process;
            this.attachLocal = attachLocal;
        }

        public Process Process { get; set; }

        // This property has to work without exceptions.
        // This bit of information is mainly used by the InjectLauncherManager, which handles wrong info from this property later.
        public bool IsProcess64Bit => this.isOwningProcess64Bit ??= NativeMethods.IsProcess64BitWithoutException(this.Process);

        public bool IsProcessElevated => this.isOwningProcessElevated ??= NativeMethods.IsProcessElevated(this.Process);

       
        public AttachResult Snoop(IntPtr targetHwnd)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try {
                Start(targetHwnd, SnoopStartTarget.SnoopUI);
            }
            catch (Exception e)
            {
                return new AttachResult(e);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }

            return new AttachResult();
        }

        public void Start(IntPtr targetHwnd, SnoopStartTarget target) {
            var ext = ExtensionLocator.Select(targetHwnd);
            var hwndStr = targetHwnd.ToInt64().ToString();
            var set = new TransientSettingsData() { StartTarget = target, PathToSnoop = typeof(SnoopManager).Assembly.Location, TargetWindowHandle = targetHwnd.ToInt64() };
            InjectorLauncherManager.Launch(IsProcess64Bit, this.Process.Id, Program.Debug, ext.ExtensionPath, "Snoop.DataAccess.ExtensionExecutor", "Start", set.WriteToFile());
        }

        public AttachResult Magnify(IntPtr targetHwnd)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                Start(targetHwnd, SnoopStartTarget.Zoomer);
            }
            catch (Exception e)
            {
                return new AttachResult(e);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }

            return new AttachResult();
        }

        private static TransientSettingsData CreateTransientSettingsData(SnoopStartTarget startTarget, IntPtr targetWindowHandle)
        {
            var settings = Settings.Default;

            return new TransientSettingsData
            {
                StartTarget = startTarget,
                TargetWindowHandle = targetWindowHandle.ToInt64(),
                PathToSnoop =  typeof(SnoopManager).Assembly.Location,
                MultipleAppDomainMode = settings.MultipleAppDomainMode,
                MultipleDispatcherMode = settings.MultipleDispatcherMode,
                SetWindowOwner = settings.SetOwnerWindow
            };
        }
    }
}