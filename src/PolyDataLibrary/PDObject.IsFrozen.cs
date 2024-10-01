namespace Brimborium.PolyData;

partial class PDObject {
    public PDObject Freeze() {
        if (this._IsFrozen) {
            return this;
        } else {
            return new PDObject(this.Uid, this._Values, this._PreviousState ?? this, true, this._Changes);
        }
    }

    public PDObject Unfreeze() {
        if (this._IsFrozen) {
            return new PDObject(this.Uid, this._Values, this._PreviousState ?? this, false, this._Changes);
        } else {
            return this;
        }
    }

    IPDObject IPDObject.Freeze() => this.Freeze();

    IPDObject IPDObject.Unfreeze() => this.Unfreeze();
}
//
