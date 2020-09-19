namespace Snoop.DataAccess.Interfaces {
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IDAS_CurrentApplication : IDataAccessStatic {
        ISO_ResourceDictionary Resources { get; }
    }
}