namespace Brimborium.PolyData;

public partial class PDObjectTest {
    [Fact]
    public void PropertyÍsFrozenTest() {
        PDMetaPropertyNamed propertyA = new("A");
        PDMetaPropertyNamed propertyB = new("B");

        PGFlowInfo flowInfo = new();
        PDObject a = PDObject.Create(isFrozen:true);
        var (aResponseIndicator, b) = a.SetProperty(new(propertyA, new PDValue("Hello"), flowInfo));
        if (aResponseIndicator is IPDFaultResponse) { Assert.Fail(aResponseIndicator.ToString()); }

        var (bResponseIndicator, c) = b.SetProperty(new(propertyB, new PDValue("World"), flowInfo));
        if (bResponseIndicator is IPDFaultResponse) { Assert.Fail(bResponseIndicator.ToString()); }

        Assert.NotNull(c);
        if (ReferenceEquals(a, b)) { Assert.Fail("a is the same as b"); }
        if (ReferenceEquals(a, c)) { Assert.Fail("a is the same as c"); }
        if (ReferenceEquals(b, c)) { Assert.Fail("b is the same as c"); }
    }

    [Fact]
    public void PropertyÍsUnfrozenTest() {
        PDMetaPropertyNamed propertyA = new("A");
        PDMetaPropertyNamed propertyB = new("B");

        PGFlowInfo flowInfo = new();
        PDObject a = PDObject.Create(isFrozen: false);
        var (aResponseIndicator, b) = a.SetProperty(new(propertyA, new PDValue("Hello"), flowInfo));
        if (aResponseIndicator is IPDFaultResponse) { Assert.Fail(aResponseIndicator.ToString()); }

        var (bResponseIndicator, c) = b.SetProperty(new(propertyB, new PDValue("World"), flowInfo));
        if (bResponseIndicator is IPDFaultResponse) { Assert.Fail(bResponseIndicator.ToString()); }

        Assert.NotNull(c);
        if (!ReferenceEquals(a, b)) { Assert.Fail("a is not the same as b"); }
        if (!ReferenceEquals(a, c)) { Assert.Fail("a is not the same as c"); }
        if (!ReferenceEquals(b, c)) { Assert.Fail("b is not the same as c"); }
    }
}