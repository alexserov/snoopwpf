namespace Snoop.DataAccess.WinUI3 {
    using System;
    using Windows.UI.Core;
    using Microsoft.UI.Xaml;

    public class DispatcherHelper {
        readonly CoreDispatcher dispatcher;

        DispatcherHelper(CoreDispatcher dispatcher) { this.dispatcher = dispatcher; }

        public static DispatcherHelper From(CoreDispatcher dobj) {
            return dobj == null ? Default : new DispatcherHelper(dobj);
        }
        public static DispatcherHelper From(DependencyObject dobj) {
            return dobj == null ? Default : new DispatcherHelper(dobj.Dispatcher);
        }
        public static DispatcherHelper From(SnoopObjectBase dobj) {
            return dobj == null ? Default : From(dobj.Source as DependencyObject);
        }
        public static DispatcherHelper Default {
            get { return new DispatcherHelper(Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher); }
        }

        public TResult Invoke<TResult>(Func<TResult> func) {
            TResult result = default;
            this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => result = func()).GetResults();
            return result;
        }
        public void Invoke(Action func) {
            this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => func()).GetResults();
        } 
    }

    public static class DispatcherHelperExtensions {
        public static TResult OnUI<TSource, TResult>(this TSource source, Func<TSource, TResult> func) where TSource : DependencyObject { return DispatcherHelper.From(source).Invoke(() => func(source)); }
        public static void OnUI<TSource>(this TSource source, Action<TSource> func) where TSource : DependencyObject { DispatcherHelper.From(source).Invoke(() => func(source)); }
        public static TResult OnUI<TResult>(this Window source, Func<Window, TResult> func) { return DispatcherHelper.From(source.Dispatcher).Invoke(() => func(source)); }
        public static void OnUI(this Window source, Action<Window> func) { DispatcherHelper.From(source.Dispatcher).Invoke(() => func(source)); }
    }
}