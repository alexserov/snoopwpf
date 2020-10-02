namespace Snoop.DataAccess.Interfaces {
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IDAS_TreeHelper : IDataAccessStatic {
        int GetChildrenCount(ISnoopObject dependencyObject);
        ISnoopObject GetChild(ISnoopObject dependencyObject, int i);
        ISnoopObject GetParent(ISnoopObject itemToFind);
    }
}