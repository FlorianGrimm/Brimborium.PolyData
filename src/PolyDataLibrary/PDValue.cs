namespace Brimborium.PolyData;

public class PDValue : IPDValue {
    public static PDValue<T> Create<T>(T value)
        => new PDValue<T>(value);

    private readonly object? _Value;
    public PDValue(object? value) {
        this._Value = value;
    }

    public object? Value => this._Value;

    public object? GetValue() => this.Value;

}


public class PDValue<T>
    : IPDValue
    , IPDValue<T> {
    private readonly T _Value;

    public PDValue(T value) {
        this._Value = value;
    }

    public T Value => this._Value;

    public object? GetValue() => this._Value;

    public T GetValueT() => this._Value;
}
