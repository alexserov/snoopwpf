namespace Snoop.DataAccess.Internal.Interfaces
{
    using Snoop.DataAccess.Sessions;

    public interface IDataAccess
    {
        
    }

    interface IDataAccessInternal {
        IExtension Extension { get; set; }
    }

    public class DataAccess : IDataAccessInternal {
        IExtension IDataAccessInternal.Extension { get; set; }
    }
}