namespace Snoop.DataAccess.Wpf {
    using System.Windows;
    using System.Windows.Media;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class DAS_RootProvider : DataAccessBase, IDAS_RootProvider{
        public ISnoopObject Root {
            get { return SnoopObjectBase.Create(Application.Current.OnUI(x=>x.MainWindow)); }
        }

        public ISnoopObject RootFrom(ISO_Visual source) {
            var v = source.UW<Visual>();
            return SnoopObjectBase.Create(v.OnUI(x => PresentationSource.FromVisual(x)?.RootVisual));
        }
    }
}