using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Brimborium.PolyData;
[MemoryDiagnoser]
public class Bench1 {
    [Benchmark]
    public void Runs1() {
        var propertyA = new PDMetaPropertyNamed("A");
        var propertyB = new PDMetaPropertyNamed("B");
        IPDRepository repository = new PDRepository();
        PGFlowInfo flowInfo = new();

        var value = 1.ToString();
        IPDObject obj1 = (new PDObject())
            .SetProperty(flowInfo.SetPropertyRequest(propertyA, value)).Result
            .SetProperty(flowInfo.SetPropertyRequest(propertyB, value)).Result
            ;

        (repository, obj1) = repository.Add(obj1);
    }

    [Benchmark]
    public void Runs2() {
        var propertyA = new PDMetaPropertyNamed("A");
        var propertyB = new PDMetaPropertyNamed("B");
        IPDRepository repository = new PDRepository();
        PGFlowInfo flowInfo = new();

        for (int index = 0; index < 100; index++) {
            var value = index.ToString();
            IPDObject obj1 = (new PDObject())
                .SetProperty(flowInfo.SetPropertyRequest(propertyA, value)).Result
                .SetProperty(flowInfo.SetPropertyRequest(propertyB, value)).Result
                ;
            (repository, obj1) = repository.Add(obj1);
        }
    }
}
    public class Program {
    public static void Main(string[] args) {
        BenchmarkRunner.Run<Bench1>(null, args);
    }
}

/*
  
dotnet run -c Release -- --warmupCount 1 --minIterationCount 1 --maxIterationCount 2

| Method | Mean       | Error        | StdDev    | Gen0    | Gen1   | Allocated |
|------- |-----------:|-------------:|----------:|--------:|-------:|----------:|
| Runs1  |   1.501 us |    19.980 us | 0.0444 us |  0.2708 |      - |   1.11 KB |
| Runs2  | 152.375 us | 1,147.694 us | 2.5495 us | 31.7383 | 0.2441 | 129.86 KB |
 
 */