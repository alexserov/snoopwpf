namespace Snoop.DataAccess.Sessions {
    using System;
    using Snoop.DataAccess.Internal.Interfaces;

    public class DataAccessBase : IDataAccess{
        public string Id { get; }

        public DataAccessBase() {
            Id = Guid.NewGuid().ToString();
        }
    }
}