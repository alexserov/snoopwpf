namespace Snoop.DataAccess.Sessions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO.MemoryMappedFiles;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Snoop.DataAccess.Generic;
    using Snoop.DataAccess.Impl;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;
    using WatsonTcp;

    public class Server : ISession, IDisposable {
        public void Dispose() {
            this.server?.Dispose();
            this.serverTask?.Dispose();
            this.memoryMappedFile?.Dispose();
        }

        public string Name { get; }
        public bool IsGeneric { get; }
        private WatsonTcpServer server = null;
        private Task serverTask;
        private ushort port;
        MemoryMappedFile memoryMappedFile;

        public Server(string name, bool isGeneric) {
            this.Name = name;
            this.IsGeneric = isGeneric;
            port = Session.IpPortFromName(name, true);
            this.memoryMappedFile = MemoryMappedFile.CreateNew("snoopmmf" + name, sizeof(ushort));
            using (var wr = this.memoryMappedFile.CreateViewAccessor(0, sizeof(ushort), MemoryMappedFileAccess.Write))
                wr.Write(0, this.port);
        }

        public string Send(string value, string type, bool wait) { throw new InvalidOperationException(); }

        public Task Start() {
            server = new WatsonTcpServer("127.0.0.1", port);
            server.Events.MessageReceived += OnMessageReceived;
            server.Events.ClientConnected += OnClientConnected;
            server.Settings.Logger = Logger;
            server.Settings.DebugMessages = true;
            server.Callbacks.SyncRequestReceived = SyncRequestReceived;
            this.serverTask = server.StartAsync();
            Current = this;
            return this.serverTask;
        }



        private void OnClientConnected(object sender, ClientConnectedEventArgs e) { this.server.Send(e.IpPort, EnumerateTypes()); }

        public void Stop() {
            this.server.Dispose();
            this.server = null;
        }

        public Action<string> Logger {
            get { return this.logger ?? (x => Debug.WriteLine(x)); }
            set { this.logger = value; }
        }

        public int OwnerPId { get; set; }
        public static Server Current { get; private set; }

        private SyncResponse SyncRequestReceived(SyncRequest arg) {
            var message = Message.Restore(arg.Data);
            Logger(message.Kind);
            switch (message.Kind) {
                case "process":
                    var result = Marshaller.Process(this, message.Json);
                    return new SyncResponse(arg, result);
            }

            return null;
        }

        private void OnMessageReceived(object sender, MessageReceivedFromClientEventArgs e) {
            var message = Message.Restore(e.Data);
            Logger(message.Kind);
            switch (message.Kind) {
                case "hello":
                    var msg = new Message() { Kind = "types", Json = this.EnumerateTypes() }.Save();
                    Logger(msg);
                    this.server.Send(e.IpPort, msg);
                    break;
                case "process":
                    Marshaller.Process(this, message.Json);
                    break;
            }
        }

        private List<(Type type, string typeName, string guid, IExecutor executor, IDataAccessStatic instance)> registered = new List<(Type type, string typeName, string guid, IExecutor executor, IDataAccessStatic instance)>();
        Dictionary<string, IDataAccess> registeredInstances = new Dictionary<string, IDataAccess>();
        Action<string> logger;

        private string EnumerateTypes() {
            Dictionary<string, string> typeValue = new Dictionary<string, string>();
            foreach (var element in this.registered) {
                typeValue[element.typeName] = element.guid;
            }

            return JsonConvert.SerializeObject(typeValue, Formatting.Indented);
        }

        public void Register<TInterface>(TInterface instance) where TInterface : IDataAccessStatic { this.registered.Add((typeof(TInterface), typeof(TInterface).ToString(), instance.Id, Impl.Marshaller.CreateServerExecutor(typeof(TInterface), instance), instance)); }

        public TInterface Instance<TInterface>(TInterface instance) where TInterface : IDataAccess { return (TInterface)this.registered.First(x => x.type == typeof(TInterface)).instance; }

        public IExecutor GetExecutor(string infoCallerId) {
            var staticResult = this.registered.FirstOrDefault(x => x.guid == infoCallerId).executor;
            if (staticResult != null)
                return staticResult;
            if(this.registeredInstances.TryGetValue(infoCallerId, out var result)) {
                var daServer = result as IDataAccessServer;
                if (daServer == null)
                    return null;
                return Marshaller.CreateServerExecutor(daServer.DataAccessType, result);
            }

            return null;
        }

        public void RegisterInstance(DataAccessBase dataAccessBase) {
            this.registeredInstances.Add(dataAccessBase.Id, dataAccessBase);
        }

        public DataAccessBase FindRegistered(string guid) {
            return this.registeredInstances[guid] as DataAccessBase;
        }
    }
}