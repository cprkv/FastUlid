using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace FastUlid.Bench
{
  public class Program
  {
    private static UlidGen _myUlidGen;

    [GlobalSetup]
    public void Setup()
    {
      _myUlidGen = new UlidGen();
    }

    [Benchmark]
    public Guid FastUlid_Version() => _myUlidGen.Generate().ToGuid();

    [Benchmark]
    public Guid NUlid_Version() => NUlid.Ulid.NewUlid().ToGuid();

    [Benchmark]
    public Guid SystemGuid_Version() => Guid.NewGuid();

    [Benchmark]
    public string UlidNet_Version() => UlidNet.NewUlid();

    static void Main(string[] args)
    {
      BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
  }
}