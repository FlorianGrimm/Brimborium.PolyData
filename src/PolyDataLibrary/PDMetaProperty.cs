using System.Diagnostics;

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
            MetaProperty: request.MetaProperty,
            Value: request.NextValue);

    public override string ToString() => this.Name;

    private string GetDebuggerDisplay() => this.Name;
}

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class PDMetaPropertySymbol(
    string name
    ) : IPDMetaProperty {
    public string Name { get; } = name;

    public PDMetaSetPropertyResponse SetPropertyPrepare(PDMetaSetPropertyRequest request)
        => new PDMetaSetPropertyResponse(
            ResponseIndicator: PDResponseWellknown.Instance.Success,
            MetaProperty: request.MetaProperty,
            Value: request.NextValue);

    public bool Equals(IPDMetaProperty? other) => ReferenceEquals(this, other);

    public override bool Equals(object? other) => ReferenceEquals(this, other);

    public override int GetHashCode() => this.Name.GetHashCode();

    public override string ToString() => this.Name;

    private string GetDebuggerDisplay() => this.Name;
}

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public sealed class PDMetaPropertyUid : IPDMetaProperty {
    public const string Name = "Uid";
    private static PDMetaPropertyUid? _Instance;
    public static PDMetaPropertyUid Instance => (_Instance ??= new());

    public PDMetaPropertyUid() {
    }

    public PDMetaSetPropertyResponse SetPropertyPrepare(PDMetaSetPropertyRequest request)
        => new PDMetaSetPropertyResponse(
            ResponseIndicator: PDResponseWellknown.Instance.Success,
            MetaProperty: request.MetaProperty,
            Value: request.NextValue);

    public bool Equals(IPDMetaProperty? other) => other is PDMetaPropertyUid;

    public override bool Equals(object? other) => other is PDMetaPropertyUid;

    public override int GetHashCode() => Name.GetHashCode();

    public override string ToString() => Name;

    private string GetDebuggerDisplay() => Name;
}
