namespace Brimborium.PolyData;

public record PGFlowInfo(
    Guid Uid
    ) {
    public PGFlowInfo() : this(Guid.NewGuid()) { }

    public PDSetPropertyRequest<T> SetPropertyRequest<T>(IPDMetaProperty<T> property, T value) {
        return new PDSetPropertyRequest<T>(
            property,
            new PDValue<T>(value),
            this
            );
    }
}
