namespace Snoop.DataAccess.Sessions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Snoop.DataAccess.Internal.Interfaces;

    public class Extension
    {
        private readonly string name;
        static List<Extension> extensions;
        private Process extProc;
        private Client client;

        static Extension()
        {
            extensions = new List<Extension>();
        }
        Extension(string name)
        {
            this.name = name;
            extensions.Add(this);
        }

        public static void Start(string name)
        {
            new Extension(name).Start();
        }
        void Start()
        {
            this.extProc = Process.Start(
                new ProcessStartInfo(name, $"{Process.GetCurrentProcess().Id}")
                {
                    Verb = "runas"
                }
            );
            var clientName = Path.GetFileNameWithoutExtension(this.name);
            clientName = clientName.Substring("Snoop.DataAccess.".Length);
            this.client = new Client(clientName);
            this.client.Start();
        }

        public static Extension Select<T>(Func<Func<T>, bool> requestPredicate) where T : IDataAccessStatic {
            return extensions.FirstOrDefault(x => requestPredicate(x.client.Request<T>));
        }
        public static IEnumerable<T> Request<T>() where T : IDataAccessStatic
        {
            return extensions.Select(x => x.client.Request<T>()).Where(x => x != null);
        }
        public T Get<T>() where T: IDataAccessStatic {
            return this.client.Request<T>();
        }
    }
}