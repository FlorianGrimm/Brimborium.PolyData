namespace Brimborium.PolyData;

public interface IPDObject {
    IPDObjectKey Uid { get; }

    IPDObject SetRepositoryKey(IPDRepositoryKey repositoryKey);

    IPDObject ClearRepositoryKey(IPDRepositoryKey repositoryKey);


    IPDValue GetProperty(IPDMetaProperty property);

    PDSetPropertyResponse SetProperty(PDSetPropertyRequest setPropertyRequest);


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

public record struct PDSetPropertyRequest(
    IPDMetaProperty MetaProperty,
    IPDValue NextValue,
    PGFlowInfo FlowInfo
    ) : IPDRequest {
    public PDSetPropertyRequest(
        IPDMetaProperty MetaProperty,
        IPDValue NextValue)
        : this(MetaProperty, NextValue, new PGFlowInfo()) {
    }
}

public record struct PDSetPropertyResponse(
    IPDResponseIndicator ResponseIndicator,
    IPDObject Result
    ) : IPDResponse {
}
