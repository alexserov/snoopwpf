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
                 (typeof(Snoop.DataAccess.Interfaces.ISO_Brush), x=>new ISO_BrushServer((Snoop.DataAccess.Interfaces.ISO_Brush)x), (s, x)=>new ISO_BrushClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_DependencyObject), x=>new ISO_DependencyObjectServer((Snoop.DataAccess.Interfaces.ISO_DependencyObject)x), (s, x)=>new ISO_DependencyObjectClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_FrameworkElement), x=>new ISO_FrameworkElementServer((Snoop.DataAccess.Interfaces.ISO_FrameworkElement)x), (s, x)=>new ISO_FrameworkElementClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_ImageSource), x=>new ISO_ImageSourceServer((Snoop.DataAccess.Interfaces.ISO_ImageSource)x), (s, x)=>new ISO_ImageSourceClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_ResourceDictionary), x=>new ISO_ResourceDictionaryServer((Snoop.DataAccess.Interfaces.ISO_ResourceDictionary)x), (s, x)=>new ISO_ResourceDictionaryClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_UIElement), x=>new ISO_UIElementServer((Snoop.DataAccess.Interfaces.ISO_UIElement)x), (s, x)=>new ISO_UIElementClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_UISurface), x=>new ISO_UISurfaceServer((Snoop.DataAccess.Interfaces.ISO_UISurface)x), (s, x)=>new ISO_UISurfaceClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_Visual), x=>new ISO_VisualServer((Snoop.DataAccess.Interfaces.ISO_Visual)x), (s, x)=>new ISO_VisualClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_Visual3D), x=>new ISO_Visual3DServer((Snoop.DataAccess.Interfaces.ISO_Visual3D)x), (s, x)=>new ISO_Visual3DClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.ISO_Window), x=>new ISO_WindowServer((Snoop.DataAccess.Interfaces.ISO_Window)x), (s, x)=>new ISO_WindowClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.IDAS_CurrentApplication), x=>new IDAS_CurrentApplicationServer((Snoop.DataAccess.Interfaces.IDAS_CurrentApplication)x), (s, x)=>new IDAS_CurrentApplicationClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.IDAS_InputManager), x=>new IDAS_InputManagerServer((Snoop.DataAccess.Interfaces.IDAS_InputManager)x), (s, x)=>new IDAS_InputManagerClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.IDAS_Mouse), x=>new IDAS_MouseServer((Snoop.DataAccess.Interfaces.IDAS_Mouse)x), (s, x)=>new IDAS_MouseClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.IDAS_RootProvider), x=>new IDAS_RootProviderServer((Snoop.DataAccess.Interfaces.IDAS_RootProvider)x), (s, x)=>new IDAS_RootProviderClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.IDAS_VisualTreeHelper), x=>new IDAS_VisualTreeHelperServer((Snoop.DataAccess.Interfaces.IDAS_VisualTreeHelper)x), (s, x)=>new IDAS_VisualTreeHelperClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.IDAS_WindowHelper), x=>new IDAS_WindowHelperServer((Snoop.DataAccess.Interfaces.IDAS_WindowHelper)x), (s, x)=>new IDAS_WindowHelperClient(s, x) ),
                 (typeof(Snoop.DataAccess.Interfaces.IDAS_WindowInfo), x=>new IDAS_WindowInfoServer((Snoop.DataAccess.Interfaces.IDAS_WindowInfo)x), (s, x)=>new IDAS_WindowInfoClient(s, x) ),
            };        
        }
    }
     
    internal sealed class ISnoopObjectServer : IExecutor {
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
    internal sealed class ISnoopObjectClient : Snoop.DataAccess.Interfaces.ISnoopObject, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISnoopObjectClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_get_TypeName {
        }
        internal class PackedArgs_get_Source {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISnoopObject, System.String, PackedArgs_get_TypeName>(this, true, "get_TypeName", new PackedArgs_get_TypeName()); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISnoopObject, System.Object, PackedArgs_get_Source>(this, true, "get_Source", new PackedArgs_get_Source()); }
        }
    }
     
    internal sealed class ISO_BrushServer : IExecutor {
        readonly ISO_Brush source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public ISO_BrushServer(ISO_Brush source){
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
    internal sealed class ISO_BrushClient : Snoop.DataAccess.Interfaces.ISO_Brush, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_BrushClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_get_TypeName {
        }
        internal class PackedArgs_get_Source {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Brush, System.String, PackedArgs_get_TypeName>(this, true, "get_TypeName", new PackedArgs_get_TypeName()); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Brush, System.Object, PackedArgs_get_Source>(this, true, "get_Source", new PackedArgs_get_Source()); }
        }
    }
     
    internal sealed class ISO_DependencyObjectServer : IExecutor {
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
    internal sealed class ISO_DependencyObjectClient : Snoop.DataAccess.Interfaces.ISO_DependencyObject, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_DependencyObjectClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_get_TypeName {
        }
        internal class PackedArgs_get_Source {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_DependencyObject, System.String, PackedArgs_get_TypeName>(this, true, "get_TypeName", new PackedArgs_get_TypeName()); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_DependencyObject, System.Object, PackedArgs_get_Source>(this, true, "get_Source", new PackedArgs_get_Source()); }
        }
    }
     
    internal sealed class ISO_FrameworkElementServer : IExecutor {
        readonly ISO_FrameworkElement source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public ISO_FrameworkElementServer(ISO_FrameworkElement source){
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
            if(methodName=="GetRenderSize") {
                return source.GetRenderSize();                 
            }
            if(methodName=="IsDescendantOf") {
                return source.IsDescendantOf(((CallInfo<ISO_FrameworkElementClient.PackedArgs_IsDescendantOf>)parameters).Args.rootVisual);                 
            }
            if(methodName=="GetSurface") {
                return source.GetSurface();                 
            }
            if(methodName=="GetTemplatedParent") {
                return source.GetTemplatedParent();                 
            }
            if(methodName=="GetResources") {
                return source.GetResources();                 
            }
            if(methodName=="GetActualHeight") {
                return source.GetActualHeight();                 
            }
            if(methodName=="GetActualWidth") {
                return source.GetActualWidth();                 
            }
            return null;
        }
    }
    internal sealed class ISO_FrameworkElementClient : Snoop.DataAccess.Interfaces.ISO_FrameworkElement, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_FrameworkElementClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_GetRenderSize {
        }
        internal class PackedArgs_IsDescendantOf {
        public Snoop.DataAccess.Interfaces.ISO_Visual rootVisual { get; set; }
        }
        internal class PackedArgs_GetSurface {
        }
        internal class PackedArgs_get_TypeName {
        }
        internal class PackedArgs_get_Source {
        }
        internal class PackedArgs_GetTemplatedParent {
        }
        internal class PackedArgs_GetResources {
        }
        internal class PackedArgs_GetActualHeight {
        }
        internal class PackedArgs_GetActualWidth {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_FrameworkElement, System.String, PackedArgs_get_TypeName>(this, true, "get_TypeName", new PackedArgs_get_TypeName()); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_FrameworkElement, System.Object, PackedArgs_get_Source>(this, true, "get_Source", new PackedArgs_get_Source()); }
        }
        public System.ValueTuple<System.Double, System.Double> GetRenderSize() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_FrameworkElement, System.ValueTuple<System.Double, System.Double>, PackedArgs_GetRenderSize>(this, true, "GetRenderSize", new PackedArgs_GetRenderSize(){}); }
        public System.Boolean IsDescendantOf(Snoop.DataAccess.Interfaces.ISO_Visual rootVisual) { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_FrameworkElement, System.Boolean, PackedArgs_IsDescendantOf>(this, true, "IsDescendantOf", new PackedArgs_IsDescendantOf(){rootVisual = rootVisual}); }
        public Snoop.DataAccess.Interfaces.ISO_UISurface GetSurface() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_FrameworkElement, Snoop.DataAccess.Interfaces.ISO_UISurface, PackedArgs_GetSurface>(this, true, "GetSurface", new PackedArgs_GetSurface(){}); }
        public Snoop.DataAccess.Interfaces.ISO_DependencyObject GetTemplatedParent() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_FrameworkElement, Snoop.DataAccess.Interfaces.ISO_DependencyObject, PackedArgs_GetTemplatedParent>(this, true, "GetTemplatedParent", new PackedArgs_GetTemplatedParent(){}); }
        public Snoop.DataAccess.Interfaces.ISO_ResourceDictionary GetResources() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_FrameworkElement, Snoop.DataAccess.Interfaces.ISO_ResourceDictionary, PackedArgs_GetResources>(this, true, "GetResources", new PackedArgs_GetResources(){}); }
        public System.Double GetActualHeight() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_FrameworkElement, System.Double, PackedArgs_GetActualHeight>(this, true, "GetActualHeight", new PackedArgs_GetActualHeight(){}); }
        public System.Double GetActualWidth() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_FrameworkElement, System.Double, PackedArgs_GetActualWidth>(this, true, "GetActualWidth", new PackedArgs_GetActualWidth(){}); }
    }
     
    internal sealed class ISO_ImageSourceServer : IExecutor {
        readonly ISO_ImageSource source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public ISO_ImageSourceServer(ISO_ImageSource source){
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
            if(methodName=="GetData") {
                return source.GetData();                 
            }
            return null;
        }
    }
    internal sealed class ISO_ImageSourceClient : Snoop.DataAccess.Interfaces.ISO_ImageSource, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_ImageSourceClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_get_TypeName {
        }
        internal class PackedArgs_get_Source {
        }
        internal class PackedArgs_GetData {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_ImageSource, System.String, PackedArgs_get_TypeName>(this, true, "get_TypeName", new PackedArgs_get_TypeName()); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_ImageSource, System.Object, PackedArgs_get_Source>(this, true, "get_Source", new PackedArgs_get_Source()); }
        }
        public System.Byte[] GetData() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_ImageSource, System.Byte[], PackedArgs_GetData>(this, true, "GetData", new PackedArgs_GetData(){}); }
    }
     
    internal sealed class ISO_ResourceDictionaryServer : IExecutor {
        readonly ISO_ResourceDictionary source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public ISO_ResourceDictionaryServer(ISO_ResourceDictionary source){
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
            if(methodName=="GetKeys") {
                return source.GetKeys();                 
            }
            if(methodName=="GetValues") {
                return source.GetValues();                 
            }
            if(methodName=="GetValue") {
                return source.GetValue(((CallInfo<ISO_ResourceDictionaryClient.PackedArgs_GetValue>)parameters).Args.key);                 
            }
            if(methodName=="GetMergedDictionaries") {
                return source.GetMergedDictionaries();                 
            }
            return null;
        }
    }
    internal sealed class ISO_ResourceDictionaryClient : Snoop.DataAccess.Interfaces.ISO_ResourceDictionary, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_ResourceDictionaryClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_get_TypeName {
        }
        internal class PackedArgs_get_Source {
        }
        internal class PackedArgs_GetKeys {
        }
        internal class PackedArgs_GetValues {
        }
        internal class PackedArgs_GetValue {
        public System.Object key { get; set; }
        }
        internal class PackedArgs_GetMergedDictionaries {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_ResourceDictionary, System.String, PackedArgs_get_TypeName>(this, true, "get_TypeName", new PackedArgs_get_TypeName()); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_ResourceDictionary, System.Object, PackedArgs_get_Source>(this, true, "get_Source", new PackedArgs_get_Source()); }
        }
        public System.Collections.ICollection GetKeys() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_ResourceDictionary, System.Collections.ICollection, PackedArgs_GetKeys>(this, true, "GetKeys", new PackedArgs_GetKeys(){}); }
        public System.Collections.ICollection GetValues() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_ResourceDictionary, System.Collections.ICollection, PackedArgs_GetValues>(this, true, "GetValues", new PackedArgs_GetValues(){}); }
        public System.Object GetValue(System.Object key) { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_ResourceDictionary, System.Object, PackedArgs_GetValue>(this, true, "GetValue", new PackedArgs_GetValue(){key = key}); }
        public System.Collections.Generic.ICollection<Snoop.DataAccess.Interfaces.ISO_ResourceDictionary> GetMergedDictionaries() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_ResourceDictionary, System.Collections.Generic.ICollection<Snoop.DataAccess.Interfaces.ISO_ResourceDictionary>, PackedArgs_GetMergedDictionaries>(this, true, "GetMergedDictionaries", new PackedArgs_GetMergedDictionaries(){}); }
    }
     
    internal sealed class ISO_UIElementServer : IExecutor {
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
            if(methodName=="IsDescendantOf") {
                return source.IsDescendantOf(((CallInfo<ISO_UIElementClient.PackedArgs_IsDescendantOf>)parameters).Args.rootVisual);                 
            }
            if(methodName=="GetSurface") {
                return source.GetSurface();                 
            }
            if(methodName=="GetRenderSize") {
                return source.GetRenderSize();                 
            }
            return null;
        }
    }
    internal sealed class ISO_UIElementClient : Snoop.DataAccess.Interfaces.ISO_UIElement, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_UIElementClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_IsDescendantOf {
        public Snoop.DataAccess.Interfaces.ISO_Visual rootVisual { get; set; }
        }
        internal class PackedArgs_GetSurface {
        }
        internal class PackedArgs_get_TypeName {
        }
        internal class PackedArgs_get_Source {
        }
        internal class PackedArgs_GetRenderSize {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UIElement, System.String, PackedArgs_get_TypeName>(this, true, "get_TypeName", new PackedArgs_get_TypeName()); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UIElement, System.Object, PackedArgs_get_Source>(this, true, "get_Source", new PackedArgs_get_Source()); }
        }
        public System.Boolean IsDescendantOf(Snoop.DataAccess.Interfaces.ISO_Visual rootVisual) { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UIElement, System.Boolean, PackedArgs_IsDescendantOf>(this, true, "IsDescendantOf", new PackedArgs_IsDescendantOf(){rootVisual = rootVisual}); }
        public Snoop.DataAccess.Interfaces.ISO_UISurface GetSurface() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UIElement, Snoop.DataAccess.Interfaces.ISO_UISurface, PackedArgs_GetSurface>(this, true, "GetSurface", new PackedArgs_GetSurface(){}); }
        public System.ValueTuple<System.Double, System.Double> GetRenderSize() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UIElement, System.ValueTuple<System.Double, System.Double>, PackedArgs_GetRenderSize>(this, true, "GetRenderSize", new PackedArgs_GetRenderSize(){}); }
    }
     
    internal sealed class ISO_UISurfaceServer : IExecutor {
        readonly ISO_UISurface source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public ISO_UISurfaceServer(ISO_UISurface source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
            this.eventCounter["Changed"] = 0;
        }
        void OnChanged(){
            Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UISurface, object, ISO_UISurfaceClient.PackedArgs_Changed>(this, false, "RaiseChanged", new ISO_UISurfaceClient.PackedArgs_Changed(){});
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="add_Changed") {
                var count = eventCounter["Changed"];
                if(count==0) {
                    source.Changed += OnChanged;
                }
                eventCounter["Changed"] = count + 1;
            }
            if(methodName=="remove_Changed") {
                var count = eventCounter["Changed"];
                if(count==1) {
                    source.Changed -= OnChanged;
                }
                eventCounter["Changed"] = count - 1;
            }
            if(methodName=="get_TypeName") {
                return source.TypeName;
            }
            if(methodName=="get_Source") {
                return source.Source;
            }
            if(methodName=="GetData") {
                return source.GetData();                 
            }
            return null;
        }
    }
    internal sealed class ISO_UISurfaceClient : Snoop.DataAccess.Interfaces.ISO_UISurface, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_UISurfaceClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_get_TypeName {
        }
        internal class PackedArgs_get_Source {
        }
        internal class PackedArgs_GetData {
        }
        internal class PackedArgs_Changed {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UISurface, System.String, PackedArgs_get_TypeName>(this, true, "get_TypeName", new PackedArgs_get_TypeName()); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UISurface, System.Object, PackedArgs_get_Source>(this, true, "get_Source", new PackedArgs_get_Source()); }
        }
        public System.Byte[] GetData() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UISurface, System.Byte[], PackedArgs_GetData>(this, true, "GetData", new PackedArgs_GetData(){}); }
    System.Action _Changed;
        public event System.Action Changed {
            add {  
                 _Changed += value;
                 Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UISurface, object, PackedArgs_UnusedArgs>(this, false, "add_Changed", new PackedArgs_UnusedArgs()); 
            }
            remove {
                 _Changed -= value;
                 Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_UISurface, object, PackedArgs_UnusedArgs>(this, false, "remove_Changed", new PackedArgs_UnusedArgs());
            }
        }
        public void RaiseChanged() {
             _Changed();
        }
    }
     
    internal sealed class ISO_VisualServer : IExecutor {
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
            if(methodName=="IsDescendantOf") {
                return source.IsDescendantOf(((CallInfo<ISO_VisualClient.PackedArgs_IsDescendantOf>)parameters).Args.rootVisual);                 
            }
            if(methodName=="GetSurface") {
                return source.GetSurface();                 
            }
            return null;
        }
    }
    internal sealed class ISO_VisualClient : Snoop.DataAccess.Interfaces.ISO_Visual, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_VisualClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_get_TypeName {
        }
        internal class PackedArgs_get_Source {
        }
        internal class PackedArgs_IsDescendantOf {
        public Snoop.DataAccess.Interfaces.ISO_Visual rootVisual { get; set; }
        }
        internal class PackedArgs_GetSurface {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Visual, System.String, PackedArgs_get_TypeName>(this, true, "get_TypeName", new PackedArgs_get_TypeName()); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Visual, System.Object, PackedArgs_get_Source>(this, true, "get_Source", new PackedArgs_get_Source()); }
        }
        public System.Boolean IsDescendantOf(Snoop.DataAccess.Interfaces.ISO_Visual rootVisual) { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Visual, System.Boolean, PackedArgs_IsDescendantOf>(this, true, "IsDescendantOf", new PackedArgs_IsDescendantOf(){rootVisual = rootVisual}); }
        public Snoop.DataAccess.Interfaces.ISO_UISurface GetSurface() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Visual, Snoop.DataAccess.Interfaces.ISO_UISurface, PackedArgs_GetSurface>(this, true, "GetSurface", new PackedArgs_GetSurface(){}); }
    }
     
    internal sealed class ISO_Visual3DServer : IExecutor {
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
    internal sealed class ISO_Visual3DClient : Snoop.DataAccess.Interfaces.ISO_Visual3D, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_Visual3DClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_get_TypeName {
        }
        internal class PackedArgs_get_Source {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Visual3D, System.String, PackedArgs_get_TypeName>(this, true, "get_TypeName", new PackedArgs_get_TypeName()); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Visual3D, System.Object, PackedArgs_get_Source>(this, true, "get_Source", new PackedArgs_get_Source()); }
        }
    }
     
    internal sealed class ISO_WindowServer : IExecutor {
        readonly ISO_Window source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public ISO_WindowServer(ISO_Window source){
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
            if(methodName=="GetTemplatedParent") {
                return source.GetTemplatedParent();                 
            }
            if(methodName=="GetResources") {
                return source.GetResources();                 
            }
            if(methodName=="GetActualHeight") {
                return source.GetActualHeight();                 
            }
            if(methodName=="GetActualWidth") {
                return source.GetActualWidth();                 
            }
            if(methodName=="GetRenderSize") {
                return source.GetRenderSize();                 
            }
            if(methodName=="IsDescendantOf") {
                return source.IsDescendantOf(((CallInfo<ISO_WindowClient.PackedArgs_IsDescendantOf>)parameters).Args.rootVisual);                 
            }
            if(methodName=="GetSurface") {
                return source.GetSurface();                 
            }
            if(methodName=="GetTitle") {
                return source.GetTitle();                 
            }
            return null;
        }
    }
    internal sealed class ISO_WindowClient : Snoop.DataAccess.Interfaces.ISO_Window, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public ISO_WindowClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_GetTemplatedParent {
        }
        internal class PackedArgs_GetResources {
        }
        internal class PackedArgs_GetActualHeight {
        }
        internal class PackedArgs_GetActualWidth {
        }
        internal class PackedArgs_GetRenderSize {
        }
        internal class PackedArgs_IsDescendantOf {
        public Snoop.DataAccess.Interfaces.ISO_Visual rootVisual { get; set; }
        }
        internal class PackedArgs_GetSurface {
        }
        internal class PackedArgs_get_TypeName {
        }
        internal class PackedArgs_get_Source {
        }
        internal class PackedArgs_GetTitle {
        }
        public System.String TypeName {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, System.String, PackedArgs_get_TypeName>(this, true, "get_TypeName", new PackedArgs_get_TypeName()); }
        }
        public System.Object Source {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, System.Object, PackedArgs_get_Source>(this, true, "get_Source", new PackedArgs_get_Source()); }
        }
        public Snoop.DataAccess.Interfaces.ISO_DependencyObject GetTemplatedParent() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, Snoop.DataAccess.Interfaces.ISO_DependencyObject, PackedArgs_GetTemplatedParent>(this, true, "GetTemplatedParent", new PackedArgs_GetTemplatedParent(){}); }
        public Snoop.DataAccess.Interfaces.ISO_ResourceDictionary GetResources() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, Snoop.DataAccess.Interfaces.ISO_ResourceDictionary, PackedArgs_GetResources>(this, true, "GetResources", new PackedArgs_GetResources(){}); }
        public System.Double GetActualHeight() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, System.Double, PackedArgs_GetActualHeight>(this, true, "GetActualHeight", new PackedArgs_GetActualHeight(){}); }
        public System.Double GetActualWidth() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, System.Double, PackedArgs_GetActualWidth>(this, true, "GetActualWidth", new PackedArgs_GetActualWidth(){}); }
        public System.ValueTuple<System.Double, System.Double> GetRenderSize() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, System.ValueTuple<System.Double, System.Double>, PackedArgs_GetRenderSize>(this, true, "GetRenderSize", new PackedArgs_GetRenderSize(){}); }
        public System.Boolean IsDescendantOf(Snoop.DataAccess.Interfaces.ISO_Visual rootVisual) { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, System.Boolean, PackedArgs_IsDescendantOf>(this, true, "IsDescendantOf", new PackedArgs_IsDescendantOf(){rootVisual = rootVisual}); }
        public Snoop.DataAccess.Interfaces.ISO_UISurface GetSurface() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, Snoop.DataAccess.Interfaces.ISO_UISurface, PackedArgs_GetSurface>(this, true, "GetSurface", new PackedArgs_GetSurface(){}); }
        public System.String GetTitle() { return Marshaller.Call<Snoop.DataAccess.Interfaces.ISO_Window, System.String, PackedArgs_GetTitle>(this, true, "GetTitle", new PackedArgs_GetTitle(){}); }
    }
     
    internal sealed class IDAS_CurrentApplicationServer : IExecutor {
        readonly IDAS_CurrentApplication source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IDAS_CurrentApplicationServer(IDAS_CurrentApplication source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="get_Resources") {
                return source.Resources;
            }
            return null;
        }
    }
    internal sealed class IDAS_CurrentApplicationClient : Snoop.DataAccess.Interfaces.IDAS_CurrentApplication, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IDAS_CurrentApplicationClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_get_Resources {
        }
        public Snoop.DataAccess.Interfaces.ISO_ResourceDictionary Resources {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_CurrentApplication, Snoop.DataAccess.Interfaces.ISO_ResourceDictionary, PackedArgs_get_Resources>(this, true, "get_Resources", new PackedArgs_get_Resources()); }
        }
    }
     
    internal sealed class IDAS_InputManagerServer : IExecutor {
        readonly IDAS_InputManager source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IDAS_InputManagerServer(IDAS_InputManager source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
            this.eventCounter["PreProcessInput"] = 0;
        }
        void OnPreProcessInput(){
            Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_InputManager, object, IDAS_InputManagerClient.PackedArgs_PreProcessInput>(this, false, "RaisePreProcessInput", new IDAS_InputManagerClient.PackedArgs_PreProcessInput(){});
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
    internal sealed class IDAS_InputManagerClient : Snoop.DataAccess.Interfaces.IDAS_InputManager, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IDAS_InputManagerClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_PreProcessInput {
        }
    System.Action _PreProcessInput;
        public event System.Action PreProcessInput {
            add {  
                 _PreProcessInput += value;
                 Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_InputManager, object, PackedArgs_UnusedArgs>(this, false, "add_PreProcessInput", new PackedArgs_UnusedArgs()); 
            }
            remove {
                 _PreProcessInput -= value;
                 Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_InputManager, object, PackedArgs_UnusedArgs>(this, false, "remove_PreProcessInput", new PackedArgs_UnusedArgs());
            }
        }
        public void RaisePreProcessInput() {
             _PreProcessInput();
        }
    }
     
    internal sealed class IDAS_MouseServer : IExecutor {
        readonly IDAS_Mouse source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IDAS_MouseServer(IDAS_Mouse source){
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
    internal sealed class IDAS_MouseClient : Snoop.DataAccess.Interfaces.IDAS_Mouse, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IDAS_MouseClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_get_DirectlyOver {
        }
        public Snoop.DataAccess.Interfaces.ISnoopObject DirectlyOver {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_Mouse, Snoop.DataAccess.Interfaces.ISnoopObject, PackedArgs_get_DirectlyOver>(this, true, "get_DirectlyOver", new PackedArgs_get_DirectlyOver()); }
        }
    }
     
    internal sealed class IDAS_RootProviderServer : IExecutor {
        readonly IDAS_RootProvider source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IDAS_RootProviderServer(IDAS_RootProvider source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="get_Root") {
                return source.Root;
            }
            if(methodName=="RootFrom") {
                return source.RootFrom(((CallInfo<IDAS_RootProviderClient.PackedArgs_RootFrom>)parameters).Args.source);                 
            }
            return null;
        }
    }
    internal sealed class IDAS_RootProviderClient : Snoop.DataAccess.Interfaces.IDAS_RootProvider, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IDAS_RootProviderClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_get_Root {
        }
        internal class PackedArgs_RootFrom {
        public Snoop.DataAccess.Interfaces.ISO_Visual source { get; set; }
        }
        public Snoop.DataAccess.Interfaces.ISnoopObject Root {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_RootProvider, Snoop.DataAccess.Interfaces.ISnoopObject, PackedArgs_get_Root>(this, true, "get_Root", new PackedArgs_get_Root()); }
        }
        public Snoop.DataAccess.Interfaces.ISnoopObject RootFrom(Snoop.DataAccess.Interfaces.ISO_Visual source) { return Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_RootProvider, Snoop.DataAccess.Interfaces.ISnoopObject, PackedArgs_RootFrom>(this, true, "RootFrom", new PackedArgs_RootFrom(){source = source}); }
    }
     
    internal sealed class IDAS_VisualTreeHelperServer : IExecutor {
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
    internal sealed class IDAS_VisualTreeHelperClient : Snoop.DataAccess.Interfaces.IDAS_VisualTreeHelper, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IDAS_VisualTreeHelperClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
    }
     
    internal sealed class IDAS_WindowHelperServer : IExecutor {
        readonly IDAS_WindowHelper source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IDAS_WindowHelperServer(IDAS_WindowHelper source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="GetVisibleWindow") {
                return source.GetVisibleWindow(((CallInfo<IDAS_WindowHelperClient.PackedArgs_GetVisibleWindow>)parameters).Args.hwnd);                 
            }
            return null;
        }
    }
    internal sealed class IDAS_WindowHelperClient : Snoop.DataAccess.Interfaces.IDAS_WindowHelper, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IDAS_WindowHelperClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_GetVisibleWindow {
        public System.Int64 hwnd { get; set; }
        }
        public Snoop.DataAccess.Interfaces.ISO_Window GetVisibleWindow(System.Int64 hwnd) { return Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_WindowHelper, Snoop.DataAccess.Interfaces.ISO_Window, PackedArgs_GetVisibleWindow>(this, true, "GetVisibleWindow", new PackedArgs_GetVisibleWindow(){hwnd = hwnd}); }
    }
     
    internal sealed class IDAS_WindowInfoServer : IExecutor {
        readonly IDAS_WindowInfo source;
        public string Id { get; }
        readonly Dictionary<string, int> eventCounter; 
        public IDAS_WindowInfoServer(IDAS_WindowInfo source){
            this.source = source;
            this.Id = source.Id;
            this.eventCounter = new Dictionary<string, int>();
        }

        public object Execute(string methodName, ICallInfo parameters) {
            if(methodName=="GetIsValidProcess") {
                return source.GetIsValidProcess(((CallInfo<IDAS_WindowInfoClient.PackedArgs_GetIsValidProcess>)parameters).Args.hwnd);                 
            }
            return null;
        }
    }
    internal sealed class IDAS_WindowInfoClient : Snoop.DataAccess.Interfaces.IDAS_WindowInfo, IDataAccessClient {
        readonly string id;
        public ISession Session {get; set;}
        public IDAS_WindowInfoClient(ISession session, string id) {
            this.id = id;
            Session = session;
        }
        public string Id { get { return id; } }
    internal class PackedArgs_UnusedArgs {
    }
        internal class PackedArgs_GetIsValidProcess {
        public System.IntPtr hwnd { get; set; }
        }
        public System.Boolean GetIsValidProcess(System.IntPtr hwnd) { return Marshaller.Call<Snoop.DataAccess.Interfaces.IDAS_WindowInfo, System.Boolean, PackedArgs_GetIsValidProcess>(this, true, "GetIsValidProcess", new PackedArgs_GetIsValidProcess(){hwnd = hwnd}); }
    }
}