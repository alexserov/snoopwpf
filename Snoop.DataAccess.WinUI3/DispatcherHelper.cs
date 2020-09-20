namespace Snoop.DataAccess.WinUI3 {
    using System;
    using System.Threading;
    using Windows.UI.Core;
    using Microsoft.System;
    using Microsoft.UI.Xaml;
    #pragma warning disable 8305

    public class DispatcherHelper {
        readonly DispatcherQueue dispatcher;

        DispatcherHelper(DispatcherQueue dispatcher) { this.dispatcher = dispatcher; }

        public static DispatcherHelper From(DispatcherQueue dobj) {
            return dobj == null ? Default : new DispatcherHelper(dobj);
        }
        public static DispatcherHelper From(DependencyObject dobj) {
            return dobj == null ? Default : new DispatcherHelper(dobj.DispatcherQueue);
        }
        public static DispatcherHelper From(SnoopObjectBase dobj) {
            return dobj == null ? Default : From(dobj.Source as DependencyObject);
        }
        public static DispatcherHelper Default {
            get { return new DispatcherHelper(WindowLocator.GetWindow().DispatcherQueue); }
        }

        public TResult Invoke<TResult>(Func<TResult> func) {
            TResult result = default;
            EventWaitHandle wh = new EventWaitHandle(false, EventResetMode.ManualReset);
            this.dispatcher.TryEnqueue(() => {
                result = func();
                wh.Set();
            });
            wh.WaitOne();
            return result;
        }
        public void Invoke(Action func) {
            EventWaitHandle wh = new EventWaitHandle(false, EventResetMode.ManualReset);
            this.dispatcher.TryEnqueue(() => {
                func();
                wh.Set();
            });
            wh.WaitOne();
        } 
    }

    public static class DispatcherHelperExtensions {
        public static TResult OnUI<TSource, TResult>(this TSource source, Func<TSource, TResult> func) where TSource : DependencyObject { return DispatcherHelper.From(source).Invoke(() => func(source)); }
        public static void OnUI<TSource>(this TSource source, Action<TSource> func) where TSource : DependencyObject { DispatcherHelper.From(source).Invoke(() => func(source)); }
        public static TResult OnUI<TResult>(this Window source, Func<Window, TResult> func) { return DispatcherHelper.From(source.DispatcherQueue).Invoke(() => func(source)); }
        public static void OnUI(this Window source, Action<Window> func) { DispatcherHelper.From(source.DispatcherQueue).Invoke(() => func(source)); }
    }
}