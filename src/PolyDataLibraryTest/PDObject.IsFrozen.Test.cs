namespace Brimborium.PolyData;

public partial class PDObjectTest {
    [Fact]
    public void IsFrozenTest() {
        PDMetaPropertyNamed propertyA = new("A");
        PDMetaPropertyNamed propertyB = new("B");

        PGFlowInfo flowInfo = new();
        {
            var a1 = PDObject.Create(isFrozen: true);
            var a2f = a1.Freeze();
            var a3f = a2f.Freeze();
            var a4u = a3f.SetProperty(new PDSetPropertyRequest(propertyA, new PDValue("A"), flowInfo)).Result.Unfreeze();
            var a5u1 = a4u.Unfreeze();
            var a5u2 = a5u1.SetProperty(new PDSetPropertyRequest(propertyA, new PDValue("A"), flowInfo)).Result;
            var a6f = a5u2.Freeze();
            var a7f = a6f.Freeze();
            
            if (!ReferenceEquals(a2f, a3f)) { Assert.Fail("a2f is not the same as a3f"); }
            if (!ReferenceEquals(a4u, a5u1)) { Assert.Fail("a4u is not the same as a5u1"); }
            if (!ReferenceEquals(a5u1, a5u2)) { Assert.Fail("a5u1 is not the same as a5u2"); }
            if (!ReferenceEquals(a6f, a7f)) { Assert.Fail("a6f is not the same as a7f"); }
            Assert.Equal("A", a7f.GetProperty(propertyA).GetValue());
        }
        {
            var b1 = PDObject.Create(isFrozen: false);
            var b2f = b1.Freeze();
            var b3f = b2f.Freeze();
            var b4u = b3f.Unfreeze();
            var b5u = b4u.Unfreeze();
            var b6f = b5u.SetProperty(new PDSetPropertyRequest(propertyB, new PDValue("B"), flowInfo)).Result.Freeze();
            var b7f = b6f.Freeze();
            if (!ReferenceEquals(b2f, b3f)) { Assert.Fail("b2f is not the same as b3f"); }
            if (!ReferenceEquals(b4u, b5u)) { Assert.Fail("b4u is not the same as b5u"); }
            if (!ReferenceEquals(b6f, b7f)) { Assert.Fail("b6f is not the same as b7f"); }
            Assert.Equal("B", b7f.GetProperty(propertyB).GetValue());
        }
    }
}