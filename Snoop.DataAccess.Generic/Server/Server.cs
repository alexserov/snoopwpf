namespace Snoop.DataAccess.Sessions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Snoop.DataAccess.Generic;
    using Snoop.DataAccess.Impl;
    using Snoop.DataAccess.Internal.Interfaces;
    using WatsonTcp;

    public class Server : ISession
    {
        private WatsonTcpServer server = null;
        private Task serverTask;
        private int port;

        public Server(string name)
        {
            port = Session.IpPortFromName(name);
        }

        public string Send(string value, string type,  bool wait)
        {
            throw new InvalidOperationException();
        }

        public Task Start()
        {
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

        

        private void OnClientConnected(object sender, ClientConnectedEventArgs e)
        {
            this.server.Send(e.IpPort, EnumerateTypes());
        }

        public void Stop()
        {
            this.server.Dispose();
            this.server = null;
        }

        public Action<string> Logger { get; set; }
        public int OwnerPId { get; set; }
        public static Server Current { get; private set; }

        private SyncResponse SyncRequestReceived(SyncRequest arg)
        {
            var message = Message.Restore(arg.Data);
            Logger(message.Kind);
            switch (message.Kind)
            {
                case "process":
                    var result = Marshaller.Process(this, message.Json);
                    return new SyncResponse(arg, result);
            }

            return null;
        }
        private void OnMessageReceived(object sender, MessageReceivedFromClientEventArgs e)
        {
            var message = Message.Restore(e.Data);
            Logger(message.Kind);
            switch (message.Kind)
            {
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

        private List<(string typeName, string guid, IExecutor executor)> registered = new List<(string typeName, string guid, IExecutor executor)>();

        private string EnumerateTypes()
        {
            Dictionary<string, string> typeValue = new Dictionary<string, string>();
            foreach (var element in this.registered)
            {
                typeValue[element.typeName] = element.guid;
            }

            return JsonConvert.SerializeObject(typeValue, Formatting.Indented);
        }

        public void Register<TInterface>(TInterface instance) where TInterface : IDataAccessStatic
        {
            this.registered.Add((typeof(TInterface).ToString(), instance.Id, Impl.Marshaller.CreateServerExecutor(typeof(TInterface), instance)));
        }

        public IExecutor GetExecutor(string infoCallerId)
        {
            return this.registered.FirstOrDefault(x => x.guid == infoCallerId).executor;
        }
    }
}