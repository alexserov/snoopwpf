namespace Snoop.DataAccess.Internal.Interfaces
{
    public interface IExecutor : IDataAccess
    {
        object Execute(string methodName, ICallInfo parameters);
    }
    public interface ICallInfo
    {
        string CallerId { get; set; }
        string Method { get; set; }
    }
}