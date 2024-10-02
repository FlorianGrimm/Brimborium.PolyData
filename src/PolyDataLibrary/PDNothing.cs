namespace Brimborium.PolyData;

public sealed class PDNothing : IPDObject, IPDValue, IPDMetaProperty, IPDResponseIndicator {
    //public static PDNothing Empty { get; } = new();
    public static PDNothing Empty => Nested.Empty;

    private sealed class Nested {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested() { }

        internal static readonly PDNothing Empty = new PDNothing();
    }

    public PDNothing() { }

    public IPDObjectKey Uid => new PDObjectKey(Guid.Empty);

    public string Message => string.Empty;

    public override int GetHashCode() => 0;

    public override bool Equals(object? obj) => obj is null || obj is PDNothing;
    public bool Equals(IPDMetaProperty? other) {
        return other is null || other is PDNothing;
    }

    public object? GetValue() => this;

    public PDMetaSetPropertyResponse SetPropertyPrepare(PDMetaSetPropertyRequest request)
        => new PDMetaSetPropertyResponse(
            PDResponseWellknown.Instance.NoValue, 
            request.MetaProperty, 
            this);

    public IPDValue GetProperty(IPDMetaProperty property) => this;
    // public PDGetPropertyResponse GetProperty(PDGetPropertyRequest getPropertyRequest) {
    //     return new PDGetPropertyResponse(
    //         ResponseIndicator: PDResponseWellknown.Instance.NoValue,
    //         MetaProperty: this,
    //         ValueExists: false,
    //         Value: this);
    // }

    public PDSetPropertyResponse SetProperty(PDSetPropertyRequest setPropertyRequest) {
        return new PDSetPropertyResponse(
            ResponseIndicator: PDResponseWellknown.Instance.NotSupported,
            Result: this);
    }

    public FlattenChangesResponse GetFlattenChanges() {
        return new(default, this, ImmutableArray<PDSetPropertyRequest>.Empty);
    }

    public IPDObject Freeze() => this;

    public IPDObject Unfreeze() => this;

    public IPDObject SetRepositoryKey(IPDRepositoryKey repositoryKey) => this;

    public IPDObject ClearRepositoryKey(IPDRepositoryKey repositoryKey) => this;


    //public static bool operator ==(PDNothing left, PDNothing right)
    //    => (object)left == right || (left?.Equals(right) ?? false);
    //public static bool operator !=(PDNothing left, PDNothing right)
    //    => !(left == right);
}