// ReSharper disable HeapView.PossibleBoxingAllocation
namespace Snoop.DataAccess.Sessions {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Threading;
    using Snoop.Data;
    using Snoop.DataAccess.Generic;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;

    public class ExtensionLocator {
        static List<Locator> locators;
        internal static IExtension Instance { get; set; }
        static ExtensionLocator() { locators = new List<Locator>(); }

        public static void StartFrom(string name) {
            var asm = Assembly.LoadFrom(name);
            if (typeof(IDataAccess).Assembly == asm)
                return;
            var tExt = asm.GetTypes().Where(x => typeof(Locator).IsAssignableFrom(x)).FirstOrDefault();
            if (tExt == null)
                return;
            var ext = (Locator)Activator.CreateInstance(tExt);
            locators.Add(ext);
        }

        public static Locator Select(IntPtr targetHwnd) {
            return locators.FirstOrDefault(x => x.GetIsValidProcess(targetHwnd));
        }

        public static IExtension From(IDataAccess element) {
            return ((IDataAccessInternal)element)?.Extension ?? Instance;
        }
    }

    public interface IExtension {
        T Get<T>() where T : IDataAccessStatic;
    }
    
    public abstract class ExtensionBase<TExtension> : IExtension where TExtension : ExtensionBase<TExtension>, new() {
        private readonly string name;

        Dictionary<Type, IDataAccessStatic> registeredInterfaces;

        static ExtensionBase() { }

        public static int StartCore(string path) {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
            var wh = new AutoResetEvent(false);
            var snoopUIThread = new Thread(() => {
                var ext = new TExtension();
                ExtensionLocator.Instance = ext;
                ext.RegisterInterfaces();
                var data = TransientSettingsData.LoadCurrent(path);
                wh.Set();
                StartSnoop(data, ext);
            });
            snoopUIThread.SetApartmentState(ApartmentState.STA);
            snoopUIThread.Start();
            wh.WaitOne();
            return 0;
        }

        static void StartSnoop(TransientSettingsData data, IExtension ext) {
            var asmSnoop = Assembly.LoadFrom(data.PathToSnoop);
            var tSnoopManager = asmSnoop.GetType("Snoop.Infrastructure.SnoopManager");
            var mStartSnoop = tSnoopManager.GetMethod("CreateSnoopWindow", BindingFlags.Static | BindingFlags.Public);
            mStartSnoop.Invoke(null, new object[] { ext, data, data.StartTarget });
        }

        static Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args) {
            var fileName = args.Name;
            var asmn = new AssemblyName(fileName);
            var shortName = asmn.Name;
            var asmp = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(typeof(TExtension).Assembly.Location), $"{shortName}.dll");
            if (!File.Exists(asmp))
                return null;
            try {
                return Assembly.LoadFrom(asmp);
            } catch {
                return null;
            }
        }

        protected ExtensionBase(string name) {
            this.name = name;
            this.registeredInterfaces = new Dictionary<Type, IDataAccessStatic>();
        }

        public T Get<T>() where T : IDataAccessStatic { return (T)this.registeredInterfaces[typeof(T)]; }
        public string Path { get; private set; }

        protected void Set<T>(T instance) where T : IDataAccessStatic {
            this.registeredInterfaces.Add(typeof(T), instance);
            ((IDataAccessInternal)instance).Extension = this;
        }

        public abstract void RegisterInterfaces();
        public void SetPath(string path) { this.Path = path; }

        public static void Patch(ISnoopObject result) {
            ((IDataAccessInternal)result).Extension = ExtensionLocator.Instance;
        }
    }
}