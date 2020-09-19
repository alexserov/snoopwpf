namespace Snoop.DataAccess.Sessions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Snoop.DataAccess.Generic;
    using Snoop.DataAccess.Impl;
    using Snoop.DataAccess.Internal.Interfaces;
    using WatsonTcp;

    public class Client : ISession
    {
        private WatsonTcpClient client = null;
        private Task clientTask;
        private int port;

        public Client(string name)
        {
            port = Session.IpPortFromName(name, false);
        }

        public string Send(string value, string type, bool wait)
        {
            var message = new Message() { Kind = type, Json = value };
            if (wait)
            {
                var debugmultiplier = Debugger.IsAttached ? 1000 : 1;
                return Encoding.UTF8.GetString(this.client.SendAndWait(10 * 1000 * debugmultiplier, message.Save()).Data);
            }

            this.client.Send(message.Save());
            return null;
        }

        public Task Start()
        {
            client = new WatsonTcpClient("127.0.0.1", port);
            client.Events.MessageReceived += MessageReceived;
            client.Settings.Logger = Logger;
            client.Settings.DebugMessages = true;
            client.Callbacks.SyncRequestReceived = SyncRequestReceived;
            this.clientTask = client.StartAsync();
            this.client.Send(new Message() { Kind = "hello" }.Save());
            return this.clientTask;
        }

        private SyncResponse SyncRequestReceived(SyncRequest arg)
        {
            return new SyncResponse(arg, "hello");
        }

        public void Stop()
        {
            this.client.Dispose();
            this.client = null;
        }

        public Action<string> Logger {
            get { return this.logger ?? (x=>Debug.WriteLine(x)); }
            set { this.logger = value; }
        }

        public int OwnerPId { get; set; }

        public TInterface Request<TInterface>() where TInterface : IDataAccessStatic
        {
            if (this.knownTypes == null)
                return default;

            if (this.knownTypes.TryGetValue(typeof(TInterface).ToString(), out var guid))
            {
                return (TInterface)Impl.Marshaller.CreateClientExecutor(this, typeof(TInterface), guid);
            }

            return default;
        }

        private Dictionary<string, string> knownTypes = null;
        Action<string> logger;

        private void MessageReceived(object sender, MessageReceivedFromServerEventArgs e)
        {
            var message = Message.Restore(e.Data);
            switch (message.Kind)
            {
                case "types":
                    this.knownTypes = JsonConvert.DeserializeObject<Dictionary<string, string>>(message.Json);
                    break;
            }
        }
    }
}