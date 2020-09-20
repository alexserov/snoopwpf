namespace Snoop.DataAccess.WinUI3 {
    using System.Collections.Generic;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Media;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class DAS_TreeHelper : DataAccessBase, IDAS_TreeHelper {
        public int GetChildrenCount(ISnoopObject dependencyObject) {
            var uw = dependencyObject.UW<object>();
            if (uw is DependencyObject visual) {
                return visual.OnUI(VisualTreeHelper.GetChildrenCount);
            }

            if (uw is Window window) {
                return 1;
            }

            return 0;
        }

        public ISnoopObject GetChild(ISnoopObject dependencyObject, int i) {
            var uw = dependencyObject.UW<object>();
            if (uw is DependencyObject visual) {
                return SnoopObjectBase.Create(visual.OnUI(x => VisualTreeHelper.GetChild(x, i)));
            }
            if (uw is Window window) {
                return SnoopObjectBase.Create(window.OnUI(x => x.Content));
            }

            return null;
        }
        public static IEnumerable<DependencyObject> GetParents(DependencyObject visual) {
            var parent = visual;
            while (parent != null) {
                yield return parent;
                parent = VisualTreeHelper.GetParent(visual);
                
            }
        }
    }
}