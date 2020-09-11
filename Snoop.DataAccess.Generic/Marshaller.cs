namespace Snoop.DataAccess.Sessions
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using Newtonsoft.Json;

    public class Marshaller
    {
        public static TResult Call<TCaller, TResult>(TCaller caller, string method, params object[] args)
        {
            // JsonConvert.SerializeObject()
            string data = null;
            var result = Session.Send(data);
            throw new NotImplementedException();
        }

        public static TResult Unwrap<TResult>(string source)
        {
            throw new NotImplementedException();
        }
    }
}