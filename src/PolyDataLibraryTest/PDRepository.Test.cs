namespace Brimborium.PolyData;
public class PDRepositoryTest {
    [Fact]
    public void PDRepository1Test() {
        var propertyA = new PDMetaPropertyNamed("A");
        var sut = new PDRepository();
        var objA1 = new PDObject();
        var sut1 = sut.Insert(objA1);
        var objA2 = objA1.SetProperty(new PDSetPropertyRequest(propertyA, new PDValue("A"))).Result;
    }
}
