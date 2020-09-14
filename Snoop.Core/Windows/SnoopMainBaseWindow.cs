namespace Snoop.Windows
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Forms.Integration;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;
    using Snoop.Infrastructure;

    public abstract class SnoopMainBaseWindow : SnoopBaseWindow
    {
        public Extension Extension { get; }
        public abstract ISnoopObject Target { get; set; }

        public bool Inspect()
        {
            var foundRoot = this.FindRoot();
            if (foundRoot == null)
            {
                if (SnoopModes.MultipleDispatcherMode == false
                    && SnoopModes.MultipleAppDomainMode == false)
                {
                    //SnoopModes.MultipleDispatcherMode is always false for all scenarios except for cases where we are running multiple dispatchers.
                    //If SnoopModes.MultipleDispatcherMode was set to true, then there definitely was a root visual found in another dispatcher, so
                    //the message below would be wrong.
                    MessageBox.Show(
                         "Can't find a current application or a PresentationSource root visual.",
                         "Can't Snoop",
                         MessageBoxButton.OK,
                         MessageBoxImage.Exclamation);
                }

                // This path should only be hit if we don't find a root in some dispatcher or app domain.
                // This is not really critical as not every dispatcher/app domain must meet this requirement.
                Trace.WriteLine("Can't find a current application or a PresentationSource root visual.");

                return false;
            }

            this.Inspect(foundRoot);

            return true;
        }

        public SnoopMainBaseWindow(Extension extension) { this.Extension = extension; }

        public void Inspect(ISnoopObject rootToInspect)
        {
            ExceptionHandler.AddExceptionHandler(this.Dispatcher);

            this.Load(rootToInspect);

            this.Owner = SnoopWindowUtils.FindOwnerWindow(this);

            Trace.WriteLine("Showing snoop UI...");

            this.Show();
            this.Activate();

            Trace.WriteLine("Shown and activated snoop UI.");
        }

        protected abstract void Load(ISnoopObject rootToInspect);

        protected virtual ISnoopObject FindRoot()
        {
            object foundRoot = null;

            if (SnoopModes.MultipleDispatcherMode)
            {
                foreach (PresentationSource presentationSource in PresentationSource.CurrentSources)
                {
                    if (presentationSource.RootVisual is UIElement element
                        && element.Dispatcher.CheckAccess())
                    {
                        foundRoot = presentationSource.RootVisual;
                        break;
                    }
                }
            }
            else if (Application.Current != null)
            {
                foundRoot = Application.Current;
            }
            else
            {
                // if we don't have a current application,
                // then we must be in an interop scenario (win32 -> wpf or windows forms -> wpf).

                // in this case, let's iterate over PresentationSource.CurrentSources,
                // and use the first non-null, visible RootVisual we find as root to inspect.
                foreach (PresentationSource presentationSource in PresentationSource.CurrentSources)
                {
                    if (presentationSource.RootVisual is UIElement element
                        && element.Visibility == Visibility.Visible)
                    {
                        foundRoot = presentationSource.RootVisual;
                        break;
                    }
                }
            }

            return foundRoot;
        }
    }
}
