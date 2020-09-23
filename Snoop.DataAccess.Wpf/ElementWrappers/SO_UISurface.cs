namespace Snoop.DataAccess.Wpf {
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class SO_UISurface : SnoopObjectBase, ISO_UISurface{
        public override Type DataAccessType {
            get { return typeof(ISO_UISurface); }
        }
        public SO_UISurface(object source) : base(source) { }
        public int Width { get; set; }
        public int Height { get; set; }
        public virtual byte[] GetData() { return new byte[] { }; }
        public event Action Changed = () => { };

        protected void RaiseChanged() {
            this.Changed();
        }
    }

    class VisualUISurface : SO_UISurface {
        readonly Visual source;
        byte[] bytes;

        public VisualUISurface(Visual source) : base(source) {
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
                var rtb = new RenderTargetBitmap((int)fe.ActualWidth, (int)fe.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                rtb.Render(fe);
                FormatConvertedBitmap formatConverter = new FormatConvertedBitmap();
                formatConverter.BeginInit();
                formatConverter.DestinationFormat = PixelFormats.Rgb24;
                formatConverter.Source = rtb;
                formatConverter.EndInit();
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                BitmapFrame frame = BitmapFrame.Create(formatConverter);
                encoder.Frames.Add(frame);
                using (MemoryStream stream = new MemoryStream()) {
                    encoder.Save(stream);
                    return stream.ToArray();
                }
            });
        }
    }
}