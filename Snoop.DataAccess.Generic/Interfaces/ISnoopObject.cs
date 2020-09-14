namespace Snoop.DataAccess.Interfaces
{
    using Snoop.DataAccess.Internal.Interfaces;

    public interface ISnoopObject : IDataAccess
    {
        string TypeName { get; }
    }
}