namespace Snoop.DataAccess.Interfaces
{
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IWindowInfo : IDataAccess
    {
        bool IsValidProcess { get; set; }
        string InvokeSomething(bool valA, int val2);
        void voidMethod(bool param1);
        object objm();
    }
}