namespace Snoop.DataAccess.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Snoop.DataAccess.Internal.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class CallInfo<TPackedArgs> : ICallInfo
    {
        public CallInfo()
        {
                
        }
        public CallInfo(IDataAccess caller, string method, TPackedArgs args)
        {
            this.CallerId = caller.Id;
            this.Method = method;
            this.Args = args;
        }

        public string CallerId { get; set; }
        public string Method { get; set; }
        [JsonRequired]
        public TPackedArgs Args { get; set; }
    }
    
    partial class Marshaller {
        static List<(Type tInterface, Func<object, IExecutor> factoryServer, Func<ISession, string, IDataAccess> factoryClient)> registeredTypes = null;
        public static IExecutor CreateServerExecutor(Type type, object instance) {
            var info = registeredTypes.FirstOrDefault(x => x.tInterface == type);
            return info.factoryServer(instance);
        }
        public static IDataAccess CreateClientExecutor(ISession session, Type type, string id) {
            var info = registeredTypes.FirstOrDefault(x => x.tInterface == type);
            return info.factoryClient(session, id);
        }
      

        class CallInfoGenericJsonConverter : JsonConverter
        {

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var vi = (CallInfoGeneric)value;
                var obj = new JObject();
                obj.AddFirst(new JProperty("TypeName", vi.TypeName));
                serializer.NullValueHandling = NullValueHandling.Ignore;
                obj.Add("Info", JToken.FromObject(vi.Info, serializer));
                obj.WriteTo(writer);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var ci = new CallInfoGeneric();
                var jo = JObject.Load(reader);
                ci.TypeName = jo.Property("TypeName").Value.ToString();
                var type = typeof(Marshaller).Assembly.GetType(ci.TypeName);
                ci.Info = (ICallInfo)jo.Property("Info").Value.ToObject(typeof(CallInfo<>).MakeGenericType(type));
                return ci;
            }

            public override bool CanConvert(Type objectType)
            {
                return true;
            }
        }
        [JsonConverter(typeof(CallInfoGenericJsonConverter))]
        class CallInfoGeneric
        {
            public string TypeName { get; set; }
            [JsonRequired]
            public ICallInfo Info { get; set; }
        }

        public static TResult Call<TCaller, TResult, TPackedArgs>(IDataAccess caller, bool wait, string method, TPackedArgs args) where TCaller : IDataAccess
        {
            // JsonConvert.SerializeObject()
            var ci = new CallInfoGeneric() { TypeName = typeof(TPackedArgs).FullName, Info = new CallInfo<TPackedArgs>(caller, method, args) };
            string data = JsonConvert.SerializeObject(ci);
            // ReSharper disable once SuspiciousTypeConversion.Global
            var result = ((IDataAccessClient)caller).Session.Send(data, "process", wait);
            return Unwrap<TResult>(caller, result);
        }

        public static TResult Unwrap<TResult>(IDataAccess caller, string source) {
            if (source == null)
                return default;
            if (typeof(IDataAccess).IsAssignableFrom(typeof(TResult))) {
                // ReSharper disable once SuspiciousTypeConversion.Global
                return (TResult)CreateClientExecutor(((IDataAccessClient)caller).Session, typeof(TResult), JObject.Parse(source).Property("Id").Value.ToString());
            }
            return JsonConvert.DeserializeObject<TResult>(source);
        }

        public static string Process(Server server, string value)
        {
            var ci = JsonConvert.DeserializeObject<CallInfoGeneric>(value);

            var executor = server.GetExecutor(ci.Info.CallerId);
            return JsonConvert.SerializeObject(executor.Execute(ci.Info.Method, ci.Info));
        }
    }
}