#if false
namespace Brimborium.PolyData;

public class PDObjectStaged : IPDObject {
    private ImmutableDictionary<IPDMetaProperty, IPDValue> _Values;
    private PDValue<Guid>? _Uid;
    private readonly IPDObject? _PreviousState;

    public PDObjectStaged() {
        this.Uid = Guid.NewGuid();
        this._Values = ImmutableDictionary<IPDMetaProperty, IPDValue>.Empty;
        this._PreviousState = default;
    }

    public PDObjectStaged(Guid uid) {
        this.Uid = uid;
        this._Values = ImmutableDictionary<IPDMetaProperty, IPDValue>.Empty;
        this._PreviousState = default;
    }

    public PDObjectStaged(IPDObject previousState) {
        this.Uid = previousState.Uid;
        this._Values = ImmutableDictionary<IPDMetaProperty, IPDValue>.Empty;
        this._PreviousState = previousState;
    }


    public Guid Uid { get; }

    public IPDObject SetProperty(IPDMetaProperty metaProperty, IPDValue value) {
        var oldValueExists = this.TryGetProperty(metaProperty, out var oldValue);
        var request = new PDMetaSetPropertyRequest(metaProperty, oldValueExists, oldValue, value);
        var response = metaProperty.SetPropertyPrepare(request);
        if (PDNothing.Empty.Equals(response.Failure)) {
            this._Values = this._Values.SetItem(response.MetaProperty, response.Value);
            return this;
        } else {
            return this;
        }
    }

    public bool TryGetProperty(IPDMetaProperty metaProperty, [MaybeNullWhen(false)] out IPDValue result) {
        if (this._Values.TryGetValue(metaProperty, out result)) {
            return true;
        } else if (PDMetaPropertyUid.Instance.Equals(metaProperty)) {
            result = (this._Uid ??= new PDValue<Guid>(this.Uid));
            return true;
        } else if (this._PreviousState is { } previousState) {
            return previousState.TryGetProperty(metaProperty, out result);
        } else {
            result = PDNothing.Empty;
            return false;
        }
    }
}
#endif
