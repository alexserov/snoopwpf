namespace Snoop.DataAccess.Sessions
{
    using System;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;

    public interface ISession
    {
        string Send(string value);
    }
    public class Session
    {
        static ISession Current
        {
            get
            {
                throw  new InvalidOperationException();
            }
        }
        public static string Send(string value)
        {
            return Current.Send(value);
        }

        public static void Register(IDataAccess element)
        {
            
        }
    }
}