namespace Snoop.DataAccess.Interfaces {
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IDAS_InjectorLibraryPath : IDataAccessStatic {
        string GetPath();
    }
}