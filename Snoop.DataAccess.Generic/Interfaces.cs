using System;
using System.Linq;
using System.Collections.Generic;
using Snoop.DataAccess.Interfaces;
using Snoop.DataAccess.Sessions;
using Snoop.DataAccess.Internal.Interfaces;
// ReSharper disable HeapView.BoxingAllocation

namespace Snoop.DataAccess.Impl {

    partial class Marshaller {
        static List<(Type tInterface, Func<object, IExecutor> factoryServer, Func<ISession, string, IDataAccess> factoryClient)> registeredTypes = new List<(Type tInterface, Func<object, IExecutor> factoryServer, Func<ISession, string, IDataAccess> factoryClient)>
        { 
            (typeof(Snoop.DataAccess.Interfaces.IWindowInfo), x=>new IWindowInfoServer((Snoop.DataAccess.Interfaces.IWindowInfo)x), (s, x)=>new IWindowInfoClient(s, x) ),
 
            (typeof(Snoop.DataAccess.Interfaces.IFakeInterface), x=>new IFakeInterfaceServer((Snoop.DataAccess.Interfaces.IFakeInterface)x), (s, x)=>new IFakeInterfaceClient(s, x) ),
 
            (typeof(Snoop.DataAccess.Interfaces.IFakeInterface2), x=>new IFakeInterface2Server((Snoop.DataAccess.Interfaces.IFakeInterface2)x), (s, x)=>new IFakeInterface2Client(s, x) ),
        };
        public static IExecutor CreateServerExecutor(Type type, object instance) {
            var info = registeredTypes.FirstOrDefault(x => x.tInterface == type);
            return info.factoryServer(instance);
        }
        public static IDataAccess CreateClientExecutor(ISession session, Type type, string id) {
            var info = registeredTypes.FirstOrDefault(x => x.tInterface == type);
            return info.factoryClient(session, id);
        }
    }
     
    public sealed class IWindowInfoServer : IExecutor {
        readonly IWindowInfo source;
        readonly string id;
        public IWindowInfoServer(IWindowInfo source){
            this.source = source;
            this.id = Guid.NewGuid().ToString();
        }
        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="GetIsValidProcess") {
                return source.GetIsValidProcess(((CallInfo<IWindowInfoClient.PackedArgs_GetIsValidProcess>)parameters).Args.hwnd);                 
            }
            return null;
        }
    }
    public sealed class IWindowInfoClient : Snoop.DataAccess.Interfaces.IWindowInfo, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IWindowInfoClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_GetIsValidProcess {
        public System.IntPtr hwnd { get; set; }
        } 
        public System.Boolean GetIsValidProcess(System.IntPtr hwnd) { return Marshaller.Call<Snoop.DataAccess.Interfaces.IWindowInfo, System.Boolean, PackedArgs_GetIsValidProcess>(this, true, "GetIsValidProcess", new PackedArgs_GetIsValidProcess(){hwnd = hwnd}); }
    }
     
    public sealed class IFakeInterfaceServer : IExecutor {
        readonly IFakeInterface source;
        readonly string id;
        public IFakeInterfaceServer(IFakeInterface source){
            this.source = source;
            this.id = Guid.NewGuid().ToString();
        }
        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="DoSomethingIllegal") {
                return source.DoSomethingIllegal(((CallInfo<IFakeInterfaceClient.PackedArgs_DoSomethingIllegal>)parameters).Args.element, ((CallInfo<IFakeInterfaceClient.PackedArgs_DoSomethingIllegal>)parameters).Args.value, ((CallInfo<IFakeInterfaceClient.PackedArgs_DoSomethingIllegal>)parameters).Args.hello);                 
            }
            return null;
        }
    }
    public sealed class IFakeInterfaceClient : Snoop.DataAccess.Interfaces.IFakeInterface, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IFakeInterfaceClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_DoSomethingIllegal {
        public Snoop.DataAccess.Interfaces.IFakeInterface2 element { get; set; }
        public System.Boolean value { get; set; }
        public System.String hello { get; set; }
        } 
        public System.Boolean DoSomethingIllegal(Snoop.DataAccess.Interfaces.IFakeInterface2 element, System.Boolean value, System.String hello) { return Marshaller.Call<Snoop.DataAccess.Interfaces.IFakeInterface, System.Boolean, PackedArgs_DoSomethingIllegal>(this, true, "DoSomethingIllegal", new PackedArgs_DoSomethingIllegal(){element = element, value = value, hello = hello}); }
    }
     
    public sealed class IFakeInterface2Server : IExecutor {
        readonly IFakeInterface2 source;
        readonly string id;
        public IFakeInterface2Server(IFakeInterface2 source){
            this.source = source;
            this.id = Guid.NewGuid().ToString();
        }
        public object Execute(string methodName, ICallInfo parameters) {
            return null;
        }
    }
    public sealed class IFakeInterface2Client : Snoop.DataAccess.Interfaces.IFakeInterface2, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IFakeInterface2Client(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    }
}