namespace Snoop.DataAccess.Interfaces {
    public interface ISO_ValueSource {
        bool IsExpression { get; }
        bool IsAnimated { get; }
        string BaseValueSource { get; }
    }
}