﻿namespace Brimborium.PolyData;

partial class PDObject {

    public PDSetPropertyResponse SetProperty(PDSetPropertyRequest setPropertyRequest) {
        var oldValue = this.GetProperty(setPropertyRequest.MetaProperty);
        PDMetaSetPropertyResponse metaSetPropertyResponse;
        {
            var metaSetPropertyRequest = new PDMetaSetPropertyRequest(
                MetaProperty: setPropertyRequest.MetaProperty,
                OldValue: oldValue,
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
            var changesNext = this._Changes.Add(setPropertyRequest);
            if (this._IsFrozen) {
                return new PDSetPropertyResponse(
                    ResponseIndicator: metaSetPropertyResponse.ResponseIndicator,
                    Result: new PDObject(
                        uid: this.Uid,
                        repositoryKey: this._RepositoryKey,
                        values: valuesNext,
                        previousState: this._PreviousState ?? this,
                        isFrozen: true,
                        changes: changesNext
                        )
                    );
            } else {
                this._Values = valuesNext;
                this._Changes = changesNext;
                return new PDSetPropertyResponse(
                    ResponseIndicator: metaSetPropertyResponse.ResponseIndicator,
                    Result: this
                    );
            }
        }
    }

    public IPDValue GetProperty(IPDMetaProperty property) {
        if (this._Values.TryGetValue(property, out var result)) {
            return result;
        } else {
            return PDNothing.Empty;
        }
    }

    public PDGetPropertyResponse HandleGetProperty(PDGetPropertyRequest getPropertyRequest) {
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
