namespace Brimborium.PolyData;

public interface IPDObject {
    IPDObjectKey Uid { get; }

    IPDObject SetRepositoryKey(IPDRepositoryKey repositoryKey);

    IPDObject ClearRepositoryKey(IPDRepositoryKey repositoryKey);


    IPDValue? GetProperty(IPDMetaProperty property);
    IPDValue<T>? GetProperty<T>(IPDMetaProperty<T> property);

    PDSetPropertyResponse SetProperty(PDSetPropertyRequest setPropertyRequest);
    PDSetPropertyResponse<T> SetProperty<T>(PDSetPropertyRequest<T> setPropertyRequest);


    IPDObject Freeze();
    IPDObject Unfreeze();

    FlattenChangesResponse GetFlattenChanges();    
}

public interface IPDValue {
    object? GetValue();
}

public interface IPDValue<T>
    : IPDValue {
    T GetValueT();
}

public readonly record struct PDGetPropertyRequest(
    IPDMetaProperty MetaProperty,
    PGFlowInfo FlowInfo
    ) : IPDRequest;

public readonly record struct PDGetPropertyResponse(
    IPDResponseIndicator ResponseIndicator,
    IPDMetaProperty MetaProperty,
    bool ValueExists,
    IPDValue? Value
    ) : IPDResponse { }

public interface IPDSetPropertyRequest : IPDRequest {
    //IPDMetaProperty MetaProperty { get; }
    //IPDValue NextValue { get; }
}

public readonly record struct PDSetPropertyRequest(
    IPDMetaProperty MetaProperty,
    IPDValue NextValue,
    PGFlowInfo FlowInfo
    ) : IPDRequest, IPDSetPropertyRequest {
    public PDSetPropertyRequest(
        IPDMetaProperty MetaProperty,
        IPDValue NextValue)
        : this(MetaProperty, NextValue, new PGFlowInfo()) {
    }
}

public readonly record struct PDSetPropertyResponse(
    IPDResponseIndicator ResponseIndicator,
    IPDObject Result
    ) : IPDResponse {
}


public record class PDSetPropertyRequest<T>(
    IPDMetaProperty<T> MetaProperty,
    IPDValue<T> NextValue,
    PGFlowInfo FlowInfo
    ) : IPDRequest, IPDSetPropertyRequest {
    public PDSetPropertyRequest(
        IPDMetaProperty<T> MetaProperty,
        IPDValue<T> NextValue)
        : this(MetaProperty, NextValue, new PGFlowInfo()) {
    }
}

public readonly record struct PDSetPropertyResponse<T>(
    IPDResponseIndicator ResponseIndicator,
    IPDObject Result
    ) : IPDResponse {
}
