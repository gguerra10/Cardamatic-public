using System;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;

namespace GGuerra.Cardamatic.Encoding.Checksum.Decodable
{
    public class ChecksumDecodable : IDecodable, IEncodable
    {
        public Type Type => typeof(Checksum);

        public bool TextLengthFixed => false;

        public FieldDecoder GetDecoder()
        {
            return DecodeChecksum;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeChecksum;
        }

        private static byte[] EncodeChecksum(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[dataSize];
            if (data is byte[] dataArray)
            {
                Array.Copy(dataArray, buffer, dataSize);
            }
            return new byte[] { CalculateChecksum(buffer) };
        }

        private static object DecodeChecksum(byte[] content, uint pointer, uint positionBits, uint lengthBytes, uint lengthBits)
        {
            var ret = string.Empty;
            var bytesReaded = 0u;
            while (bytesReaded < lengthBytes)
            {
                ret += content.GetSubValueUnsignedInteger(pointer + bytesReaded, 0, 8).ToString("X2");
                bytesReaded++;
            }

            return new Checksum(ret);
        }

        private static byte CalculateChecksum(byte[] bytes)
        {
            byte checksum = 0xAA;
            foreach (byte b in bytes)
            {
                checksum ^= b;
            }
            return checksum;
        }
    }
}
