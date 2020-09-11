using System;
using Snoop.DataAccess.Interfaces;
using Snoop.DataAccess.Sessions;
// ReSharper disable HeapView.BoxingAllocation

namespace Snoop.DataAccess.Impl {

     
    sealed class IWindowInfoServer {
        readonly IWindowInfo source;
        readonly string id;
        public IWindowInfoServer(IWindowInfo source){
            this.source = source;
            this.id = Guid.NewGuid().ToString();
        }
        public object Execute(string methodName, string[] parameters) {
            if(methodName=="get_IsValidProcess") {
                return source.IsValidProcess;
            }
            if(methodName=="set_IsValidProcess") {
                source.IsValidProcess = Marshaller.Unwrap<System.Boolean>(parameters[0]);
                return null;
            }
            if(methodName=="InvokeSomething") {
                return source.InvokeSomething(Marshaller.Unwrap<System.Boolean>(parameters[0]), Marshaller.Unwrap<System.Int32>(parameters[1]));                 
            }
            if(methodName=="voidMethod") {
                source.voidMethod(Marshaller.Unwrap<System.Boolean>(parameters[0]));
                return null;                 
            }
            if(methodName=="objm") {
                return source.objm();                 
            }
            return null;
        }
    }
    sealed class IWindowInfoClient : Snoop.DataAccess.Interfaces.IWindowInfo {
        readonly string id;
        public IWindowInfoClient(string id) {
            this.id = id;
        }
        public string Id { get { return id; } }
        public System.Boolean IsValidProcess {
            get { return Marshaller.Call<Snoop.DataAccess.Interfaces.IWindowInfo, System.Boolean>(this, "get_IsValidProcess"); }
            set { Marshaller.Call<Snoop.DataAccess.Interfaces.IWindowInfo, System.Boolean>(this, "set_IsValidProcess", value); }
        }
        public System.String InvokeSomething(System.Boolean valA, System.Int32 val2) { return Marshaller.Call<Snoop.DataAccess.Interfaces.IWindowInfo, System.String>(this, "InvokeSomething", valA, val2); }
        public void voidMethod(System.Boolean param1) { Marshaller.Call<Snoop.DataAccess.Interfaces.IWindowInfo, System.Object>(this, "voidMethod", param1); }
        public System.Object objm() { return Marshaller.Call<Snoop.DataAccess.Interfaces.IWindowInfo, System.Object>(this, "objm"); }
    }
}