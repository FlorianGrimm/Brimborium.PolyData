#pragma warning disable xUnit2013

namespace Brimborium.PolyData;

public partial class PDObjectTest
{

    [Fact]
    public void FlattenChangesIsFrozenTest()
    {
        PDMetaPropertyNamed propertyA = new("A");
        PDMetaPropertyNamed propertyB = new("B");

        PGFlowInfo flowInfo = new();
        PDObject a = PDObject.Create(isFrozen: true);
        var (aResponseIndicator, b) = a.SetProperty(new(propertyA, new PDValue("Hello"), flowInfo));
        if (aResponseIndicator is IPDFaultResponse) { Assert.Fail(aResponseIndicator.ToString()); }

        var (bResponseIndicator, c) = b.SetProperty(new(propertyB, new PDValue("World"), flowInfo));
        if (bResponseIndicator is IPDFaultResponse) { Assert.Fail(bResponseIndicator.ToString()); }

        Assert.NotNull(c);
        var result = c.GetFlattenChanges();
        Assert.Equal(2, result.Changes.Length);
    }

    [Fact]
    public void FlattenChangesIsUnfrozenTest()
    {
        PDMetaPropertyNamed propertyA = new("A");
        PDMetaPropertyNamed propertyB = new("B");

        PGFlowInfo flowInfo = new();
        PDObject a = PDObject.Create(isFrozen: false);
        var (aResponseIndicator, b) = a.SetProperty(new(propertyA, new PDValue("Hello"), flowInfo));
        if (aResponseIndicator is IPDFaultResponse) { Assert.Fail(aResponseIndicator.ToString()); }

        var (bResponseIndicator, c) = b.SetProperty(new(propertyB, new PDValue("World"), flowInfo));
        if (bResponseIndicator is IPDFaultResponse) { Assert.Fail(bResponseIndicator.ToString()); }

        Assert.NotNull(c);
        var result = c.GetFlattenChanges();
        Assert.Equal(2, result.Changes.Length);
    }

    [Fact]
    public void FlattenChangesIsUnfrozen2Test()
    {
        PDMetaPropertyNamed propertyA = new("A");
        PDMetaPropertyNamed propertyB = new("B");

        PGFlowInfo flowInfo = new();
        PDObject a = PDObject.Create(isFrozen: false);
        var (_, b) = a.SetProperty(new(propertyA, new PDValue("Hello"), flowInfo));
        var (_, c) = b.SetProperty(new(propertyB, new PDValue("World"), flowInfo));
        var (_, d) = c.SetProperty(new(propertyB, new PDValue("Universe"), flowInfo));

        var result1 = d.GetFlattenChanges();
        Assert.Equal(3, result1.Changes.Length);
        var e = result1.NextInstance;

        var (_, f) = e.SetProperty(new(propertyA, new PDValue("Hallo"), flowInfo));
        var result2 = f.GetFlattenChanges();
        Assert.Equal(1, result2.Changes.Length);

    }
}