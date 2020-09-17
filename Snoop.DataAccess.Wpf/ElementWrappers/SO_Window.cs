namespace Snoop.DataAccess.Wpf {
    using System.Windows;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;


    public class SO_Window : SO_FrameworkElement, ISO_Window {
        readonly Window window;

        public SO_Window(Window window) : base(window) { this.window = window; }

        public string Title {
            get { return this.window.Title; }
            set { this.window.Title = value; }
        }
    }
}