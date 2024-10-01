namespace Brimborium.PolyData;

public interface IPDRequest {
    PGFlowInfo FlowInfo { get; }
}

public interface IPDResponse {
    IPDResponseIndicator ResponseIndicator { get; }
}
