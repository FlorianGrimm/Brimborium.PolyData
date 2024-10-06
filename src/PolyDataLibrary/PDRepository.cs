namespace Brimborium.PolyData;

public interface IPDRepository {
    IPDRepository AddRange(IEnumerable<IPDObject> value);
    PDRepositoryResponse Add(IPDObject value);
    PDRepositoryResponse Set(IPDObject value);
    PDRepositoryResponse Remove(IPDObject value);
    PDRepositoryResponse Insert(IPDObject value);
    PDRepositoryResponse Update(IPDObject value);
    PDRepositoryResponse Delete(IPDObject value);
}


public readonly record struct PDRepositoryResponse(
    PDRepository Repository,
    IPDObject Result
    );

[Flags]
public enum PDRepositoryModification {
    None = 0,
    Insert = 1,
    Update = 2,
    Delete = 4,
    Ignore = 8,
}

public class PDRepository : IPDRepository {
    private readonly PDRepositoryKey _RepositoryKey;
    private readonly ImmutableDictionary<IPDObjectKey, IPDObject> _Items;
    private readonly ImmutableSortedDictionary<IPDObjectKey, PDRepositoryModification> _Modifications;

    public PDRepository() {
        this._RepositoryKey = new PDRepositoryKey();
        this._Items = ImmutableDictionary<IPDObjectKey, IPDObject>.Empty;
        this._Modifications = ImmutableSortedDictionary<IPDObjectKey, PDRepositoryModification>.Empty;
    }

    public PDRepository(
        PDRepositoryKey repositoryKey,
        ImmutableDictionary<IPDObjectKey, IPDObject> items,
        ImmutableSortedDictionary<IPDObjectKey, PDRepositoryModification> modifications
        ) {
        this._RepositoryKey = repositoryKey;
        this._Items = items;
        this._Modifications = modifications;
    }

    public IPDRepository AddRange(IEnumerable<IPDObject> value) {
        var itemsBuilder = this._Items.ToBuilder();
        foreach (var item in value) {
            var itemNext = item.SetRepositoryKey(this._RepositoryKey);
            itemsBuilder.Add(itemNext.Uid, itemNext);
        }
        var items = itemsBuilder.ToImmutable();

        var result = new PDRepository(
            repositoryKey: this._RepositoryKey,
            items: items,
            modifications: this._Modifications);
        return result;
    }

    public PDRepositoryResponse Add(IPDObject value) {
        var valueNext = value.SetRepositoryKey(this._RepositoryKey);
        var items = this._Items.Add(valueNext.Uid, valueNext);

        var result = new PDRepository(
            repositoryKey: this._RepositoryKey,
            items: items,
            modifications: this._Modifications);
        return new PDRepositoryResponse(result, valueNext);
    }

    public PDRepositoryResponse Set(IPDObject value) {
        var valueNext = value.SetRepositoryKey(this._RepositoryKey);
        var items = this._Items.SetItem(valueNext.Uid, valueNext);

        var result = new PDRepository(
            repositoryKey: this._RepositoryKey,
            items: items,
            modifications: this._Modifications);
        return new PDRepositoryResponse(result, valueNext);
    }

    public PDRepositoryResponse Remove(IPDObject value) {
        var valueNext = value.ClearRepositoryKey(this._RepositoryKey);
        var items = this._Items.Remove(valueNext.Uid);

        var result = new PDRepository(
            repositoryKey: this._RepositoryKey,
            items: items,
            modifications: this._Modifications);
        return new PDRepositoryResponse(result, valueNext);
    }

    public PDRepositoryResponse Insert(IPDObject value) {
        var valueNext = value.SetRepositoryKey(this._RepositoryKey);
        var items = this._Items.Add(valueNext.Uid, valueNext);

        // thinkof
        ImmutableSortedDictionary<IPDObjectKey, PDRepositoryModification> modifications;
        if (this._Modifications.TryGetValue(valueNext.Uid, out var modification)) {
            if (modification.HasFlag(PDRepositoryModification.Delete)) {
                modifications = this._Modifications.SetItem(valueNext.Uid, PDRepositoryModification.Update);
            } else {
                modifications = this._Modifications.SetItem(valueNext.Uid, PDRepositoryModification.Insert);
            }
        } else {
            modifications = this._Modifications.SetItem(valueNext.Uid, PDRepositoryModification.Insert);
        }

        var result = new PDRepository(
            repositoryKey: this._RepositoryKey,
            items: items,
            modifications: modifications);
        return new PDRepositoryResponse(result, valueNext);
    }


    public PDRepositoryResponse Update(IPDObject value) {
        var valueNext = value.SetRepositoryKey(this._RepositoryKey);
        var items = this._Items.SetItem(valueNext.Uid, valueNext);

        // thinkof
        ImmutableSortedDictionary<IPDObjectKey, PDRepositoryModification> modifications;
        if (this._Modifications.TryGetValue(valueNext.Uid, out var modification)) {
            if (modification.HasFlag(PDRepositoryModification.Insert)) {
                modifications = this._Modifications;
            } else {
                modifications = this._Modifications.SetItem(valueNext.Uid, PDRepositoryModification.Update);
            }
        } else {
            modifications = this._Modifications.SetItem(valueNext.Uid, PDRepositoryModification.Update);
        }

        var result = new PDRepository(
            repositoryKey: this._RepositoryKey,
            items: items,
            modifications: modifications);
        return new PDRepositoryResponse(result, valueNext);
    }

    public PDRepositoryResponse Delete(IPDObject value) {
        var valueNext = value.ClearRepositoryKey(this._RepositoryKey);
        var items = this._Items.Remove(valueNext.Uid);

        // thinkof
        ImmutableSortedDictionary<IPDObjectKey, PDRepositoryModification> modifications;
        if (this._Modifications.TryGetValue(valueNext.Uid, out var modification)) {
            if (modification.HasFlag(PDRepositoryModification.Insert)) {
                modifications = this._Modifications.SetItem(valueNext.Uid, PDRepositoryModification.Ignore);
            } else {
                modifications = this._Modifications.SetItem(valueNext.Uid, PDRepositoryModification.Delete);
            }
        } else {
            modifications = this._Modifications.SetItem(valueNext.Uid, PDRepositoryModification.Delete);
        }

        var result = new PDRepository(
            repositoryKey: this._RepositoryKey,
            items: items,
            modifications: modifications);
        return new PDRepositoryResponse(result, valueNext);
    }
}
