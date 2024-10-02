namespace Brimborium.PolyData;

public interface IPDRepositoryKey : IEquatable<IPDRepositoryKey> {
}

public sealed class PDRepositoryKey : IPDRepositoryKey {
    private readonly Guid _Uid;

    public PDRepositoryKey() {
        this._Uid = Guid.NewGuid();
    }

    public bool Equals(IPDRepositoryKey? other)
        => other is PDRepositoryKey otherKey && this._Uid == otherKey._Uid;

    public override bool Equals(object? obj) 
        => obj is PDRepositoryKey otherKey && this._Uid == otherKey._Uid;

    public override int GetHashCode() => this._Uid.GetHashCode();

    public override string ToString() => this._Uid.ToString();
}
