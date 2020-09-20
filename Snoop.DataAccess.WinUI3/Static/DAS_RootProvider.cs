namespace Snoop.DataAccess.WinUI3 {
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Media;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class DAS_RootProvider : DataAccessBase, IDAS_RootProvider{
        public ISnoopObject Root {
            get { return SnoopObjectBase.Create(WindowLocator.GetWindow()); }
        }

        public ISnoopObject RootFrom(ISO_Visual source) {
            return SnoopObjectBase.Create(source.UW<DependencyObject>().OnUI(visual => DAS_TreeHelper.GetParents(visual).Last()));
        }
     
    }
}