namespace Snoop.DataAccess.Wpf {
    using System;
    using System.Windows;
    using System.Windows.Media;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public abstract class SnoopObjectBase : DataAccessBase, ISnoopObject {

        protected SnoopObjectBase(object source) {
            this.TypeName = source.GetType().FullName;
            this.Source = source;
        }

        public virtual string TypeName { get; }
        public object Source { get; }

        public static ISnoopObject Create(object source) {
            switch (source) {
                case Window window:
                    return new SO_Window(window);
                case FrameworkElement frameworkElement:
                    return new SO_FrameworkElement(frameworkElement);
                case Visual visual:
                    return new SO_Visual(visual);
                case DependencyObject dependencyObject:
                    return new SO_DependencyObject(dependencyObject);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
