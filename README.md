# FastUlid
Fastest Universally Unique Lexicographically Sortable Identifier

## Sample code

```cs
var gen = new UlidGen();                    // create generator
var id = gen.Generate();                    // comparable item
var idNext = gen.Generate();                // next id is always greater: idNext > id
id.ToString();                              // not readable, but good to store
id.ToString('-');                           // more readable format
var dst = new byte[16]; id.CopyTo(dst, 0);  // binary store
```

## Summary

`UlidGen` instance is **not** thread safe!

Works perfect on single machine.
In case of having many machines you probably needs to add machine id.
See method `CopyTo`.

Structure `Ulid` works only with 16 length byte array.
Don't try to contsruct it yourself.
Thats done by `UlidGen.Generate` method.

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
 
 ```
 cd FastUlid.Bench
 dotnet build -c Release
 dotnet exec ./bin/Release/netcoreapp2.2/FastUlid.Bench.dll
 ```