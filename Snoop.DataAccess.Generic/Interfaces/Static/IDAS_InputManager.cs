namespace Snoop.DataAccess.Interfaces {
    using System;
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IDAS_InputManager : IDataAccessStatic{
        event Action PreProcessInput;
    }
}