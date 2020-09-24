namespace Snoop.DataAccess
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Loader;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;
    using Snoop.DataAccess.WinUI3;
    using Snoop.Infrastructure;

    public class Extension : ExtensionBase<Extension>
    {
        public override void RegisterInterfaces()
        {
            this.Set<IDAS_CurrentApplication>(new DAS_CurrentApplication());
            this.Set<IDAS_InputManager>(new DAS_InputManager());
            this.Set<IDAS_Mouse>(new DAS_Mouse());
            this.Set<IDAS_RootProvider>(new DAS_RootProvider());
            this.Set<IDAS_TreeHelper>(new DAS_TreeHelper());
            this.Set<IDAS_WindowHelper>(new DAS_WindowHelper());
        }

        public override void StartSnoop() { SnoopManager.CreateSnoopWindow(this, this.data, this.data.StartTarget); }
        public Extension() : base("WinUI3") { }
    }

    public class ExtensionExecutor
    {
        public static int Start(string param)
        {
            var executingLocation = AppDomain.CurrentDomain.BaseDirectory;
            var thisLocation = Path.GetDirectoryName(typeof(ExtensionExecutor).Assembly.Location);
            foreach (var file in Directory.GetFiles(thisLocation, "*.dll", SearchOption.AllDirectories))
            {
                try {
                    var targetPath = Path.Combine(executingLocation, Path.GetFileName(file));
                    if (!File.Exists(targetPath) || file.StartsWith("Snoop"))
                        File.Copy(file, targetPath, true);
                }
                catch
                {
                    //whoops
                }
            }                

            AssemblyLoadContext.Default.Resolving += DefaultOnResolving;
            AssemblyLoadContext.Default.ResolvingUnmanagedDll += DefaultOnResolvingUnmanagedDll;
            var generic = Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(typeof(ExtensionExecutor).Assembly.Location), "Snoop.DataAccess.Generic.dll"));
            return (int)typeof(ExtensionExecutor).Assembly.GetType("Snoop.DataAccess.Extension").GetMethod("StartCore", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).Invoke(null, new[] { param });
        }

        static IntPtr DefaultOnResolvingUnmanagedDll(Assembly arg1, string arg2)
        {
            return IntPtr.Zero;
        }

        static Assembly DefaultOnResolving(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            //var currentDir = Path.GetDirectoryName(typeof(ExtensionExecutor).Assembly.Location);
            var asmName = $"{arg2.Name}.dll";
            //foreach (var segment in new[] { "" })
            //{
            //    var path = Path.Combine(currentDir, segment, asmName);
            //    path = Path.GetFullPath(path);
            //    if (File.Exists(path))
            //        return Assembly.LoadFrom(path);
            //}
            //var fwdir = Path.Combine(Path.GetDirectoryName(typeof(ExtensionExecutor).Assembly.Location), IntPtr.Size == 8 ? "WPFx64" : "WPFx86");
            var fwdir = AppDomain.CurrentDomain.BaseDirectory;
            var file = Path.Combine(fwdir, asmName);
            if (File.Exists(file))
                return Assembly.LoadFrom(file);
            return null;
        }
    }
}