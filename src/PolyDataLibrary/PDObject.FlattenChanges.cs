namespace Brimborium.PolyData;

partial class PDObject {
    public FlattenChangesResponse GetFlattenChanges() {
        if (this._PreviousState is null && this._Changes.Length == 0) {
            // No Changes
            return new FlattenChangesResponse(
                PrevInstance: default,
                NextInstance: this,
                Changes: this._Changes);
        }

        if (this._PreviousState is null) {
            // no parent
            PDObject nextInstance = new PDObject(
                    uid: this.Uid,
                    values: this._Values,
                    previousState: default,
                    isFrozen: this._IsFrozen,
                    changes: ImmutableArray<PDSetPropertyRequest>.Empty);
            return new FlattenChangesResponse(
                PrevInstance: default,
                NextInstance: nextInstance,
                Changes: this._Changes);
        } 
        
        {
            PDObject prevInstance = this;
            ImmutableDictionary<IPDMetaProperty, IPDValue>? orginalDictProperty = this._Values;
            Dictionary<IPDMetaProperty, IPDValue>? dictProperty = default;
            ImmutableArray<PDSetPropertyRequest> changes = this._Changes;

            for(;prevInstance._PreviousState is PDObject previousState
                ; prevInstance = previousState) {
                foreach (var kv in previousState._Values) {
                    if (orginalDictProperty is { }) {
                        if (orginalDictProperty.ContainsKey(kv.Key)) {
                            continue;
                        }
                        dictProperty = orginalDictProperty.ToDictionary();
                    } 
                    if (dictProperty is { }) { 
                        if (dictProperty.ContainsKey(kv.Key)) { continue; }
                        dictProperty.Add(kv.Key, kv.Value);
                    }
                }
            }
         
            PDObject nextInstance = new PDObject(
                    uid: this.Uid,
                    values: (orginalDictProperty 
                            ?? (dictProperty?.ToImmutableDictionary()) 
                            ?? ImmutableDictionary<IPDMetaProperty, IPDValue>.Empty),
                    previousState: default,
                    isFrozen: this._IsFrozen,
                    changes: ImmutableArray<PDSetPropertyRequest>.Empty);
            return new FlattenChangesResponse(
                PrevInstance: prevInstance,
                NextInstance: nextInstance,
                Changes: changes);
        }
    }
}

public sealed record FlattenChangesResponse(
    IPDObject? PrevInstance,
    IPDObject NextInstance,
    ImmutableArray<PDSetPropertyRequest> Changes);