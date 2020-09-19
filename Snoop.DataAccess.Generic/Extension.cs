namespace Snoop.DataAccess.Sessions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Snoop.DataAccess.Internal.Interfaces;

    public class ClientExtension : Extension {
        readonly string clientName;

        public ClientExtension(Extension source, string clientName) : base(source.Path) { this.clientName = clientName; }
        protected override string GetClientName() { return this.clientName; }
        protected override void StartProcess() {  }
    }
    
    public class Extension
    {
        private readonly string name;
        static List<Extension> extensions;
        private Process extProc;
        private Client client;
        public string Path {
            get { return this.name; }
        }

        static Extension()
        {
            extensions = new List<Extension>();
        }

        protected Extension(string name )
        {
            this.name = name;
        }

        public static void Start(string name) {
            var ext = new Extension(name);
            ext.Start();
        }

        public void Start() {
            this.StartProcess();
            Register(this);
            this.client = new Client(this.GetClientName());
            this.client.Start();
        }

        protected virtual void StartProcess() {
            this.extProc = Process.Start(
                new ProcessStartInfo(this.name, $"{Process.GetCurrentProcess().Id}") {
                    // UseShellExecute = true,
                    // CreateNoWindow = true,
                    // WindowStyle = ProcessWindowStyle.Hidden,
                    Verb = "runas"
                }
            );
        }

        protected virtual string GetClientName() {
            return System.IO.Path.GetFileNameWithoutExtension(this.name).Substring("Snoop.DataAccess.".Length);
        }

        public static Extension Select<T>(Func<Func<T>, bool> requestPredicate) where T : IDataAccessStatic {
            return extensions.FirstOrDefault(x => !(x is ClientExtension) && x.ProcessRequestPredicate(requestPredicate));
        }
        public static IEnumerable<T> Request<T>() where T : IDataAccessStatic {
            return extensions.Where(x => !(x is ClientExtension)).Select(x => x.client.Request<T>()).Where(x => x != null);
        }

        protected virtual bool ProcessRequestPredicate<T>(Func<Func<T>, bool> requestPredicate) where T : IDataAccessStatic { return requestPredicate(this.client.Request<T>); }

        public T Get<T>() where T: IDataAccessStatic {
            return this.client.Request<T>();
        }

        public static void Register(Extension ext) {
            extensions.Add(ext);
        }

        public static Extension From(IDataAccess element) { return extensions.FirstOrDefault(x => x.client == ((IDataAccessClient)element).Session); }
    }
}