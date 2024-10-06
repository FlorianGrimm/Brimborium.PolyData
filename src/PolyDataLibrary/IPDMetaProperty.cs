namespace Brimborium.PolyData;

public interface IPDMetaProperty: IEquatable<IPDMetaProperty> {
    PDMetaSetPropertyResponse SetPropertyPrepare(PDMetaSetPropertyRequest request);
}

public interface IPDMetaProperty<T> : IPDMetaProperty, IEquatable<IPDMetaProperty<T>> {
    PDMetaSetPropertyResponse<T> SetPropertyPrepare(PDMetaSetPropertyRequest<T> request);
}

public readonly record struct PDMetaSetPropertyRequest(
    IPDMetaProperty MetaProperty,
    IPDValue? OldValue,
    IPDValue NextValue,
    PGFlowInfo FlowInfo
    ) : IPDRequest { 
}

public readonly record struct PDMetaSetPropertyResponse(
    IPDResponseIndicator ResponseIndicator,
    IPDMetaProperty MetaProperty,
    IPDValue Value
    ) : IPDResponse { }

public readonly record struct PDMetaSetPropertyRequest<T>(
    IPDMetaProperty<T> MetaProperty,
    IPDValue<T> OldValue,
    IPDValue<T> NextValue,
    PGFlowInfo FlowInfo
    ) : IPDRequest {
}

public readonly record struct PDMetaSetPropertyResponse<T>(
    IPDResponseIndicator ResponseIndicator,
    IPDMetaProperty<T> MetaProperty,
    IPDValue<T> Value
    ) : IPDResponse { }
