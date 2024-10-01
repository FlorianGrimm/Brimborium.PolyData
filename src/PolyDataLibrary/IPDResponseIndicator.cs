namespace Brimborium.PolyData;

public interface IPDResponseIndicator : IPDValue {
    string Message { get; }

    //public enum PDResponseMode { NoValue, Success, Failure }
    //PDResponseMode ResponseMode { get; }
}
public interface IPDFaultResponse : IPDValue, IPDResponseIndicator {
    // string Message { get; }
}

