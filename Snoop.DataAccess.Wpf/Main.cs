using System.Threading;

namespace Snoop.DataAccess {
    using System;
    using Snoop.DataAccess.Impl;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;
    using Snoop.DataAccess.Wpf;
    
    public class Program {
        public static void Main() {
            try {
                Server wpfSrv = new Server("Wpf", true);
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
            if (server.IsGeneric)
                return;
            server.Register<IDAS_CurrentApplication>(new DAS_CurrentApplication());
            server.Register<IDAS_InputManager>(new DAS_InputManager());
            server.Register<IDAS_Mouse>(new DAS_Mouse());
            server.Register<IDAS_RootProvider>(new DAS_RootProvider());
            server.Register<IDAS_VisualTreeHelper>(new DAS_VisualTreeHelper());            
            server.Register<IDAS_WindowHelper>(new DAS_WindowHelper());
            server.Register<IDAS_WindowInfo>(new DAS_WindowInfo());
        }
    }
}