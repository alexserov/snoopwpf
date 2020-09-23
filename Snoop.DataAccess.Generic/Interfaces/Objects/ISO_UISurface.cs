namespace Snoop.DataAccess.Interfaces {
    using System;

    public interface ISO_UISurface : ISnoopObject {
        public int Width { get; set; }
        public int Height { get; set; }
        byte[] GetData();
        event Action Changed;
    }
}