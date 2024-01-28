using BenchmarkDotNet.Running;
using NewWayOfCalling;
using System.Reflection;
using System.Runtime.CompilerServices;

BenchmarkRunner.Run<Benchmarks>();

//var example = new Example();

//// old way
//var method = typeof(Example).GetMethod("Method", BindingFlags.NonPublic | BindingFlags.Instance);
//Console.WriteLine(method.Invoke(example, null));

//// new way
//var name = Caller.CallMethod(example);
//Console.WriteLine(name);

//var field = Caller.GetField(example);
//Console.WriteLine(field);

//var propertyGet = Caller.GetProperty(example);
//Console.WriteLine(propertyGet);

public class Caller
{
    public static string PublicMethod()
    {
        return "Public Hello World";
    }

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "Method")]
    public static extern string CallMethod(Example example);

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "Field")]
    public static extern ref string GetField(Example example);

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_Property")]
    public static extern string GetProperty(Example example);
}

public class Example
{

    private string Field = "Taylor";

    private string Method()
    {
        return "Hello World";
    }

    private string Property { get; set; } = "Taylor";
}
