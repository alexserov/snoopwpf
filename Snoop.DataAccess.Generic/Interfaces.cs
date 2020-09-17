/*
 * THIS CODE IS AUTO-GENERATED
 */
using System;
using System.Linq;
using System.Collections.Generic;
using Snoop.DataAccess.Interfaces;
using Snoop.DataAccess.Sessions;
using Snoop.DataAccess.Internal.Interfaces;
// ReSharper disable HeapView.BoxingAllocation

namespace Snoop.DataAccess.Impl {

    partial class Marshaller {
        static Marshaller() {
            registeredTypes = new List<(Type tInterface, Func<object, IExecutor> factoryServer, Func<ISession, string, IDataAccess> factoryClient)> {
                 (typeof(Snoop.DataAccess.Interfaces.ISnoopObject), x=>new ISnoopObjectServer((Snoop.DataAccess.Interfaces.ISnoopObject)x), (s, x)=>new ISnoopObjectClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_DependencyObject), x=>new ISO_DependencyObjectServer((Snoop.DataAccess.Interfaces.ISO_DependencyObject)x), (s, x)=>new ISO_DependencyObjectClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_FrameworkElement), x=>new ISO_FrameworkElementServer((Snoop.DataAccess.Interfaces.ISO_FrameworkElement)x), (s, x)=>new ISO_FrameworkElementClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_UIElement), x=>new ISO_UIElementServer((Snoop.DataAccess.Interfaces.ISO_UIElement)x), (s, x)=>new ISO_UIElementClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_Visual), x=>new ISO_VisualServer((Snoop.DataAccess.Interfaces.ISO_Visual)x), (s, x)=>new ISO_VisualClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_Visual3D), x=>new ISO_Visual3DServer((Snoop.DataAccess.Interfaces.ISO_Visual3D)x), (s, x)=>new ISO_Visual3DClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_Window), x=>new ISO_WindowServer((Snoop.DataAccess.Interfaces.ISO_Window)x), (s, x)=>new ISO_WindowClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.IDAS_InputManagerStatic), x=>new IDAS_InputManagerStaticServer((Snoop.DataAccess.Interfaces.IDAS_InputManagerStatic)x), (s, x)=>new IDAS_InputManagerStaticClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.IDAS_MouseStatic), x=>new IDAS_MouseStaticServer((Snoop.DataAccess.Interfaces.IDAS_MouseStatic)x), (s, x)=>new IDAS_MouseStaticClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.IDAS_VisualTreeHelper), x=>new IDAS_VisualTreeHelperServer((Snoop.DataAccess.Interfaces.IDAS_VisualTreeHelper)x), (s, x)=>new IDAS_VisualTreeHelperClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.IDAS_WindowHelperStatic), x=>new IDAS_WindowHelperStaticServer((Snoop.DataAccess.Interfaces.IDAS_WindowHelperStatic)x), (s, x)=>new IDAS_WindowHelperStaticClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.IDAS_WindowInfoStatic), x=>new IDAS_WindowInfoStaticServer((Snoop.DataAccess.Interfaces.IDAS_WindowInfoStatic)x), (s, x)=>new IDAS_WindowInfoStaticClient(s, x) ),
            };        
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
            if(methodName=="get_Source") {
                return source.Source;
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
        public class PackedArgs_get_Source {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISnoopObject, System.String, object>(this, true, "get_TypeName", null); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISnoopObject, System.Object, object>(this, true, "get_Source", null); }
        }
    }
     
    public sealed class ISO_DependencyObjectServer : IExecutor {
        readonly ISO_DependencyObject source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public ISO_DependencyObjectServer(ISO_DependencyObject source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="get_TypeName") {
                return source.TypeName;
            }
            if(methodName=="get_Source") {
                return source.Source;
            }
            return null;
        }
    }
    public sealed class ISO_DependencyObjectClient : Snoop.DataAccess.Interfaces.ISO_DependencyObject, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_DependencyObjectClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_get_TypeName {
        }
        public class PackedArgs_get_Source {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_DependencyObject, System.String, object>(this, true, "get_TypeName", null); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_DependencyObject, System.Object, object>(this, true, "get_Source", null); }
        }
    }
     
    public sealed class ISO_FrameworkElementServer : IExecutor {
        readonly ISO_FrameworkElement source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public ISO_FrameworkElementServer(ISO_FrameworkElement source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="get_RenderSize") {
                return source.RenderSize;
            }
            if(methodName=="set_RenderSize") {
                source.RenderSize = ((CallInfo<ISO_FrameworkElementClient.PackedArgs_set_RenderSize>)parameters).Args.value;
                return null;
            }
            if(methodName=="get_TypeName") {
                return source.TypeName;
            }
            if(methodName=="get_Source") {
                return source.Source;
            }
            if(methodName=="get_TemplatedParent") {
                return source.TemplatedParent;
            }
            return null;
        }
    }
    public sealed class ISO_FrameworkElementClient : Snoop.DataAccess.Interfaces.ISO_FrameworkElement, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_FrameworkElementClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_get_RenderSize {
        }
        public class PackedArgs_set_RenderSize {
        public System.ValueTuple<System.Double, System.Double> value { get; set; }
        }
        public class PackedArgs_get_TypeName {
        }
        public class PackedArgs_get_Source {
        }
        public class PackedArgs_get_TemplatedParent {
        }
        public System.ValueTuple<System.Double, System.Double> RenderSize {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_FrameworkElement, System.ValueTuple<System.Double, System.Double>, object>(this, true, "get_RenderSize", null); }
            set { Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_FrameworkElement, System.ValueTuple<System.Double, System.Double>, PackedArgs_set_RenderSize>(this, false, "set_RenderSize", new PackedArgs_set_RenderSize() { value = value }); }
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_FrameworkElement, System.String, object>(this, true, "get_TypeName", null); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_FrameworkElement, System.Object, object>(this, true, "get_Source", null); }
        }
        public Snoop.DataAccess.Interfaces.ISO_DependencyObject TemplatedParent {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_FrameworkElement, Snoop.DataAccess.Interfaces.ISO_DependencyObject, object>(this, true, "get_TemplatedParent", null); }
        }
    }
     
    public sealed class ISO_UIElementServer : IExecutor {
        readonly ISO_UIElement source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public ISO_UIElementServer(ISO_UIElement source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="get_TypeName") {
                return source.TypeName;
            }
            if(methodName=="get_Source") {
                return source.Source;
            }
            if(methodName=="get_RenderSize") {
                return source.RenderSize;
            }
            if(methodName=="set_RenderSize") {
                source.RenderSize = ((CallInfo<ISO_UIElementClient.PackedArgs_set_RenderSize>)parameters).Args.value;
                return null;
            }
            return null;
        }
    }
    public sealed class ISO_UIElementClient : Snoop.DataAccess.Interfaces.ISO_UIElement, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_UIElementClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_get_TypeName {
        }
        public class PackedArgs_get_Source {
        }
        public class PackedArgs_get_RenderSize {
        }
        public class PackedArgs_set_RenderSize {
        public System.ValueTuple<System.Double, System.Double> value { get; set; }
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UIElement, System.String, object>(this, true, "get_TypeName", null); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UIElement, System.Object, object>(this, true, "get_Source", null); }
        }
        public System.ValueTuple<System.Double, System.Double> RenderSize {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UIElement, System.ValueTuple<System.Double, System.Double>, object>(this, true, "get_RenderSize", null); }
            set { Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UIElement, System.ValueTuple<System.Double, System.Double>, PackedArgs_set_RenderSize>(this, false, "set_RenderSize", new PackedArgs_set_RenderSize() { value = value }); }
        }
    }
     
    public sealed class ISO_VisualServer : IExecutor {
        readonly ISO_Visual source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public ISO_VisualServer(ISO_Visual source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="get_TypeName") {
                return source.TypeName;
            }
            if(methodName=="get_Source") {
                return source.Source;
            }
            return null;
        }
    }
    public sealed class ISO_VisualClient : Snoop.DataAccess.Interfaces.ISO_Visual, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_VisualClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_get_TypeName {
        }
        public class PackedArgs_get_Source {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Visual, System.String, object>(this, true, "get_TypeName", null); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Visual, System.Object, object>(this, true, "get_Source", null); }
        }
    }
     
    public sealed class ISO_Visual3DServer : IExecutor {
        readonly ISO_Visual3D source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public ISO_Visual3DServer(ISO_Visual3D source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="get_TypeName") {
                return source.TypeName;
            }
            if(methodName=="get_Source") {
                return source.Source;
            }
            return null;
        }
    }
    public sealed class ISO_Visual3DClient : Snoop.DataAccess.Interfaces.ISO_Visual3D, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_Visual3DClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_get_TypeName {
        }
        public class PackedArgs_get_Source {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Visual3D, System.String, object>(this, true, "get_TypeName", null); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Visual3D, System.Object, object>(this, true, "get_Source", null); }
        }
    }
     
    public sealed class ISO_WindowServer : IExecutor {
        readonly ISO_Window source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public ISO_WindowServer(ISO_Window source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="get_TemplatedParent") {
                return source.TemplatedParent;
            }
            if(methodName=="get_RenderSize") {
                return source.RenderSize;
            }
            if(methodName=="set_RenderSize") {
                source.RenderSize = ((CallInfo<ISO_WindowClient.PackedArgs_set_RenderSize>)parameters).Args.value;
                return null;
            }
            if(methodName=="get_TypeName") {
                return source.TypeName;
            }
            if(methodName=="get_Source") {
                return source.Source;
            }
            if(methodName=="get_Title") {
                return source.Title;
            }
            if(methodName=="set_Title") {
                source.Title = ((CallInfo<ISO_WindowClient.PackedArgs_set_Title>)parameters).Args.value;
                return null;
            }
            return null;
        }
    }
    public sealed class ISO_WindowClient : Snoop.DataAccess.Interfaces.ISO_Window, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_WindowClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_get_TemplatedParent {
        }
        public class PackedArgs_get_RenderSize {
        }
        public class PackedArgs_set_RenderSize {
        public System.ValueTuple<System.Double, System.Double> value { get; set; }
        }
        public class PackedArgs_get_TypeName {
        }
        public class PackedArgs_get_Source {
        }
        public class PackedArgs_get_Title {
        }
        public class PackedArgs_set_Title {
        public System.String value { get; set; }
        }
        public Snoop.DataAccess.Interfaces.ISO_DependencyObject TemplatedParent {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, Snoop.DataAccess.Interfaces.ISO_DependencyObject, object>(this, true, "get_TemplatedParent", null); }
        }
        public System.ValueTuple<System.Double, System.Double> RenderSize {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, System.ValueTuple<System.Double, System.Double>, object>(this, true, "get_RenderSize", null); }
            set { Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, System.ValueTuple<System.Double, System.Double>, PackedArgs_set_RenderSize>(this, false, "set_RenderSize", new PackedArgs_set_RenderSize() { value = value }); }
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, System.String, object>(this, true, "get_TypeName", null); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, System.Object, object>(this, true, "get_Source", null); }
        }
        public System.String Title {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, System.String, object>(this, true, "get_Title", null); }
            set { Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, System.String, PackedArgs_set_Title>(this, false, "set_Title", new PackedArgs_set_Title() { value = value }); }
        }
    }
     
    public sealed class IDAS_InputManagerStaticServer : IExecutor {
        readonly IDAS_InputManagerStatic source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IDAS_InputManagerStaticServer(IDAS_InputManagerStatic source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
            this.eventCounter["PreProcessInput"] = 0;
        }
        void OnPreProcessInput(){
            Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_InputManagerStatic, object, IDAS_InputManagerStaticClient.PackedArgs_PreProcessInput>(this, false, "RaisePreProcessInput", new IDAS_InputManagerStaticClient.PackedArgs_PreProcessInput(){});
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="add_PreProcessInput") {
                var count = eventCounter["PreProcessInput"];
                if(count==0) {
                    source.PreProcessInput += OnPreProcessInput;
                }
                eventCounter["PreProcessInput"] = count + 1;
            }
            if(methodName=="remove_PreProcessInput") {
                var count = eventCounter["PreProcessInput"];
                if(count==1) {
                    source.PreProcessInput -= OnPreProcessInput;
                }
                eventCounter["PreProcessInput"] = count - 1;
            }
            return null;
        }
    }
    public sealed class IDAS_InputManagerStaticClient : Snoop.DataAccess.Interfaces.IDAS_InputManagerStatic, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IDAS_InputManagerStaticClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_PreProcessInput {
        }
    System.Action _PreProcessInput;
        public event System.Action PreProcessInput {
            add {  
                 _PreProcessInput += value;
                 Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_InputManagerStatic, object, object>(this, false, "add_PreProcessInput", null); 
            }
            remove {
                 _PreProcessInput -= value;
                 Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_InputManagerStatic, object, object>(this, false, "remove_PreProcessInput", null);
            }
        }
        public void RaisePreProcessInput() {
             _PreProcessInput();
        }
    }
     
    public sealed class IDAS_MouseStaticServer : IExecutor {
        readonly IDAS_MouseStatic source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IDAS_MouseStaticServer(IDAS_MouseStatic source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="get_DirectlyOver") {
                return source.DirectlyOver;
            }
            return null;
        }
    }
    public sealed class IDAS_MouseStaticClient : Snoop.DataAccess.Interfaces.IDAS_MouseStatic, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IDAS_MouseStaticClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_get_DirectlyOver {
        }
        public Snoop.DataAccess.Interfaces.ISnoopObject DirectlyOver {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_MouseStatic, Snoop.DataAccess.Interfaces.ISnoopObject, object>(this, true, "get_DirectlyOver", null); }
        }
    }
     
    public sealed class IDAS_VisualTreeHelperServer : IExecutor {
        readonly IDAS_VisualTreeHelper source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IDAS_VisualTreeHelperServer(IDAS_VisualTreeHelper source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            return null;
        }
    }
    public sealed class IDAS_VisualTreeHelperClient : Snoop.DataAccess.Interfaces.IDAS_VisualTreeHelper, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IDAS_VisualTreeHelperClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    }
     
    public sealed class IDAS_WindowHelperStaticServer : IExecutor {
        readonly IDAS_WindowHelperStatic source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IDAS_WindowHelperStaticServer(IDAS_WindowHelperStatic source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="GetVisibleWindow") {
                return source.GetVisibleWindow(((CallInfo<IDAS_WindowHelperStaticClient.PackedArgs_GetVisibleWindow>)parameters).Args.hwnd);                 
            }
            return null;
        }
    }
    public sealed class IDAS_WindowHelperStaticClient : Snoop.DataAccess.Interfaces.IDAS_WindowHelperStatic, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IDAS_WindowHelperStaticClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_GetVisibleWindow {
        public System.Int64 hwnd { get; set; }
        }
        public Snoop.DataAccess.Interfaces.ISO_Window GetVisibleWindow(System.Int64 hwnd) { return Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_WindowHelperStatic, Snoop.DataAccess.Interfaces.ISO_Window, PackedArgs_GetVisibleWindow>(this, true, "GetVisibleWindow", new PackedArgs_GetVisibleWindow(){hwnd = hwnd}); }
    }
     
    public sealed class IDAS_WindowInfoStaticServer : IExecutor {
        readonly IDAS_WindowInfoStatic source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IDAS_WindowInfoStaticServer(IDAS_WindowInfoStatic source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="GetIsValidProcess") {
                return source.GetIsValidProcess(((CallInfo<IDAS_WindowInfoStaticClient.PackedArgs_GetIsValidProcess>)parameters).Args.hwnd);                 
            }
            return null;
        }
    }
    public sealed class IDAS_WindowInfoStaticClient : Snoop.DataAccess.Interfaces.IDAS_WindowInfoStatic, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IDAS_WindowInfoStaticClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
        public class PackedArgs_GetIsValidProcess {
        public System.IntPtr hwnd { get; set; }
        }
        public System.Boolean GetIsValidProcess(System.IntPtr hwnd) { return Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_WindowInfoStatic, System.Boolean, PackedArgs_GetIsValidProcess>(this, true, "GetIsValidProcess", new PackedArgs_GetIsValidProcess(){hwnd = hwnd}); }
    }
}