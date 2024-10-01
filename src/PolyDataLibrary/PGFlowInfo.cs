

namespace Brimborium.PolyData;

public record PGFlowInfo(
    Guid Uid
    ) {
    public PGFlowInfo() : this(Guid.NewGuid()) { }
}
