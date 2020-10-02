namespace Snoop.DataAccess.Interfaces {
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IDAS_AdornerService : IDataAccessStatic {
        ISnoopObject HighlightedElement { get; set; }
    }
}