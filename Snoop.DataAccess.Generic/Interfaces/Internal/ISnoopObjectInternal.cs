namespace Snoop.DataAccess.Internal.Interfaces {

    public interface ISnoopObjectInternal : IDataAccess{
        object Source { get; }
    }
}