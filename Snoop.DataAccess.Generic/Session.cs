namespace Snoop.DataAccess.Sessions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO.MemoryMappedFiles;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Threading;
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

        public static ushort IpPortFromName(string name, bool server) {
            if (server)
                return GetAvailablePort(9000);
            for (int i = 0; i<100; i++) {
                Thread.Sleep(100);
                try {
                    using var mmf = MemoryMappedFile.OpenExisting("snoopmmf" + name);
                    using var acc = mmf.CreateViewAccessor();
                    return acc.ReadUInt16(0);
                } catch {
                    
                }
            }
            throw new InvalidOperationException();
        }

        static ushort GetAvailablePort(ushort startAt) {
            // Evaluate current system tcp connections. This is the same information provided
            // by the netstat command line application, just in .Net strongly-typed object
            // form.  We will look through the list, and if our port we would like to use
            // in our TcpClient is occupied, we will set isAvailable to false.
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var listeners = ipGlobalProperties.GetActiveTcpListeners().Select(x => (ushort)x.Port).ToList();
            var connections = ipGlobalProperties.GetActiveTcpConnections().Select(x => (ushort)x.LocalEndPoint.Port).ToList();
            var unavailablePorts = listeners.Concat(connections).Distinct().ToList();
            for (ushort i = startAt; i < ushort.MaxValue; i++) {
                if (!unavailablePorts.Contains(i))
                    return i;
            }

            throw new InvalidOperationException();
        }

        public static void Register(IDataAccess element)
        {
            
        }
    }
}