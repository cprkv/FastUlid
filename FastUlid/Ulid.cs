using System;

// ReSharper disable ClassCanBeSealed.Global

namespace FastUlid
{
  public struct Ulid : IComparable<Ulid>, IEquatable<Ulid>
  {
    private readonly byte[] _bytes;

    public Ulid(byte[] bytes)
    {
      _bytes = new byte[16];
      bytes.CopyTo(_bytes, 0);
    }

    public int CompareTo(Ulid other)
    {
      for (var i = 0; i < 16; i++)
      {
        var diff = _bytes[i].CompareTo(other._bytes[i]);
        if (diff != 0)
          return diff;
      }

      return 0;
    }

    private static readonly byte[] CharsetToString =
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

    public override string ToString()
    {
      var str = new char[26];

      str[0] = (char) CharsetToString[_bytes[0] >> 5];
      str[1] = (char) CharsetToString[_bytes[0] >> 0];
      str[2] = (char) CharsetToString[_bytes[1] >> 3];
      str[3] = (char) CharsetToString[(_bytes[1] << 2 | _bytes[2] >> 6) & 0x1f];
      str[4] = (char) CharsetToString[_bytes[2] >> 1];
      str[5] = (char) CharsetToString[(_bytes[2] << 4 | _bytes[3] >> 4) & 0x1f];
      str[6] = (char) CharsetToString[(_bytes[3] << 1 | _bytes[4] >> 7) & 0x1f];
      str[7] = (char) CharsetToString[_bytes[4] >> 2];
      str[8] = (char) CharsetToString[(_bytes[4] << 3 | _bytes[5] >> 5) & 0x1f];
      str[9] = (char) CharsetToString[_bytes[5] >> 0];
      str[10] = (char) CharsetToString[_bytes[6] >> 3];
      str[11] = (char) CharsetToString[(_bytes[6] << 2 | _bytes[7] >> 6) & 0x1f];
      str[12] = (char) CharsetToString[_bytes[7] >> 1];
      str[13] = (char) CharsetToString[(_bytes[7] << 4 | _bytes[8] >> 4) & 0x1f];
      str[14] = (char) CharsetToString[(_bytes[8] << 1 | _bytes[9] >> 7) & 0x1f];
      str[15] = (char) CharsetToString[_bytes[9] >> 2];
      str[16] = (char) CharsetToString[(_bytes[9] << 3 | _bytes[10] >> 5) & 0x1f];
      str[17] = (char) CharsetToString[_bytes[10] >> 0];
      str[18] = (char) CharsetToString[_bytes[11] >> 3];
      str[19] = (char) CharsetToString[(_bytes[11] << 2 | _bytes[12] >> 6) & 0x1f];
      str[20] = (char) CharsetToString[_bytes[12] >> 1];
      str[21] = (char) CharsetToString[(_bytes[12] << 4 | _bytes[13] >> 4) & 0x1f];
      str[22] = (char) CharsetToString[(_bytes[13] << 1 | _bytes[14] >> 7) & 0x1f];
      str[23] = (char) CharsetToString[_bytes[14] >> 2];
      str[24] = (char) CharsetToString[(_bytes[14] << 3 | _bytes[15] >> 5) & 0x1f];
      str[25] = (char) CharsetToString[_bytes[15] >> 0];

      return new string(str);
    }

    public string ToString(char divider)
    {
      var str = new char[32];

      str[0] = (char) CharsetToString[_bytes[0] >> 5];
      str[1] = (char) CharsetToString[_bytes[0] >> 0];
      str[2] = (char) CharsetToString[_bytes[1] >> 3];
      str[3] = (char) CharsetToString[(_bytes[1] << 2 | _bytes[2] >> 6) & 0x1f];
      str[4] = divider;
      str[5] = (char) CharsetToString[_bytes[2] >> 1];
      str[6] = (char) CharsetToString[(_bytes[2] << 4 | _bytes[3] >> 4) & 0x1f];
      str[7] = (char) CharsetToString[(_bytes[3] << 1 | _bytes[4] >> 7) & 0x1f];
      str[8] = (char) CharsetToString[_bytes[4] >> 2];
      str[9] = divider;
      str[10] = (char) CharsetToString[(_bytes[4] << 3 | _bytes[5] >> 5) & 0x1f];
      str[11] = (char) CharsetToString[_bytes[5] >> 0];
      str[12] = (char) CharsetToString[_bytes[6] >> 3];
      str[13] = (char) CharsetToString[(_bytes[6] << 2 | _bytes[7] >> 6) & 0x1f];
      str[14] = divider;
      str[15] = (char) CharsetToString[_bytes[7] >> 1];
      str[16] = (char) CharsetToString[(_bytes[7] << 4 | _bytes[8] >> 4) & 0x1f];
      str[17] = (char) CharsetToString[(_bytes[8] << 1 | _bytes[9] >> 7) & 0x1f];
      str[18] = (char) CharsetToString[_bytes[9] >> 2];
      str[19] = divider;
      str[20] = (char) CharsetToString[(_bytes[9] << 3 | _bytes[10] >> 5) & 0x1f];
      str[21] = (char) CharsetToString[_bytes[10] >> 0];
      str[22] = (char) CharsetToString[_bytes[11] >> 3];
      str[23] = (char) CharsetToString[(_bytes[11] << 2 | _bytes[12] >> 6) & 0x1f];
      str[24] = divider;
      str[25] = (char) CharsetToString[_bytes[12] >> 1];
      str[26] = (char) CharsetToString[(_bytes[12] << 4 | _bytes[13] >> 4) & 0x1f];
      str[27] = (char) CharsetToString[(_bytes[13] << 1 | _bytes[14] >> 7) & 0x1f];
      str[28] = (char) CharsetToString[_bytes[14] >> 2];
      str[29] = divider;
      str[30] = (char) CharsetToString[(_bytes[14] << 3 | _bytes[15] >> 5) & 0x1f];
      str[31] = (char) CharsetToString[_bytes[15] >> 0];

      return new string(str);
    }

    public static bool operator !=(Ulid a, Ulid b)
    {
      return !(a == b);
    }

    public static bool operator ==(Ulid a, Ulid b)
    {
      for (var i = 0; i < 16; i++)
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
        for (var i = 0; i < 16; i++) 
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
  }
}