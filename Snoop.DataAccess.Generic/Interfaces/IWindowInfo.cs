namespace Snoop.DataAccess.Interfaces
{
    using System;
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IWindowInfo : IDataAccessStatic
    {
        bool GetIsValidProcess(IntPtr hwnd);
    }

    public interface IFakeInterface : IDataAccessStatic
    {
        bool DoSomethingIllegal(IFakeInterface2 element, bool value, string hello);
    }
    public interface IFakeInterface2 : IDataAccessStatic
    {
    }
}