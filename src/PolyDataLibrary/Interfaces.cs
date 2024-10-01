namespace Brimborium.PolyData;

public interface IPDObject {
    Guid Uid { get; }

    PDGetPropertyResponse GetProperty(PDGetPropertyRequest getPropertyRequest);

    PDSetPropertyResponse SetProperty(PDSetPropertyRequest setPropertyRequest);

    FlattenChangesResponse GetFlattenChanges();
    IPDObject Freeze();
    IPDObject Unfreeze();

#if false
    IPDObject SetProperty(IPDMetaProperty metaProperty, IPDValue value);
    bool TryGetProperty(IPDMetaProperty metaProperty, [MaybeNullWhen(false)] out IPDValue result);
#endif
}

public interface IPDValue {
    object? GetValue();
}

public interface IPDValue<T>
    : IPDValue {
    T GetValueT();
}

public record PDGetPropertyRequest(
    IPDMetaProperty MetaProperty,
    PGFlowInfo FlowInfo
    ) : IPDRequest;

public record PDGetPropertyResponse(
    IPDResponseIndicator ResponseIndicator,
    IPDMetaProperty MetaProperty,
    bool ValueExists,
    IPDValue Value
    ) : IPDResponse { }

public record PDSetPropertyRequest(
    IPDMetaProperty MetaProperty,
    IPDValue NextValue,
    PGFlowInfo FlowInfo
    ) : IPDRequest;

public record PDSetPropertyResponse(
    IPDResponseIndicator ResponseIndicator,
    IPDObject Result
    ) : IPDResponse { 
}

