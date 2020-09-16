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
            (typeof(Snoop.DataAccess.Interfaces.ISnoopObject), x=>new ISnoopObjectServer((Snoop.DataAccess.Interfaces.ISnoopObject)x), (s, x)=>new ISnoopObjectClient(s, x) ),
 
            (typeof(Snoop.DataAccess.Interfaces.IWindowHelper), x=>new IWindowHelperServer((Snoop.DataAccess.Interfaces.IWindowHelper)x), (s, x)=>new IWindowHelperClient(s, x) ),
 
            (typeof(Snoop.DataAccess.Interfaces.IWindowInfo), x=>new IWindowInfoServer((Snoop.DataAccess.Interfaces.IWindowInfo)x), (s, x)=>new IWindowInfoClient(s, x) ),
 
            (typeof(Snoop.DataAccess.Interfaces.IFakeInterface), x=>new IFakeInterfaceServer((Snoop.DataAccess.Interfaces.IFakeInterface)x), (s, x)=>new IFakeInterfaceClient(s, x) ),
 
            (typeof(Snoop.DataAccess.Interfaces.IFakeInterface2), x=>new IFakeInterface2Server((Snoop.DataAccess.Interfaces.IFakeInterface2)x), (s, x)=>new IFakeInterface2Client(s, x) ),
 
            (typeof(Snoop.DataAccess.Interfaces.IWindowInstance), x=>new IWindowInstanceServer((Snoop.DataAccess.Interfaces.IWindowInstance)x), (s, x)=>new IWindowInstanceClient(s, x) ),
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
     
    public sealed class ISnoopObjectServer : IExecutor {
        readonly ISnoopObject source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public ISnoopObjectServer(ISnoopObject source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="get_TypeName") {
                return source.TypeName;
            }
            return null;
        }
    }
    public sealed class ISnoopObjectClient : Snoop.DataAccess.Interfaces.ISnoopObject, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISnoopObjectClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_get_TypeName {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISnoopObject, System.String, object>(this, true, "get_TypeName", null); }
        }
    }
     
    public sealed class IWindowHelperServer : IExecutor {
        readonly IWindowHelper source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IWindowHelperServer(IWindowHelper source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="GetVisibleWindow") {
                return source.GetVisibleWindow(((CallInfo<IWindowHelperClient.PackedArgs_GetVisibleWindow>)parameters).Args.hwnd);                 
            }
            return null;
        }
    }
    public sealed class IWindowHelperClient : Snoop.DataAccess.Interfaces.IWindowHelper, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IWindowHelperClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_GetVisibleWindow {
        public System.Int64 hwnd { get; set; }
        }
        public Snoop.DataAccess.Interfaces.IWindowInstance GetVisibleWindow(System.Int64 hwnd) { return Marshaller.Call<Snoop.DataAccess.Interfaces.IWindowHelper, Snoop.DataAccess.Interfaces.IWindowInstance, PackedArgs_GetVisibleWindow>(this, true, "GetVisibleWindow", new PackedArgs_GetVisibleWindow(){hwnd = hwnd}); }
    }
     
    public sealed class IWindowInfoServer : IExecutor {
        readonly IWindowInfo source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IWindowInfoServer(IWindowInfo source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
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
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IFakeInterfaceServer(IFakeInterface source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
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
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IFakeInterface2Server(IFakeInterface2 source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
            this.eventCounter["SomeEvent"] = 0;
        }
        System.String OnSomeEvent(System.Int32 arg1, System.Boolean arg2){
            return Marshaller.Call<Snoop.DataAccess.Interfaces.IFakeInterface2, System.String, IFakeInterface2Client.PackedArgs_SomeEvent>(this, true, "RaiseSomeEvent", new IFakeInterface2Client.PackedArgs_SomeEvent(){arg1 = arg1, arg2 = arg2});
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="add_SomeEvent") {
                var count = eventCounter["SomeEvent"];
                if(count==0) {
                    source.SomeEvent += OnSomeEvent;
                }
                eventCounter["SomeEvent"] = count + 1;
            }
            if(methodName=="remove_SomeEvent") {
                var count = eventCounter["SomeEvent"];
                if(count==1) {
                    source.SomeEvent -= OnSomeEvent;
                }
                eventCounter["SomeEvent"] = count - 1;
            }
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
        public class PackedArgs_SomeEvent {
        public System.Int32 arg1 { get; set; }
        public System.Boolean arg2 { get; set; }
        }
    System.Func<System.Int32, System.Boolean, System.String> _SomeEvent;
        public event System.Func<System.Int32, System.Boolean, System.String> SomeEvent {
            add {  
                 _SomeEvent += value;
                 Marshaller.Call<Snoop.DataAccess.Interfaces.IFakeInterface2, object, object>(this, true, "add_SomeEvent", null); 
            }
            remove {
                 _SomeEvent -= value;
                 Marshaller.Call<Snoop.DataAccess.Interfaces.IFakeInterface2, object, object>(this, true, "remove_SomeEvent", null);
            }
        }
        public System.String RaiseSomeEvent(System.Int32 arg1, System.Boolean arg2) {
            return _SomeEvent(arg1, arg2);
        }
    }
     
    public sealed class IWindowInstanceServer : IExecutor {
        readonly IWindowInstance source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IWindowInstanceServer(IWindowInstance source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="get_TypeName") {
                return source.TypeName;
            }
            if(methodName=="get_Title") {
                return source.Title;
            }
            if(methodName=="set_Title") {
                source.Title = ((CallInfo<IWindowInstanceClient.PackedArgs_set_Title>)parameters).Args.value;
                return null;
            }
            return null;
        }
    }
    public sealed class IWindowInstanceClient : Snoop.DataAccess.Interfaces.IWindowInstance, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IWindowInstanceClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_get_TypeName {
        }
        public class PackedArgs_get_Title {
        }
        public class PackedArgs_set_Title {
        public System.String value { get; set; }
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.IWindowInstance, System.String, object>(this, true, "get_TypeName", null); }
        }
        public System.String Title {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.IWindowInstance, System.String, object>(this, true, "get_Title", null); }
            set { Marshaller.Call<Snoop.DataAccess.Interfaces.IWindowInstance, System.String, PackedArgs_set_Title>(this, false, "set_Title", new PackedArgs_set_Title() { value = value }); }
        }
    }
}