namespace Snoop.DataAccess.Generic
{
    using System.IO;
    using System.Text;
    using Newtonsoft.Json;

    public class Message
    {
        public string Kind { get; set; }
        public string Json { get; set; }

        public string Save()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Message Restore(byte[] data)
        {
            return JsonConvert.DeserializeObject<Message>(Encoding.UTF8.GetString(data));
        }
    }
}