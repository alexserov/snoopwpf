namespace Snoop.DataAccess.Interfaces {
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IDAS_MouseStatic : IDataAccessStatic{
        ISnoopObject DirectlyOver { get; }
    }
}