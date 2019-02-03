using System;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;

namespace FastUlid
{
  public static class Functions
  {
    internal static ulong UTime()
    {
      var hFt1 = DateTime.UtcNow.ToFileTimeUtc();
      var ft = new FILETIME
      {
        dwLowDateTime = (int) (hFt1 & 0xFFFFFFFF),
        dwHighDateTime = (int) (hFt1 >> 32)
      };
      return ((ulong) ft.dwHighDateTime << 32 |
              (ulong) ft.dwLowDateTime << 0)
             / 10 - 11644473600000000UL;
    }

    internal static byte[] Entropy(int len)
    {
      var result = new byte[len];
      //var rand = new Random();
      //rand.NextBytes(result);
      RandomNumberGenerator.Create().GetBytes(result, 0, len);
      return result;
    }
  }
}