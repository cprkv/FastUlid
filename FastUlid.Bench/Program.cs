using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace FastUlid.Bench
{
  public class Program
  {
    private static UlidGen _myUlidGen;
    private static UlidGenTS _myUlidGenTS;

    [GlobalSetup]
    public void Setup()
    {
      _myUlidGen = new UlidGen();
      _myUlidGenTS = new UlidGenTS();
    }

    [Benchmark(Description = "FastUlid")]
    public Guid FastUlidV() => _myUlidGen.Generate().ToGuid();
    
    [Benchmark(Description = "FastUlid thread safe")]
    public Guid ThreadSafe_FastUlidV() => _myUlidGenTS.Generate().ToGuid();

    [Benchmark(Description = "NUlid")]
    public Guid NUlidV() => NUlid.Ulid.NewUlid().ToGuid();

    [Benchmark(Description = "System.Guid")]
    public Guid SystemGuidV() => Guid.NewGuid();

    [Benchmark(Description = "Ulid.Net")]
    public string UlidNetV() => UlidNet.NewUlid();

    static void Main(string[] args)
    {
      BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
  }
}