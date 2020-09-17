namespace Snoop.DataAccess.Interfaces
{
    using Newtonsoft.Json;
    using Snoop.DataAccess.Internal.Interfaces;

    public interface ISnoopObject : IDataAccess
    {
        string TypeName { get; }
        [JsonIgnore]
        object Source { get; }
    }
}