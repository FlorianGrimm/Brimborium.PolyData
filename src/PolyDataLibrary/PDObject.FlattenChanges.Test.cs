namespace Brimborium.PolyData;

public partial class PDObjectTest {

    [Fact]
    public void FlattenChangesÍsFrozenTest() {
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
    public void FlattenChangesÍsUnfrozenTest() {
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
}