namespace Brimborium.PolyData;

public interface IPDObjectKey : IEquatable<IPDObjectKey>{
}

public record PDObjectKey(
    Guid Uid
    ) : IPDObjectKey {
    public PDObjectKey() : this(Guid.NewGuid()){        
    }

    public bool Equals(IPDObjectKey? other) 
        => other is PDObjectKey otherKey && this.Uid == otherKey.Uid;
}