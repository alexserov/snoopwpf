namespace Snoop.DataAccess.Interfaces
{
    using System;
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IDAS_WindowInfoStatic : IDataAccessStatic
    {
        bool GetIsValidProcess(IntPtr hwnd);
    }
}