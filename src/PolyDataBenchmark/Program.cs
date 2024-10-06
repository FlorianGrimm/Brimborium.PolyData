using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Brimborium.PolyData;
[MemoryDiagnoser]
public class Bench1 {
    [Benchmark]
    public void Runs1() {
        RunN(1);
    }

    [Benchmark]
    public void Runs10() {
        RunN(10);
    }

    [Benchmark]
    public void Runs100() {
        RunN(100);
    }

    [Benchmark]
    public void Runs1000() {
        RunN(1000);
    }

    private void RunN(int count) {
        var propertyA = new PDMetaPropertyNamed<string>("A");
        var propertyB = new PDMetaPropertyNamed<string>("B");
        IPDRepository repository = new PDRepository();
        PGFlowInfo flowInfo = new();

        for (int index = 0; index < count; index++) {
            var value = index.ToString();
            IPDObject obj1 = (PDObject.Create())
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


| Method | Mean       | Error        | StdDev    | Gen0    | Gen1   | Allocated |
|------- |-----------:|-------------:|----------:|--------:|-------:|----------:|
| Runs1  |   1.524 us |     7.719 us | 0.0171 us |  0.2136 |      - |     896 B |
| Runs2  | 149.489 us | 1,697.851 us | 3.7717 us | 25.8789 | 0.2441 |  108962 B |
 

| Method   | Mean         | Error         | StdDev     | Gen0     | Gen1     | Allocated |
|--------- |-------------:|--------------:|-----------:|---------:|---------:|----------:|
| Runs1    |     1.500 us |     13.701 us |  0.0304 us |   0.2060 |        - |     864 B |
| Runs10   |    11.222 us |    105.092 us |  0.2335 us |   2.0752 |        - |    8702 B |
| Runs100  |   146.786 us |    493.193 us |  1.0956 us |  25.1465 |        - |  105779 B |
| Runs1000 | 2,125.350 us | 30,032.389 us | 66.7152 us | 250.0000 | 171.8750 | 1293099 B |


 */