namespace Brimborium.PolyData;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class PDMetaPropertyNamed(
    string name
    ) : IPDMetaProperty {
    public string Name { get; } = name;

    public bool Equals(IPDMetaProperty? other)
        => (other is PDMetaPropertyNamed metaPropertyNamed)
            && (string.Equals(this.Name, metaPropertyNamed.Name, StringComparison.Ordinal));

    public override bool Equals(object? other)
        => (other is PDMetaPropertyNamed metaPropertyNamed)
            && (string.Equals(this.Name, metaPropertyNamed.Name, StringComparison.Ordinal));

    public override int GetHashCode() => this.Name.GetHashCode();

    public PDMetaSetPropertyResponse SetPropertyPrepare(PDMetaSetPropertyRequest request)
        => new PDMetaSetPropertyResponse(
            ResponseIndicator: PDResponseWellknown.Instance.Success,
            MetaProperty: this,
            Value: request.NextValue);

    public override string ToString() => this.Name;

    private string GetDebuggerDisplay() => this.Name;
}

public class PDMetaPropertyNamed<T>(
    string name
    ) : PDMetaPropertyNamed(name), IPDMetaProperty<T> {
    public bool Equals(IPDMetaProperty<T>? other)
        => (other is PDMetaPropertyNamed<T> metaPropertyNamed)
            && (string.Equals(this.Name, metaPropertyNamed.Name, StringComparison.Ordinal));

    public PDMetaSetPropertyResponse<T> SetPropertyPrepare(PDMetaSetPropertyRequest<T> request)
        => new PDMetaSetPropertyResponse<T>(
            ResponseIndicator: PDResponseWellknown.Instance.Success,
            MetaProperty: this,
            Value: request.NextValue);
}
