namespace Snoop.DataAccess.Interfaces {
    using System;

    public interface ISO_UISurface : ISnoopObject {
        byte[] GetData();
        event Action Changed;
    }
}