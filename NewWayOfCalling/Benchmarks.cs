using BenchmarkDotNet.Attributes;
using System.Reflection;

namespace NewWayOfCalling;

[MemoryDiagnoser(false)]
public class Benchmarks
{
    private readonly Example _example = new();

    private static readonly MethodInfo CachedMethod = typeof(Example)
        .GetMethod("Method", BindingFlags.NonPublic | BindingFlags.Instance)!;

    [Benchmark]
    public string GetMethod_Reflection()
    {
        return (string)_example.GetType()
            .GetMethod("Method", BindingFlags.NonPublic | BindingFlags.Instance)!
            .Invoke(_example, null)!;
    }

    [Benchmark]
    public string GetMethod_ReflectionCached()
    {
        return (string)CachedMethod.Invoke(_example, null)!;
    }

    [Benchmark]
    public string GetMethod_Unsafe()
    {
        return Caller.CallMethod(_example);
    }

    [Benchmark]
    public string GetMethod_Direct()
    {
        return Caller.PublicMethod();
    }
}
