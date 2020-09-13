namespace Snoop.DataAccess.Sessions
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;

    public interface ISession
    {
        string Send(string value, string type,  bool wait);
        Task Start();
        void Stop();
        Action<string> Logger { get; set; }
        int OwnerPId { get; set; }
    }
    public class Session
    {
        
        public static int IpPortFromName(string name)
        {
            //todo better port generation
            var value = 9000;
            switch (name)
            {
                case "WinUI3":
                    value = 9004;
                    break;
                case "Wpf":
                    value = 9001;
                    break;
            }

            return value;
        }

        public static void Register(IDataAccess element)
        {
            
        }
    }
}