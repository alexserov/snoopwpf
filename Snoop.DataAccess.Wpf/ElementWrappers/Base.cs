namespace Snoop.DataAccess.Wpf {
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;
    using Snoop.DataAccess.Sessions;

    public abstract class SnoopObjectBase : DataAccess, ISnoopObject{

        protected SnoopObjectBase(object source) {
            this.TypeName = source.GetType().FullName;
            this.Source = source;
        }

        public virtual string TypeName { get; }

        public virtual Type DataAccessType { get { return typeof(ISnoopObject); } }

        public object Source { get; }

        public static ISnoopObject Create(object source) {
            if (source == null)
                return null;
            var result = CreateImpl(source);
            Extension.Patch(result);
            return result;
        }

        static ISnoopObject CreateImpl(object source) {
            switch (source) {
                case Binding descriptor:
                    return new SO_Binding(descriptor);
                case ValueSource descriptor:
                    return new SO_ValueSource(descriptor);
                case PropertyDescriptor descriptor:
                    return new SO_PropertyDescriptor(descriptor);
                case Window window:
                    return new SO_Window(window);
                case FrameworkElement frameworkElement:
                    return new SO_FrameworkElement(frameworkElement);
                case UIElement uiElement:
                    return new SO_UIElement(uiElement);
                case Visual visual:
                    return new SO_Visual(visual);
                case Brush brush:
                    return new SO_Brush(brush);
                case ImageSource imageSource:
                    return  new SO_ImageSource(imageSource);
                case ResourceDictionary resourceDictionary:
                    return new SO_ResourceDictionary(resourceDictionary);
                case DependencyObject dependencyObject:
                    return new SO_DependencyObject(dependencyObject);
                default:
                    return new SO_Dumb(source);
            }
        }
    }

    public static class SnoopObjectExtensions {
        public static T UW<T>(this ISnoopObject source) where T : class {
            return ((SnoopObjectBase)source).Source as T;
        }
    }
}
