namespace Snoop.DataAccess.Wpf {
    using System;
    using System.IO.Compression;
    using System.Windows;
    using System.Windows.Threading;

    public class DispatcherHelper {
        readonly Dispatcher dispatcher;

        DispatcherHelper(Dispatcher dispatcher) { this.dispatcher = dispatcher; }

        public static DispatcherHelper From(DispatcherObject dobj) {
            return dobj == null ? Default : new DispatcherHelper(dobj.Dispatcher);
        }
        public static DispatcherHelper From(SnoopObjectBase dobj) {
            return dobj == null ? Default : From(dobj.Source as DispatcherObject);
        }
        public static DispatcherHelper Default {
            get { return From(Application.Current); }
        }

        public TResult Invoke<TResult>(Func<TResult> func) {
            return this.dispatcher.Invoke(func);
        }
        public void Invoke(Action func) {
            this.dispatcher.Invoke(func);
        } 
    }

    public static class DispatcherHelperExtensions {
        public static TResult OnUI<TSource, TResult>(this TSource source, Func<TSource, TResult> func) where TSource : DispatcherObject { return DispatcherHelper.From(source).Invoke(() => func(source)); }
        public static void OnUI<TSource>(this TSource source, Action<TSource> func) where TSource : DispatcherObject { DispatcherHelper.From(source).Invoke(() => func(source)); }
    }
}