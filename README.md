# FastUlid
Fastest Universally Unique Lexicographically Sortable Identifier

## Benefits

A GUID/UUID can be suboptimal for many use-cases because:

 * It isn't the most character efficient way of encoding 128 bits
 * It provides no other information than randomness

A ULID however:

 * Is compatible with UUID/GUID's
 * 1,208,925,819,614,629,174,706,176 unique ULIDs per millisecond possible to be generated theoretically (in real benchmark ~12,000,000 ULIDs per second by one thread on modern cpu)
 * Lexicographically sortable
 * Canonically encoded as a 26 character string, as opposed to the 36 character UUID
 * Uses Crockford's base32 for better efficiency and readability (5 bits per character)
 * Case insensitive
 * No special characters (URL safe)

## Sample code

```cs
var gen = new UlidGen();                    // create generator
var id = gen.Generate();                    // comparable item
var idNext = gen.Generate();                // next id is always greater: idNext > id
id.ToString();                              // readable format
var dst = new byte[16]; id.CopyTo(dst, 0);  // binary store
```

### Serialization

 * Using string:
```cs
var encoded = id.Encode();                  // returns string, like 01D2RXZS981QZGSEYYFA4EQMRZ
var decoded = Ulid.Decode(encoded);         // returns Ulid
```

 * Using Guid (good for EntityFramework, Newtonsoft.Json, AspNet):
```cs
var encoded = id.ToGuid();                  // returns Guid
var decode = Ulid.FromGuid(encoded);        // returns Ulid
```

**Warning**: `Ulid.FromGuid` accepts only id's serialized from `Ulid` via `ToGuid` method!

## Summary

`UlidGen` instance is **not** thread safe!

Works perfect on single machine.
In case of having many machines you probably needs to add machine id.
See method `CopyTo`.

Structure `Ulid` works only with 16 length byte array.
If instance of `Ulid` not constructed by `UlidGen`, length validation should be done by clients.


## Benchmarks

 * Results on my machine:

``` ini
BenchmarkDotNet=v0.11.3, OS=centos 7
Intel Core i5-6400 CPU 2.70GHz (Skylake), 1 CPU, 4 logical and 4 physical cores
.NET Core SDK=2.2.103
  [Host]     : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
```

|             Method |         Mean |      Error |     StdDev |
|------------------- |-------------:|-----------:|-----------:|
|   FastUlid_Version |     82.43 ns |   1.187 ns |   1.110 ns |
|      NUlid_Version |    842.83 ns |   4.318 ns |   3.828 ns |
| SystemGuid_Version |  1,372.10 ns |   6.711 ns |   6.277 ns |
|    UlidNet_Version | 31,741.53 ns | 338.760 ns | 316.876 ns |


 * Run commands to benchmark:
 
```sh
cd FastUlid.Bench
dotnet build -c Release
dotnet exec ./bin/Release/netcoreapp2.2/FastUlid.Bench.dll
```

## Reference

This project is my adaptation of [skeeto/ulid-c](https://github.com/skeeto/ulid-c) for dotnet.