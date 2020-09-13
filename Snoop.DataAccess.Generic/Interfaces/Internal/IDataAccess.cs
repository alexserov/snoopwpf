namespace Snoop.DataAccess.Internal.Interfaces
{
    using Snoop.DataAccess.Sessions;

    public interface IDataAccess
    {
        public string Id { get; }
    }

    public interface IDataAccessClient
    {
        ISession Session { get; set; }
    }
}