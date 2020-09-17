namespace Snoop.DataAccess.Wpf {
    using System.Reflection;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class MouseStatic : DataAccessBase, IDAS_MouseStatic{
        private static readonly PropertyInfo rawDirectlyOverPropertyInfo = typeof(MouseDevice).GetProperty("RawDirectlyOver", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        public static IInputElement GetDirectlyOver(MouseDevice mouseDevice)
        {
            return GetElementAtMousePos(mouseDevice.Dispatcher)
                   ?? rawDirectlyOverPropertyInfo?.GetValue(mouseDevice, null) as IInputElement
                   ?? mouseDevice.DirectlyOver;
        }
        

        private static FrameworkElement GetElementAtMousePos(Dispatcher dispatcher)
        {
            var windowHandleUnderMouse = NativeMethods.GetWindowUnderMouse();
            var windowUnderMouse = WindowHelperStatic.GetVisibleWindow(windowHandleUnderMouse);

            FrameworkElement directlyOverElement = null;

            if (windowUnderMouse != null)
            {
                VisualTreeHelper2.HitTest(windowUnderMouse, FilterCallback, r => ResultCallback(r, ref directlyOverElement), new PointHitTestParameters(Mouse.GetPosition(windowUnderMouse)));
            }

            return directlyOverElement;
        }

        private static HitTestFilterBehavior FilterCallback(DependencyObject target)
        {
            return HitTestFilterBehavior.Continue;
        }

        private static HitTestResultBehavior ResultCallback(HitTestResult result, ref FrameworkElement directlyOverElement)
        {
            if (result != null
                && result.VisualHit is FrameworkElement frameworkElement
                && frameworkElement.IsVisible
                )
            {
                directlyOverElement = frameworkElement;
                return HitTestResultBehavior.Stop;
            }

            return HitTestResultBehavior.Continue;
        }
        public ISnoopObject DirectlyOver {
            get { return SnoopObjectBase.Create(GetDirectlyOver(Mouse.PrimaryDevice)); }
        }
    }
}