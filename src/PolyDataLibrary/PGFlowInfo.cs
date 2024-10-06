

namespace Brimborium.PolyData;

public record PGFlowInfo(
    Guid Uid
    ) {
    public PGFlowInfo() : this(Guid.NewGuid()) { }

    public PDSetPropertyRequest SetPropertyRequest(IPDMetaProperty property, object? value) {
        return new PDSetPropertyRequest(
            property,
            new PDValue(value),
            this
            );
    }
}
