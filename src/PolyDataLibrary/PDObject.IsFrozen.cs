namespace Brimborium.PolyData;

partial class PDObject {
    public PDObject Freeze() {
        if (this._IsFrozen) {
            return this;
        } else {
            return new PDObject(
                uid:this.Uid,
                repositoryKey: this._RepositoryKey,
                values: this._Values, 
                previousState: this._PreviousState ?? this, 
                isFrozen: true, 
                changes: this._Changes);
        }
    }

    public PDObject Unfreeze() {
        if (this._IsFrozen) {
            return new PDObject(
                uid:this.Uid,
                repositoryKey: this._RepositoryKey,
                values: this._Values,
                previousState: this._PreviousState ?? this,
                isFrozen: false,
                changes: this._Changes);
        } else {
            return this;
        }
    }

    IPDObject IPDObject.Freeze() => this.Freeze();

    IPDObject IPDObject.Unfreeze() => this.Unfreeze();
}
//
