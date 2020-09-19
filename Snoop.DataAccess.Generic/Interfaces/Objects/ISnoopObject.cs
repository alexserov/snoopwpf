namespace Snoop.DataAccess.Interfaces
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Snoop.DataAccess.Impl;
    using Snoop.DataAccess.Internal.Interfaces;
    using Snoop.DataAccess.Sessions;

    [JsonConverter(typeof(SnoopObjectJsonConverter))]
    public interface ISnoopObject : ISnoopObjectInternal
    {
        string TypeName { get; }

    }

    public class SnoopObjectJsonConverter : JsonConverter {
        /*
         *       public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
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
         */
        public override bool CanWrite { get { return false; } }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { throw new NotImplementedException(); }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var jo = JObject.Load(reader);
            return Server.Current.FindRegistered(jo.Property("Id").Value.ToString());
        }
        public override bool CanConvert(Type objectType) { return typeof(ISnoopObject).IsAssignableFrom(objectType);}
    }
    
}