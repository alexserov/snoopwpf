namespace Snoop.DataAccess.WinUI3 {
    using System;
    using System.Collections;
    using System.ComponentModel;
    using Microsoft.UI.Xaml;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;

    public class DAS_TypeDescriptor : DataAccess, IDAS_TypeDescriptor{
        public IEnumerable GetProperties(Type type, Attribute[] attributes) {
            foreach (PropertyDescriptor element in TypeDescriptor.GetProperties(type, attributes)) {
                if(element.Name=="Dispatcher")
                    continue;
                if (element.Name == "Interactions" && element.ComponentType == typeof(UIElement))
                    continue;
                yield return SnoopObjectBase.Create(element);
            }
        }

        public ISO_PropertyDescriptor GetDependencyPropertyDescriptor(ISO_PropertyDescriptor property) {
            return (ISO_PropertyDescriptor)SnoopObjectBase.Create(DependencyPropertyDescriptor.FromProperty(property.Source as PropertyDescriptor));
        }
    }
}