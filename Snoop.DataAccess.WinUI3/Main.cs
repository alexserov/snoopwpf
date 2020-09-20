using System.Threading;

namespace Snoop.DataAccess {
    using System;
    using System.IO;
    using System.Reflection;
    using Snoop.DataAccess.Impl;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;
    using Snoop.DataAccess.WinUI3;
    
    public class Program {
        static Program() {
            AppDomain.CurrentDomain.AssemblyResolve+=CurrentDomainOnAssemblyResolve;
        }

        static Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args) {
            var fileName = args.Name;
            var asmn = new AssemblyName(fileName);
            var shortName = asmn.Name;
            var asmp = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), $"{shortName}.dll");
            if (!File.Exists(asmp))
                return null;
            try {
                return Assembly.LoadFrom(asmp);
            } catch {
                return null;
            }
        }

        public static void Main() {
            try {
                Server wpfSrv = new Server("WinUI3", true);
                RegisterStatics(wpfSrv);
                wpfSrv.Logger = Console.WriteLine;
                wpfSrv.Start().Wait();
            } catch {
                Console.ReadLine();
            }
        }

        public static int Start(string param) {
            var wh = new EventWaitHandle(false, EventResetMode.ManualReset);
            int result = 0;
            var t = new Thread(() => {
                result = ServerLauncher.StartSnoop(param, RegisterStatics);
                wh.Set();
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            wh.WaitOne();
            return result;
        }

        private static void RegisterStatics(Server server) {
            server.Register<IDAS_WindowInfo>(new DAS_WindowInfo());
            server.Register<IDAS_InjectorLibraryPath>(new DAS_InjectorLibraryPath());
            if (server.IsGeneric)
                return;
            server.Register<IDAS_CurrentApplication>(new DAS_CurrentApplication());
            server.Register<IDAS_InputManager>(new DAS_InputManager());
            server.Register<IDAS_Mouse>(new DAS_Mouse());
            server.Register<IDAS_RootProvider>(new DAS_RootProvider());
            server.Register<IDAS_TreeHelper>(new DAS_TreeHelper());            
            server.Register<IDAS_WindowHelper>(new DAS_WindowHelper());
            server.Register<IDAS_WindowInfo>(new DAS_WindowInfo());
        }
    }
}