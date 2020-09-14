namespace Snoop.DataAccess.Sessions {
    using System;
    using System.Diagnostics;
    using System.Reflection;

    public class ServerLauncher {
        public static int StartSnoop(string settingsFile) {
            AttachAssemblyResolveHandler(AppDomain.CurrentDomain);
            new Server(Process.GetCurrentProcess().Id.ToString()).Start();
            return 1;
        }
        static void AttachAssemblyResolveHandler(AppDomain domain)
        {
            domain.AssemblyResolve += (sender, args) =>
            {
                if (args.Name.StartsWith("Snoop.Core,"))
                {
                    throw new NotImplementedException();
                    // return Assembly.GetExecutingAssembly();
                }

#if NETCOREAPP3_1
                if (args.Name.StartsWith("System.Management.Automation,"))
                {
                    return Assembly.LoadFrom(Snoop.PowerShell.ShellConstants.GetPowerShellAssemblyPath());
                }
#endif

                return null;
            };
        }
    }
}