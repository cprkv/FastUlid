using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable ClassCanBeSealed.Global

namespace FastUlid.Tests
{
  public class UlidGenTests
  {
    private readonly ITestOutputHelper _output;

    public UlidGenTests(ITestOutputHelper output)
    {
      _output = output;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static IEnumerable<object[]> UlidCounts()
    {
      var i = 10;
      for (var step = 0; step < 5; step++)
      {
        i *= 10;
        yield return new object[] {i};
      }
    }

    [Theory]
    [MemberData(nameof(UlidCounts))]
    public void SortTest(int c)
    {
      var gen = new UlidGen();
      var l = new List<Ulid>();
      var ls = new List<Ulid>();

      for (var i = 0; i < c; i++)
      {
        l.Add(gen.Generate());
        ls.Add(l[i]);
      }

      ls.Sort();

      for (var i = 0; i < l.Count; i++)
      {
        if (ls[i] != l[i])
        {
          Assert.False(
            true,
            $"{ls[i]} != {l[i]} at {i} element!"
          );
        }
      }
    }

    [Theory]
    [MemberData(nameof(UlidCounts))]
    public void SortStrTest(int c)
    {
      var gen = new UlidGen();
      var l = new List<Ulid>();
      var ls = new List<string>();

      for (var i = 0; i < c; i++)
      {
        l.Add(gen.Generate());
        ls.Add(l[i].ToString());
      }

      ls.Sort();

      for (var i = 0; i < l.Count; i++)
      {
        if (ls[i] != l[i].ToString())
        {
          Assert.False(
            true,
            $"{ls[i]} != {l[i]} at {i} element!"
          );
        }
      }
    }

    [Theory]
    [MemberData(nameof(UlidCounts))]
    public void DistinctUniqueTest(int c)
    {
      var gen = new UlidGen();
      var l = new List<Ulid>();

      for (var i = 0; i < c; i++)
      {
        l.Add(gen.Generate());
        _output.WriteLine(l[i].ToString('-'));
      }

      Assert.Equal(
        l.Count,
        l.Distinct().Count()
      );
    }
  }
}