namespace Snoop.DataAccess.Interfaces
{
    using System;
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IDAS_WindowHelperStatic : IDataAccessStatic
    {
        ISO_Window GetVisibleWindow(long hwnd);
    }
}