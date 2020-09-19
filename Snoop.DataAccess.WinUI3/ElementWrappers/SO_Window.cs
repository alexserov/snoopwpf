namespace Snoop.DataAccess.WinUI3 {
    using System;
    using Microsoft.UI.Xaml;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;


    public class SO_Window : SnoopObjectBase, ISO_Window {
        public override Type DataAccessType {
            get { return typeof(ISO_Window); }
        }
        readonly Window window;

        public SO_Window(Window window) : base(window) { this.window = window; }

        public string GetTitle() { return this.window.OnUI(x => x.Title); }
        public bool IsDescendantOf(ISO_Visual rootVisual) { return false;}
        public ISO_UISurface GetSurface() { throw new NotImplementedException(); }
        public (double Width, double Height) GetRenderSize() { return this.window.OnUI(x => (x.Bounds.Width, x.Bounds.Height));}
        public ISO_DependencyObject GetTemplatedParent() { return null; }
        public ISO_ResourceDictionary GetResources() { return null;}
        public double GetActualHeight() { return this.GetRenderSize().Height;}
        public double GetActualWidth() { return this.GetRenderSize().Width;}
        public string GetName() { return ""; }
    }
}