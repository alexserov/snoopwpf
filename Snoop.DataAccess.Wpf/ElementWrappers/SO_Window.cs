namespace Snoop.DataAccess.Wpf {
    using System;
    using System.Windows;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;


    public class SO_Window : SO_FrameworkElement, ISO_Window {
        public override Type DataAccessType {
            get { return typeof(ISO_Window); }
        }
        readonly Window window;

        public SO_Window(Window window) : base(window) { this.window = window; }

        public string GetTitle() { return this.window.OnUI(x => x.Title); }
    }
}