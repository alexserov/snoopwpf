namespace Snoop.DataAccess.Wpf {
    using System.Windows.Media;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class DAS_TreeHelper : DataAccessBase, IDAS_TreeHelper {
        public int GetChildrenCount(ISnoopObject dependencyObject) {
            var uw = dependencyObject.UW<object>();
            if (uw is Visual visual) {
                return visual.OnUI(VisualTreeHelper.GetChildrenCount);
            }

            return 0;
        }

        public ISnoopObject GetChild(ISnoopObject dependencyObject, int i) {
            var uw = dependencyObject.UW<object>();
            if (uw is Visual visual) {
                return SnoopObjectBase.Create(visual.OnUI(x => VisualTreeHelper.GetChild(x, i)));
            }

            return null;
        }
    }
}