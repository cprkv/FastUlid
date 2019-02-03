namespace FastUlid
{
  /// <summary>
  /// ThreadSafe ULID generator.
  /// </summary>
  public sealed class UlidGenTS : IUlidGen
  {
    private static readonly object SyncRoot = new object();
    private readonly UlidGen _gen;

    public UlidGenTS()
    {
      _gen = new UlidGen();
    }

    public Ulid Generate()
    {
      var ts = Functions.UTime() / 1000;
      lock (SyncRoot)
      {
        return _gen.GenerateInternal(ts);
      }
    }
  }
}