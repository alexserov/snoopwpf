namespace Snoop.DataAccess.WinUI3 {
    using System;
    using System.IO;
    using System.Runtime.InteropServices.WindowsRuntime;
    using Windows.Graphics.Imaging;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Media.Imaging;
    using Snoop.DataAccess.Interfaces;

    public class SO_UISurface : SnoopObjectBase, ISO_UISurface{
        public override Type DataAccessType {
            get { return typeof(ISO_UISurface); }
        }
        public SO_UISurface(object source) : base(source) { }
        public virtual byte[] GetData() { return new byte[] { }; }
        public event Action Changed = () => { };

        protected void RaiseChanged() {
            this.Changed();
        }
    }

    class VisualUISurface : SO_UISurface {
        readonly UIElement source;
        byte[] bytes;

        public VisualUISurface(UIElement source) : base(source) {
            this.source = source;
        }

        public override byte[] GetData() {
            if (this.bytes == null)
                this.UpdateBytes();
            return this.bytes;
        }

        void UpdateBytes() {
            bytes = this.source.OnUI(x => {
                var fe = x as FrameworkElement;
                var rtb = new RenderTargetBitmap();
                rtb.RenderAsync(fe).GetResults();
                var buff = rtb.GetPixelsAsync().GetResults();
                var result = new byte[buff.Length];
                buff.CopyTo(result);
                return result;
            });
        }
    }
}