namespace Brimborium.PolyData;

partial class PDObject {

    public PDSetPropertyResponse SetProperty(PDSetPropertyRequest setPropertyRequest) {
        PDGetPropertyResponse getPropertyResponse;
        {
            var getPropertyRequest = new PDGetPropertyRequest(
                MetaProperty: setPropertyRequest.MetaProperty,
                FlowInfo: setPropertyRequest.FlowInfo);
            getPropertyResponse = this.GetProperty(getPropertyRequest);
        }
        PDMetaSetPropertyResponse metaSetPropertyResponse;
        {
            var metaSetPropertyRequest = new PDMetaSetPropertyRequest(
                MetaProperty: setPropertyRequest.MetaProperty,
                OldValueExists: getPropertyResponse.ValueExists,
                OldValue: getPropertyResponse.Value,
                NextValue: setPropertyRequest.NextValue,
                FlowInfo: setPropertyRequest.FlowInfo);

            metaSetPropertyResponse = metaSetPropertyRequest.MetaProperty
                .SetPropertyPrepare(metaSetPropertyRequest);
        }

        if (metaSetPropertyResponse.ResponseIndicator is IPDFaultResponse) {
            return new PDSetPropertyResponse(
                ResponseIndicator: metaSetPropertyResponse.ResponseIndicator,
                Result: this
                );
        }

        {
            var valuesNext = this._Values.SetItem(
                    metaSetPropertyResponse.MetaProperty,
                    metaSetPropertyResponse.Value);

            if (this._IsFrozen) {
                return new PDSetPropertyResponse(
                    ResponseIndicator: metaSetPropertyResponse.ResponseIndicator,
                    Result: new PDObject(
                        uid: this.Uid,
                        values: valuesNext,
                        previousState: this._PreviousState ?? this,
                        isFrozen: true,
                        changes: this._Changes.Add(setPropertyRequest)
                        )
                    );
            } else {
                this._Values = valuesNext;
                this._Changes = this._Changes.Add(setPropertyRequest);
                return new PDSetPropertyResponse(
                    ResponseIndicator: metaSetPropertyResponse.ResponseIndicator,
                    Result: this
                    );
            }
        }
    }

    public PDGetPropertyResponse GetProperty(PDGetPropertyRequest getPropertyRequest) {
        if (this._Values.TryGetValue(getPropertyRequest.MetaProperty, out var result)) {
            return new PDGetPropertyResponse(
                PDResponseWellknown.Instance.Success,
                getPropertyRequest.MetaProperty, true, result);
        } else {
            return new PDGetPropertyResponse(
                PDResponseWellknown.Instance.NoValue,
                getPropertyRequest.MetaProperty, false, PDNothing.Empty);
        }
    }
}
// 
