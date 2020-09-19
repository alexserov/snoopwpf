namespace Snoop.DataAccess.Interfaces
{
    using System;
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IDAS_WindowHelper : IDataAccessStatic
    {
        ISO_Window GetVisibleWindow(long hwnd);
    }
}