namespace Snoop.DataAccess.Internal.Interfaces {
    using Newtonsoft.Json;

    public interface ISnoopObjectInternal : IDataAccess{
        [JsonIgnore]
        object Source { get; }
    }
}