namespace Snoop.DataAccess.Wpf {
    using System.Windows.Data;
    using Snoop.DataAccess.Interfaces;

    public class SO_Binding : SnoopObjectBase, ISO_Binding{
        public SO_Binding(Binding source) : base(source) { }
    }
}