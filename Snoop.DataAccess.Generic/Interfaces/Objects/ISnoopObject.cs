namespace Snoop.DataAccess.Interfaces
{
    using Snoop.DataAccess.Internal.Interfaces;

    public interface ISnoopObject : ISnoopObjectInternal
    {
        string TypeName { get; }

    }
}