namespace Brimborium.PolyData;


public partial class PDObject : IPDObject {
    public static PDObject Create(
        IPDObjectKey? uid = default,
        IPDRepositoryKey? repositoryKey = default,
        ImmutableDictionary<IPDMetaProperty, IPDValue>? values = default,
        IPDObject? previousState = default,
        bool? isFrozen = default,
        ImmutableArray<IPDSetPropertyRequest>? changes = default) {
        return new PDObject(
            uid: uid is { } uidNN ? uidNN : new PDObjectKey(),
            repositoryKey: repositoryKey,
            values: values ?? ImmutableDictionary<IPDMetaProperty, IPDValue>.Empty,
            previousState: previousState,
            isFrozen: isFrozen.GetValueOrDefault(),
            changes: changes ?? ImmutableArray<IPDSetPropertyRequest>.Empty
            );
    }

    private IPDRepositoryKey? _RepositoryKey;
    private ImmutableDictionary<IPDMetaProperty, IPDValue> _Values;
    private readonly IPDObject? _PreviousState;
    private bool _IsFrozen;
    private ImmutableArray<IPDSetPropertyRequest> _Changes;

    public PDObject() {
        this.Uid = new PDObjectKey();
        this._Values = ImmutableDictionary<IPDMetaProperty, IPDValue>.Empty;
        this._PreviousState = default;
        this._Changes = ImmutableArray<IPDSetPropertyRequest>.Empty;
    }

    public PDObject(IPDObjectKey uid) {
        this.Uid = uid;
        this._Values = ImmutableDictionary<IPDMetaProperty, IPDValue>.Empty;
        this._PreviousState = default;
        this._Changes = ImmutableArray<IPDSetPropertyRequest>.Empty;
    }

    public PDObject(
        IPDObjectKey uid,
        IPDRepositoryKey? repositoryKey,
        ImmutableDictionary<IPDMetaProperty, IPDValue> values,
        IPDObject? previousState,
        bool isFrozen,
        ImmutableArray<IPDSetPropertyRequest> changes
        ) {
        this.Uid = uid;
        this._RepositoryKey = repositoryKey;
        this._Values = values;
        this._PreviousState = previousState;
        this._IsFrozen = isFrozen;
        this._Changes = changes;
    }

    public IPDObjectKey Uid { get; }

    public IPDObject SetRepositoryKey(IPDRepositoryKey repositoryKey) {
        if (this._IsFrozen) {
            return new PDObject(
                uid: this.Uid,
                repositoryKey: repositoryKey,
                values: this._Values,
                previousState: this._PreviousState,
                isFrozen: this._IsFrozen,
                changes: this._Changes
                );
        } else { 
            this._RepositoryKey = repositoryKey;
            return this;
        }
    }

    public IPDObject ClearRepositoryKey(IPDRepositoryKey repositoryKey) {
        return this;
    }

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
