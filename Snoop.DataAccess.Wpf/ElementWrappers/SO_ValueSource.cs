namespace Snoop.DataAccess.Wpf {
    using System.Windows;
    using Snoop.DataAccess.Interfaces;

    public class SO_ValueSource : SnoopObjectBase, ISO_ValueSource{
        readonly ValueSource source;
        public SO_ValueSource(ValueSource source) : base(source) { this.source = source; }
        public bool IsExpression {
            get => this.source.IsExpression;
        }
        public bool IsAnimated {
            get => this.source.IsAnimated;
        }
        public string BaseValueSource {
            get => this.source.BaseValueSource.ToString();
        }
    }
}