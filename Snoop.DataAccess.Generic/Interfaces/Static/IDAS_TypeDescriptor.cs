namespace Snoop.DataAccess.Interfaces {
    using System;
    using System.Collections;
    using System.ComponentModel;
    using Snoop.DataAccess.Internal.Interfaces;

    public interface IDAS_TypeDescriptor : IDataAccessStatic {
        IEnumerable GetProperties(Type type, Attribute[] attributes);
        ISO_PropertyDescriptor GetDependencyPropertyDescriptor(ISO_PropertyDescriptor property);
    }
}