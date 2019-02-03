namespace FastUlid
{
  /// <summary>
  /// ULID generator.
  /// </summary>
  public sealed class UlidGen : IUlidGen
  {
    private readonly byte[] _s;
    private readonly byte[] _last;
    private ulong _lastTs;
    private byte _i, _j;

    public UlidGen()
    {
      _last = new byte[16];
      _s = new byte[256];
      _lastTs = 0;
      _i = _j = 0;

      for (var i = 0; i < 256; i++)
        _s[i] = (byte) i;

      var key = Functions.Entropy(256);

      for (int i = 0, j = 0; i < 256; i++)
      {
        j = (j + _s[i] + key[i]) & 0xff;
        var tmp = _s[i];
        _s[i] = _s[j];
        _s[j] = tmp;
      }
    }

    public Ulid Generate()
    {
      var ts = Functions.UTime() / 1000;
      return GenerateInternal(ts);
    }

    internal Ulid GenerateInternal(ulong ts)
    {
      if (_lastTs == ts)
      {
        for (var i = 15; i > 5; i--)
          if (++_last[i] != 0)
            break;
      }
      else
      {
        _lastTs = ts;
        _last[0] = (byte) (ts >> 40);
        _last[1] = (byte) (ts >> 32);
        _last[2] = (byte) (ts >> 24);
        _last[3] = (byte) (ts >> 16);
        _last[4] = (byte) (ts >> 8);
        _last[5] = (byte) (ts >> 0);

        for (var k = 0; k < 10; k++)
        {
          _i = (byte) ((_i + 1) & 0xff);
          _j = (byte) ((_j + _s[_i]) & 0xff);
          var tmp = _s[_i];
          _s[_i] = _s[_j];
          _s[_j] = tmp;
          _last[6 + k] = _s[(_s[_i] + _s[_j]) & 0xff];
        }
      }
      return new Ulid(_last);
    }
  }
}