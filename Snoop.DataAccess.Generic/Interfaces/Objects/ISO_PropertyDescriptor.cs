namespace Snoop.DataAccess.Interfaces {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface ISO_PropertyDescriptor : ISnoopObject{
        bool IsReadOnly { get; }
        AttributeCollection Attributes { get; }
        string Name { get; }
        string DisplayName { get; }
        string BindingError { get; }
        ISO_ValueSource GetValueSource(ISnoopObject source);
        Type PropertyType { get; }
        Type ComponentType { get; }
        void Clear();
        ISO_Binding GetBinding(ISnoopObject target);
        void SetValue(ISnoopObject target, string value);
        ISnoopObject GetValue(ISnoopObject target);
    }
}