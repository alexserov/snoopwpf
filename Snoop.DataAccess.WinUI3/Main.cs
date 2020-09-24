namespace Snoop.DataAccess {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Loader;
    using System.Text.RegularExpressions;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;
    using Snoop.DataAccess.WinUI3;
    public class Extension : ExtensionBase<Extension> {
        public override void RegisterInterfaces() {
            this.Set<IDAS_CurrentApplication>(new DAS_CurrentApplication());
            this.Set<IDAS_InputManager>(new DAS_InputManager());
            this.Set<IDAS_Mouse>(new DAS_Mouse());
            this.Set<IDAS_RootProvider>(new DAS_RootProvider());
            this.Set<IDAS_TreeHelper>(new DAS_TreeHelper());
            this.Set<IDAS_WindowHelper>(new DAS_WindowHelper());
        }

        protected override void StartSnoopOverride(string handle) {
            InjectorLauncherManager.Launch(IntPtr.Size == 8, Process.GetCurrentProcess().Id, false, typeof(ExtensionLocator).Assembly.Location, typeof(ExtensionLocator).FullName, nameof(ExtensionLocator.StartSnoop), handle, "new_net40");
        }
        public Extension() : base("WinUI3") { }
    }

    public class ExtensionExecutor {
        public ExtensionExecutor() {
            
        }
        public static int Start(string param) {
            AssemblyLoadContext.Default.Resolving+=DefaultOnResolving;
            AssemblyLoadContext.Default.ResolvingUnmanagedDll+=DefaultOnResolvingUnmanagedDll;
            var generic = Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(typeof(ExtensionExecutor).Assembly.Location), "Snoop.DataAccess.Generic.dll"));
            return (int)typeof(ExtensionExecutor).Assembly.GetType("Snoop.DataAccess.Extension").GetMethod("StartCore", BindingFlags.Static|BindingFlags.Public|BindingFlags.FlattenHierarchy).Invoke(null, new[] { param });
        }

        static IntPtr DefaultOnResolvingUnmanagedDll(Assembly arg1, string arg2) {
            return  IntPtr.Zero;
        }

        static Assembly DefaultOnResolving(AssemblyLoadContext arg1, AssemblyName arg2) {
            var currentDir = Path.GetDirectoryName(typeof(ExtensionExecutor).Assembly.Location);
            var asmName = arg2.Name;
            foreach(var segment in new[] { "", @"..\" })
            {
                var path = Path.Combine(currentDir, segment, $"{asmName}.dll");
                path = Path.GetFullPath(path);
                if (File.Exists(path))
                    return Assembly.LoadFrom(path);
            }            
            return null;
        }
    }
}