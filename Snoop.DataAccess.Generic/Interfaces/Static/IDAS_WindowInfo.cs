namespace Snoop.DataAccess.Interfaces
{
    using System;
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IDAS_WindowInfo : IDataAccessStatic
    {
        bool GetIsValidProcess(IntPtr hwnd);
    }
}