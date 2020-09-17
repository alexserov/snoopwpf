namespace Snoop.DataAccess.Interfaces {
    using System;
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IDAS_InputManagerStatic : IDataAccessStatic{
        event Action PreProcessInput;
    }
}