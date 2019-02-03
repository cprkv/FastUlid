using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Shouldly;
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
        ls[i].ShouldBe(l[i], $"{ls[i]} != {l[i]} at {i} element!");
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
        ls[i].ShouldBe(l[i].Encode(), $"{ls[i]} != {l[i]} at {i} element!");
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

      l.Distinct()
        .Count()
        .ShouldBe(l.Count);
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
      
      l.Distinct()
        .Count()
        .ShouldBe(l.Count);
    }

    [Theory]
    [MemberData(nameof(UlidCounts))]
    public void EncodeDecodeWithGuidTest(int c)
    {
      var gen = new UlidGen();

      for (var i = 0; i < c; i++)
      {
        var id = gen.Generate();
        Ulid
          .Decode(id.Encode())
          .ToGuid()
          .ShouldBe(id.ToGuid());
      }
    }

    [Fact]
    public void DecodeThrowsOnUnexpectedCharacterTest()
    {
      Should.Throw<ArgumentOutOfRangeException>(
        () => Ulid.Decode(null)
      );
      Should.Throw<ArgumentOutOfRangeException>(
        () => Ulid.Decode("")
      );
      Should.Throw<ArgumentOutOfRangeException>(
        () => Ulid.Decode("absdgg34tgga")
      );
      Should.Throw<ArgumentOutOfRangeException>(
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
        ulid.ShouldBe(l[i], $"{ulid} != {l[i]} at {i} element!");
      }
    }

    [Fact]
    public void MtTest()
    {
      var gen = new UlidGenTS();
      var cl = new ConcurrentStack<Ulid>();
      const int c = 10_000_000;

      var t1 = new Thread(
        () =>
        {
          var l = new List<Ulid>();
          for (var i = 0; i < c; i++)
          {
            l.Add(gen.Generate());
          }

          cl.PushRange(l.ToArray());
        }
      );
      var t2 = new Thread(
        () =>
        {
          var l = new List<Ulid>();
          for (var i = 0; i < c; i++)
          {
            l.Add(gen.Generate());
          }

          cl.PushRange(l.ToArray());
        }
      );
      var t3 = new Thread(
        () =>
        {
          var l = new List<Ulid>();
          for (var i = 0; i < c; i++)
          {
            l.Add(gen.Generate());
          }

          cl.PushRange(l.ToArray());
        }
      );
      t1.Start();
      t2.Start();
      t3.Start();
      t1.Join();
      t2.Join();
      t3.Join();
      
      cl.Count
        .ShouldBe(c * 3, "Threads PushRange failed. Maybe some threads failed execution");
      cl.Distinct()
        .Count()
        .ShouldBe(c * 3, "Some id's duplicated!");
    }
  }
}