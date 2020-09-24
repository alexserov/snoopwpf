namespace Snoop.DataAccess.WinUI3 {
    using Snoop.DataAccess.Interfaces;

    public class SO_ValueSource : SnoopObjectBase, ISO_ValueSource{
        public SO_ValueSource() : base(null) { }
        public bool IsExpression {
            get => false;
        }
        public bool IsAnimated {
            get => false;
        }
        public string BaseValueSource {
            get => "Not Implemented";
        }
    }
}