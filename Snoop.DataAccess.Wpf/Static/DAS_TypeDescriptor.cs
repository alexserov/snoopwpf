namespace Snoop.DataAccess.Wpf {
    using System;
    using System.Collections;
    using System.ComponentModel;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;

    public class DAS_TypeDescriptor : DataAccess, IDAS_TypeDescriptor{
        public IEnumerable GetProperties(Type type, Attribute[] attributes) {
            foreach (var element in TypeDescriptor.GetProperties(attributes)) {
                yield return SnoopObjectBase.Create(element);
            }
        }

        public ISO_PropertyDescriptor GetDependencyPropertyDescriptor(ISO_PropertyDescriptor property) {
            return (ISO_PropertyDescriptor)SnoopObjectBase.Create(DependencyPropertyDescriptor.FromProperty(property.Source as PropertyDescriptor));
        }
    }
}