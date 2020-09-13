namespace Snoop.DataAccess.Internal.Interfaces
{
    public interface IExecutor
    {
        object Execute(string methodName, ICallInfo parameters);
    }
    public interface ICallInfo
    {
        string CallerId { get; set; }
        string Method { get; set; }
    }
}