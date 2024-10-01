namespace Brimborium.PolyData;

public partial class PDObject : IPDObject {
    public static PDObject Create(
        Guid? uid = default,
        ImmutableDictionary<IPDMetaProperty, IPDValue>? values = default,
        IPDObject? previousState = default,
        bool? isFrozen = default,
        ImmutableArray<PDSetPropertyRequest>? changes = default) {
        return new PDObject(
            uid: uid.HasValue ? uid.Value : Guid.NewGuid(),
            values: values ?? ImmutableDictionary<IPDMetaProperty, IPDValue>.Empty,
            previousState: previousState,
            isFrozen: isFrozen.GetValueOrDefault(),
            changes: changes ?? ImmutableArray<PDSetPropertyRequest>.Empty
            );
    }
    private ImmutableDictionary<IPDMetaProperty, IPDValue> _Values;
    private readonly IPDObject? _PreviousState;
    private bool _IsFrozen;
    private ImmutableArray<PDSetPropertyRequest> _Changes;

    public PDObject() {
        this.Uid = Guid.NewGuid();
        this._Values = ImmutableDictionary<IPDMetaProperty, IPDValue>.Empty;
        this._PreviousState = default;
        this._Changes = ImmutableArray<PDSetPropertyRequest>.Empty;
    }

    public PDObject(Guid uid) {
        this.Uid = uid;
        this._Values = ImmutableDictionary<IPDMetaProperty, IPDValue>.Empty;
        this._PreviousState = default;
        this._Changes = ImmutableArray<PDSetPropertyRequest>.Empty;
    }

    public PDObject(
        Guid uid,
        ImmutableDictionary<IPDMetaProperty, IPDValue> values,
        IPDObject? previousState,
        bool isFrozen,
        ImmutableArray<PDSetPropertyRequest> changes
        ) {
        this.Uid = uid;
        this._Values = values;
        this._PreviousState = previousState;
        this._IsFrozen = isFrozen;
        this._Changes = changes;
    }

    public Guid Uid { get; }



#if false
    public IPDObject SetProperty(IPDMetaProperty metaProperty, IPDValue value) {
        var valuesNext = this._Values.SetItem(metaProperty, value);
        return new PDObjectImmutable(this.Uid, valuesNext, this._PreviousState ?? this);
    }

    public bool TryGetProperty(IPDMetaProperty metaProperty, [MaybeNullWhen(false)] out IPDValue result) {
        if (this._Values.TryGetValue(metaProperty, out result)) {
            return true;
        } else if (PDMetaPropertyUid.Instance.Equals(metaProperty)) {
            result = (this._Uid ??= new PDValue<Guid>(this.Uid));
            return true;
        } else {
            result = PDNothing.Empty;
            return false;
        }
    }
#endif
}
//
