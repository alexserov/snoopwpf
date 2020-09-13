namespace Snoop.DataAccess.WinUI3
{
    using System;
    using Snoop.DataAccess.Sessions;

    public class Program
    {
        public static void Main()
        {
            Server wuiSrv = new Server("WinUI3");
            wuiSrv.Logger = Console.WriteLine;
            wuiSrv.Start().Wait();
        }
    }
}