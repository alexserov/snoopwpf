namespace Snoop.DataAccess.Wpf
{
    using System;
    using Snoop.DataAccess.Impl;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class Program
    {
        public static void Main()
        {
            try
            {
                Server wpfSrv = new Server("Wpf");
                RegisterStatics(wpfSrv);
                wpfSrv.Logger = Console.WriteLine;
                wpfSrv.Start().Wait();
            }
            catch
            {
                Console.ReadLine();
            }
        }

        private static void RegisterStatics(Server server)
        {
            server.Register<IWindowInfo>(new WindowInfo());
        }
    }
}