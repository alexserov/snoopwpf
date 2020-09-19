namespace Snoop.DataAccess.Sessions {
    using System;
    using System.Diagnostics;
    using System.Reflection;

    public class ServerLauncher {
        public static int StartSnoop(string param, Action<Server> init) {
            if (Server.Current != null)
                return 1;
            AttachAssemblyResolveHandler(AppDomain.CurrentDomain);
            var result = new Server(param, false);
            result.Start();
            init(result);
            return 0;
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