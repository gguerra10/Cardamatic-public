using System;
using System.Linq;

namespace GGuerra.Cardamatic.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToByteArray(this string hexData)
        {
            return Enumerable.Range(0, hexData.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hexData.Substring(x, 2), 16))
                .ToArray();
        }

    }
}
