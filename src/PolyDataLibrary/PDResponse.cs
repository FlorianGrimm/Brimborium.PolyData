namespace Brimborium.PolyData;

public sealed class PDSuccessResponse(
    string message
    ) : IPDValue, IPDResponseIndicator {
    public string Message { get; } = message;

    public object? GetValue() => this;

    public override string ToString() => this.Message;
}

public sealed class PDNoValueResponse(
    string message
    ) : IPDValue, IPDResponseIndicator {
    public string Message { get; } = message;

    public object? GetValue() => this;

    public override string ToString() => this.Message;
}


public sealed class PDFaultResponse(
    string errorMessage,
    Exception? error
    ) : IPDFaultResponse, IPDValue {
    public string Message { get; } = errorMessage;

    public Exception? Error { get; } = error;

    public object? GetValue() => this;

    public override string ToString()
        => (this.Error is { } error)
        ? $"{this.Message} {error.Message}"
        : this.Message
        ;
}


public sealed class PDResponseWellknown {
    public static PDResponseWellknown Instance => Nested.instance;

    private sealed class Nested {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested() {
        }

        internal static readonly PDResponseWellknown instance = new PDResponseWellknown();
    }

    private PDResponseWellknown() {
        this.Success = new PDSuccessResponse(string.Empty);
        this.NoValue = new PDNoValueResponse(string.Empty);
        this.PropertyNotFound = new PDFaultResponse("Property not found.", default);
        this.NotSupported = new PDFaultResponse("Not Supported.", default);
    }

    public readonly PDSuccessResponse Success;
    public readonly PDNoValueResponse NoValue;
    public readonly PDFaultResponse PropertyNotFound;
    public readonly PDFaultResponse NotSupported;
}