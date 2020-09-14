namespace Snoop.DataAccess.Wpf {
    using System.Windows;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public abstract class SnoopObjectBase : DataAccessBase, ISnoopObject {

        public SnoopObjectBase(object source) { this.TypeName = source.GetType().FullName; }
        public virtual string TypeName { get; }
    }
    public class WindowInstance : SnoopObjectBase, IWindowInstance {
        readonly Window window;

        public WindowInstance(Window window) : base(window) { this.window = window; }

        public string Title {
            get { return this.window.Title; }
            set { this.window.Title = value; }
        }
    }
}