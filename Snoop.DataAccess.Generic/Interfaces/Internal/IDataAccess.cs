namespace Snoop.DataAccess.Internal.Interfaces
{
    using System;
    using Snoop.DataAccess.Sessions;

    public interface IDataAccess
    {
        public string Id { get; }
    }

    public interface IDataAccessServer {
        public Type DataAccessType { get; }
    }

    public interface IDataAccessClient
    {
        ISession Session { get; set; }
    }
}