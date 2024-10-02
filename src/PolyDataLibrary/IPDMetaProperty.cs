namespace Brimborium.PolyData;

public interface IPDMetaProperty: IEquatable<IPDMetaProperty> {
    PDMetaSetPropertyResponse SetPropertyPrepare(PDMetaSetPropertyRequest request);
}
public record PDMetaSetPropertyRequest(
    IPDMetaProperty MetaProperty,
    IPDValue OldValue,
    IPDValue NextValue,
    PGFlowInfo FlowInfo
    ) : IPDRequest { 
}

public record PDMetaSetPropertyResponse(
    IPDResponseIndicator ResponseIndicator,
    IPDMetaProperty MetaProperty,
    IPDValue Value
    ) : IPDResponse { }
