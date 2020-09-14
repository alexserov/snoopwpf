namespace Snoop.DataAccess.Interfaces
{
    using System;
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IWindowHelper : IDataAccessStatic
    {
        IWindowInstance GetVisibleWindow(long hwnd);
    }
}