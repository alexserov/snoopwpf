namespace Snoop.DataAccess.Interfaces {
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IDAS_RootProvider : IDataAccessStatic{
        ISnoopObject Root { get; }
        ISnoopObject RootFrom(ISO_Visual source);
    }
}