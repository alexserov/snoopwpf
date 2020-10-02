// (c) Copyright Cory Plotts.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace Snoop.Infrastructure.Helpers
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;
    using Snoop.DataAccess.Sessions;

    public static class VisualTreeHelper2
    {
        public delegate HitTestFilterBehavior EnumerateTreeFilterCallback(DependencyObject input, object misc);

        public delegate HitTestResultBehavior EnumerateTreeResultCallback(DependencyObject input, object misc);

        public static T GetAncestor<T>(DependencyObject input, Predicate<T> predicate = null)
            where T : DependencyObject
        {
            var current = input;

            {
                if (current is T result
                    && predicate?.Invoke(result) != false)
                {
                    return result;
                }
            }

            while (!(current is null))
            {
                current = VisualTreeHelper.GetParent(current);

                if (current is T result
                    && predicate?.Invoke(result) != false)
                {
                    return result;
                }
            }

            return null;
        }

        public static void EnumerateTree(Visual reference, EnumerateTreeFilterCallback filterCallback, EnumerateTreeResultCallback enumeratorCallback, object misc)
        {
            if (reference is null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            DoEnumerateTree(reference, filterCallback, enumeratorCallback, misc);
        }

        private static bool DoEnumerateTree(DependencyObject reference, EnumerateTreeFilterCallback filterCallback, EnumerateTreeResultCallback enumeratorCallback, object misc)
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(reference); ++i)
            {
                var child = VisualTreeHelper.GetChild(reference, i);

                var filterResult = filterCallback?.Invoke(child, misc) ?? HitTestFilterBehavior.Continue;

                var enumerateSelf = true;
                var enumerateChildren = true;

                switch (filterResult)
                {
                    case HitTestFilterBehavior.Continue:
                        break;

                    case HitTestFilterBehavior.ContinueSkipChildren:
                        enumerateChildren = false;
                        break;

                    case HitTestFilterBehavior.ContinueSkipSelf:
                        enumerateSelf = false;
                        break;

                    case HitTestFilterBehavior.ContinueSkipSelfAndChildren:
                        enumerateChildren = false;
                        enumerateSelf = false;
                        break;

                    default:
                        return false;
                }

                if ((enumerateSelf && enumeratorCallback?.Invoke(child, misc) == HitTestResultBehavior.Stop)
                    || (enumerateChildren && DoEnumerateTree(child, filterCallback, enumeratorCallback, misc) == false))
                {
                    return false;
                }
            }

            return true;
        }

        public static ISO_DependencyObject GetParent(ISO_DependencyObject itemToFind) {
            return ExtensionLocator.From(itemToFind).Get<IDAS_TreeHelper>().GetParent(itemToFind) as ISO_DependencyObject;
        }
        

        public static int GetChildrenCount(ISnoopObject dependencyObject) {
            return ExtensionLocator.From(dependencyObject).Get<IDAS_TreeHelper>().GetChildrenCount(dependencyObject);
        }

        public static ISnoopObject GetChild(ISnoopObject dependencyObject, int i) {
            return ExtensionLocator.From(dependencyObject).Get<IDAS_TreeHelper>().GetChild(dependencyObject, i);
        }
        public static Rect GetContentBounds(ISO_Visual visual) { throw new NotImplementedException(); }
        public static Rect GetDescendantBounds(ISO_Visual visual) { throw new NotImplementedException(); }
        public static Transform GetTransform(ISO_Visual visual) { throw new NotImplementedException(); }
        public static Point GetOffset(ISO_Visual visual) { throw new NotImplementedException(); }
        public static Drawing GetDrawing(ISO_Visual visual) { throw new NotImplementedException(); }
    }
}