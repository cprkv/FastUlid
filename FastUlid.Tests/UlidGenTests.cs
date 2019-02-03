using System;
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
    public void SortEncodedTest(int c)
    {
      var gen = new UlidGen();
      var l = new List<Ulid>();
      var ls = new List<string>();

      for (var i = 0; i < c; i++)
      {
        l.Add(gen.Generate());
        ls.Add(l[i].Encode());
      }

      ls.Sort();

      for (var i = 0; i < l.Count; i++)
      {
        if (ls[i] != l[i].Encode())
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
    public void DistinctEncodedUniqueTest(int c)
    {
      var gen = new UlidGen();
      var l = new List<string>();

      for (var i = 0; i < c; i++)
      {
        l.Add(gen.Generate().Encode());
      }

      Assert.Equal(
        l.Count,
        l.Distinct().Count()
      );
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
      }

      Assert.Equal(
        l.Count,
        l.Distinct().Count()
      );
    }

    [Theory]
    [MemberData(nameof(UlidCounts))]
    public void EncodeDecodeWithGuidTest(int c)
    {
      var gen = new UlidGen();

      for (var i = 0; i < c; i++)
      {
        var id = gen.Generate();

        Assert.Equal(
          id.ToGuid(),
          Ulid.Decode(id.Encode()).ToGuid()
        );
      }
    }

    [Fact]
    public void DecodeThrowsOnUnexpectedCharacterTest()
    {
      Assert.Throws<ArgumentOutOfRangeException>(
        () => Ulid.Decode(null)
      );
      Assert.Throws<ArgumentOutOfRangeException>(
        () => Ulid.Decode("")
      );
      Assert.Throws<ArgumentOutOfRangeException>(
        () => Ulid.Decode("absdgg34tgga")
      );
      Assert.Throws<ArgumentOutOfRangeException>(
        () => Ulid.Decode("привет, мир!")
      );
    }

    [Theory]
    [MemberData(nameof(UlidCounts))]
    public void GuidTest(int c)
    {
      var gen = new UlidGen();
      var l = new List<Ulid>();
      var ls = new List<Guid>();

      for (var i = 0; i < c; i++)
      {
        l.Add(gen.Generate());
        ls.Add(l[i].ToGuid());
      }

      for (var i = 0; i < l.Count; i++)
      {
        var ulid = Ulid.FromGuid(ls[i]);

        if (l[i] != ulid)
        {
          Assert.False(
            true,
            $"{ulid} != {l[i]} at {i} element!"
          );
        }
      }
    }
  }
}