namespace Snoop.DataAccess.Wpf {
    using System;

    public class SO_Dumb : SnoopObjectBase{
        public SO_Dumb(object source) : base(source) { }
        public override string ToString() {
            return Convert.ToString(Source);
        }
    }
}