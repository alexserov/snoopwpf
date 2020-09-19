namespace Snoop.DataAccess.Interfaces {
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IDAS_Mouse : IDataAccessStatic{
        ISnoopObject DirectlyOver { get; }
    }
}