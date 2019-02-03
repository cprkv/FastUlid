using System;

// ReSharper disable ClassCanBeSealed.Global

namespace FastUlid
{
  public struct Ulid : IComparable<Ulid>, IEquatable<Ulid>
  {
    private const int Count = 16;
    private readonly byte[] _bytes;

    /// <param name="bytes">Array of 16 bytes.</param>
    public Ulid(byte[] bytes)
    {
      _bytes = new byte[16];
      bytes.CopyTo(_bytes, 0);
    }

    public int CompareTo(Ulid other)
    {
      for (var i = 0; i < Count; i++)
      {
        var diff = _bytes[i].CompareTo(other._bytes[i]);
        if (diff != 0)
          return diff;
      }

      return 0;
    }

    public override string ToString()
    {
      const char divider = '-';
      var str = new char[32];

      str[0] = (char) CharToStr[_bytes[0] >> 5];
      str[1] = (char) CharToStr[_bytes[0] >> 0];
      str[2] = (char) CharToStr[_bytes[1] >> 3];
      str[3] = (char) CharToStr[(_bytes[1] << 2 | _bytes[2] >> 6) & 0x1f];
      str[4] = divider;
      str[5] = (char) CharToStr[_bytes[2] >> 1];
      str[6] = (char) CharToStr[(_bytes[2] << 4 | _bytes[3] >> 4) & 0x1f];
      str[7] = (char) CharToStr[(_bytes[3] << 1 | _bytes[4] >> 7) & 0x1f];
      str[8] = (char) CharToStr[_bytes[4] >> 2];
      str[9] = divider;
      str[10] = (char) CharToStr[(_bytes[4] << 3 | _bytes[5] >> 5) & 0x1f];
      str[11] = (char) CharToStr[_bytes[5] >> 0];
      str[12] = (char) CharToStr[_bytes[6] >> 3];
      str[13] = (char) CharToStr[(_bytes[6] << 2 | _bytes[7] >> 6) & 0x1f];
      str[14] = divider;
      str[15] = (char) CharToStr[_bytes[7] >> 1];
      str[16] = (char) CharToStr[(_bytes[7] << 4 | _bytes[8] >> 4) & 0x1f];
      str[17] = (char) CharToStr[(_bytes[8] << 1 | _bytes[9] >> 7) & 0x1f];
      str[18] = (char) CharToStr[_bytes[9] >> 2];
      str[19] = divider;
      str[20] = (char) CharToStr[(_bytes[9] << 3 | _bytes[10] >> 5) & 0x1f];
      str[21] = (char) CharToStr[_bytes[10] >> 0];
      str[22] = (char) CharToStr[_bytes[11] >> 3];
      str[23] = (char) CharToStr[(_bytes[11] << 2 | _bytes[12] >> 6) & 0x1f];
      str[24] = divider;
      str[25] = (char) CharToStr[_bytes[12] >> 1];
      str[26] = (char) CharToStr[(_bytes[12] << 4 | _bytes[13] >> 4) & 0x1f];
      str[27] = (char) CharToStr[(_bytes[13] << 1 | _bytes[14] >> 7) & 0x1f];
      str[28] = (char) CharToStr[_bytes[14] >> 2];
      str[29] = divider;
      str[30] = (char) CharToStr[(_bytes[14] << 3 | _bytes[15] >> 5) & 0x1f];
      str[31] = (char) CharToStr[_bytes[15] >> 0];

      return new string(str);
    }

    public static bool operator !=(Ulid a, Ulid b)
    {
      for (var i = 0; i < Count; i++)
        if (a._bytes[i] != b._bytes[i])
          return true;
      return false;
    }

    public static bool operator ==(Ulid a, Ulid b)
    {
      for (var i = 0; i < Count; i++)
        if (a._bytes[i] != b._bytes[i])
          return false;
      return true;
    }

    public bool Equals(Ulid other)
    {
      for (var i = 0; i < 16; i++)
        if (_bytes[i] != other._bytes[i])
          return false;
      return true;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      return obj is Ulid other && Equals(other);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = 123;
        for (var i = 0; i < Count; i++)
          hashCode = hashCode * 23 + _bytes[i].GetHashCode();
        return hashCode;
      }
    }

    public void CopyTo(byte[] array, int index)
    {
      _bytes.CopyTo(array, index);
    }

    public Guid ToGuid()
    {
      return new Guid(_bytes);
    }

    public static Ulid FromGuid(Guid id)
    {
      return new Ulid(id.ToByteArray());
    }

    public string Encode()
    {
      var str = new char[26];

      str[0] = (char) CharToStr[_bytes[0] >> 5];
      str[1] = (char) CharToStr[_bytes[0] >> 0];
      str[2] = (char) CharToStr[_bytes[1] >> 3];
      str[3] = (char) CharToStr[(_bytes[1] << 2 | _bytes[2] >> 6) & 0x1f];
      str[4] = (char) CharToStr[_bytes[2] >> 1];
      str[5] = (char) CharToStr[(_bytes[2] << 4 | _bytes[3] >> 4) & 0x1f];
      str[6] = (char) CharToStr[(_bytes[3] << 1 | _bytes[4] >> 7) & 0x1f];
      str[7] = (char) CharToStr[_bytes[4] >> 2];
      str[8] = (char) CharToStr[(_bytes[4] << 3 | _bytes[5] >> 5) & 0x1f];
      str[9] = (char) CharToStr[_bytes[5] >> 0];
      str[10] = (char) CharToStr[_bytes[6] >> 3];
      str[11] = (char) CharToStr[(_bytes[6] << 2 | _bytes[7] >> 6) & 0x1f];
      str[12] = (char) CharToStr[_bytes[7] >> 1];
      str[13] = (char) CharToStr[(_bytes[7] << 4 | _bytes[8] >> 4) & 0x1f];
      str[14] = (char) CharToStr[(_bytes[8] << 1 | _bytes[9] >> 7) & 0x1f];
      str[15] = (char) CharToStr[_bytes[9] >> 2];
      str[16] = (char) CharToStr[(_bytes[9] << 3 | _bytes[10] >> 5) & 0x1f];
      str[17] = (char) CharToStr[_bytes[10] >> 0];
      str[18] = (char) CharToStr[_bytes[11] >> 3];
      str[19] = (char) CharToStr[(_bytes[11] << 2 | _bytes[12] >> 6) & 0x1f];
      str[20] = (char) CharToStr[_bytes[12] >> 1];
      str[21] = (char) CharToStr[(_bytes[12] << 4 | _bytes[13] >> 4) & 0x1f];
      str[22] = (char) CharToStr[(_bytes[13] << 1 | _bytes[14] >> 7) & 0x1f];
      str[23] = (char) CharToStr[_bytes[14] >> 2];
      str[24] = (char) CharToStr[(_bytes[14] << 3 | _bytes[15] >> 5) & 0x1f];
      str[25] = (char) CharToStr[_bytes[15] >> 0];

      return new string(str); // ?? string.FastAllocateString  >  (char*) &str;  >  change bytes
    }

    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static Ulid Decode(string str)
    {
      if (str == null)
        throw new ArgumentOutOfRangeException(nameof(str), "Input string cannot be null");
      if (str.Length != 26)
        throw new ArgumentOutOfRangeException(nameof(str), "Input string should be 26 characters length");
      for (var i = 0; i < 26; i++)
        if (str[i] > CharFromStr.Length || CharFromStr[str[i]] == -1)
          throw new ArgumentOutOfRangeException(nameof(str), $"Invalid character at position {i}: '{str[i]}'");

      var bytes = new byte[Count];

      bytes[0] = (byte) (CharFromStr[str[0]] << 5 | CharFromStr[str[1]] >> 0);
      bytes[1] = (byte) (CharFromStr[str[2]] << 3 | CharFromStr[str[3]] >> 2);
      bytes[2] = (byte) (CharFromStr[str[3]] << 6 | CharFromStr[str[4]] << 1 | CharFromStr[str[5]] >> 4);
      bytes[3] = (byte) (CharFromStr[str[5]] << 4 | CharFromStr[str[6]] >> 1);
      bytes[4] = (byte) (CharFromStr[str[6]] << 7 | CharFromStr[str[7]] << 2 | CharFromStr[str[8]] >> 3);
      bytes[5] = (byte) (CharFromStr[str[8]] << 5 | CharFromStr[str[9]] >> 0);
      bytes[6] = (byte) (CharFromStr[str[10]] << 3 | CharFromStr[str[11]] >> 2);
      bytes[7] = (byte) (CharFromStr[str[11]] << 6 | CharFromStr[str[12]] << 1 | CharFromStr[str[13]] >> 4);
      bytes[8] = (byte) (CharFromStr[str[13]] << 4 | CharFromStr[str[14]] >> 1);
      bytes[9] = (byte) (CharFromStr[str[14]] << 7 | CharFromStr[str[15]] << 2 | CharFromStr[str[16]] >> 3);
      bytes[10] = (byte) (CharFromStr[str[16]] << 5 | CharFromStr[str[17]] >> 0);
      bytes[11] = (byte) (CharFromStr[str[18]] << 3 | CharFromStr[str[19]] >> 2);
      bytes[12] = (byte) (CharFromStr[str[19]] << 6 | CharFromStr[str[20]] << 1 | CharFromStr[str[21]] >> 4);
      bytes[13] = (byte) (CharFromStr[str[21]] << 4 | CharFromStr[str[22]] >> 1);
      bytes[14] = (byte) (CharFromStr[str[22]] << 7 | CharFromStr[str[23]] << 2 | CharFromStr[str[24]] >> 3);
      bytes[15] = (byte) (CharFromStr[str[24]] << 5 | CharFromStr[str[25]] >> 0);

      return new Ulid(bytes);
    }

    private static readonly byte[] CharToStr =
    {
      0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
      0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46,
      0x47, 0x48, 0x4a, 0x4b, 0x4d, 0x4e, 0x50, 0x51,
      0x52, 0x53, 0x54, 0x56, 0x57, 0x58, 0x59, 0x5a,
      0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
      0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46,
      0x47, 0x48, 0x4a, 0x4b, 0x4d, 0x4e, 0x50, 0x51,
      0x52, 0x53, 0x54, 0x56, 0x57, 0x58, 0x59, 0x5a,
      0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
      0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46,
      0x47, 0x48, 0x4a, 0x4b, 0x4d, 0x4e, 0x50, 0x51,
      0x52, 0x53, 0x54, 0x56, 0x57, 0x58, 0x59, 0x5a,
      0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
      0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46,
      0x47, 0x48, 0x4a, 0x4b, 0x4d, 0x4e, 0x50, 0x51,
      0x52, 0x53, 0x54, 0x56, 0x57, 0x58, 0x59, 0x5a,
      0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
      0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46,
      0x47, 0x48, 0x4a, 0x4b, 0x4d, 0x4e, 0x50, 0x51,
      0x52, 0x53, 0x54, 0x56, 0x57, 0x58, 0x59, 0x5a,
      0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
      0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46,
      0x47, 0x48, 0x4a, 0x4b, 0x4d, 0x4e, 0x50, 0x51,
      0x52, 0x53, 0x54, 0x56, 0x57, 0x58, 0x59, 0x5a,
      0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
      0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46,
      0x47, 0x48, 0x4a, 0x4b, 0x4d, 0x4e, 0x50, 0x51,
      0x52, 0x53, 0x54, 0x56, 0x57, 0x58, 0x59, 0x5a,
      0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
      0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46,
      0x47, 0x48, 0x4a, 0x4b, 0x4d, 0x4e, 0x50, 0x51,
      0x52, 0x53, 0x54, 0x56, 0x57, 0x58, 0x59, 0x5a
    };

    private static readonly int[] CharFromStr =
    {
      -1, -1, -1, -1, -1, -1, -1, -1,
      -1, -1, -1, -1, -1, -1, -1, -1,
      -1, -1, -1, -1, -1, -1, -1, -1,
      -1, -1, -1, -1, -1, -1, -1, -1,
      -1, -1, -1, -1, -1, -1, -1, -1,
      -1, -1, -1, -1, -1, -1, -1, -1,
      0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
      0x08, 0x09, -1, -1, -1, -1, -1, -1,
      -1, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x10,
      0x11, 0x01, 0x12, 0x13, 0x01, 0x14, 0x15, 0x00,
      0x16, 0x17, 0x18, 0x19, 0x1a, -1, 0x1b, 0x1c,
      0x1d, 0x1e, 0x1f, -1, -1, -1, -1, -1,
      -1, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x10,
      0x11, 0x01, 0x12, 0x13, 0x01, 0x14, 0x15, 0x00,
      0x16, 0x17, 0x18, 0x19, 0x1a, -1, 0x1b, 0x1c,
      0x1d, 0x1e, 0x1f
    };
  }
}