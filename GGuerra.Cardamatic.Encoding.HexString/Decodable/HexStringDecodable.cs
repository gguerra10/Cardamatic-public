using System;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;


namespace GGuerra.Cardamatic.Encoding.HexString.Decodable
{
    public class HexStringDecodable : IDecodable, IEncodable, IParseable
    {
        public Type Type => typeof(HexString);

        public bool TextLengthFixed => true;

        public FieldDecoder GetDecoder()
        {
            return DecodeHexString;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeHexString;
        }

        public FieldParser GetParser()
        {
            return ParseHexString;
        }

        private static object DecodeHexString(byte[] content, uint pointer, uint positionBits, uint lengthBytes, uint lengthBits)
        {
            var ret = string.Empty;
            var bytesReaded = 0u;
            while (bytesReaded < lengthBytes)
            {
                ret += content.GetSubValueUnsignedInteger(pointer + bytesReaded, 0, 8).ToString("X2");
                bytesReaded++;
            }

            return new HexString(ret);
        }

        private static byte[] EncodeHexString(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[dataSize];
            if (data is HexString hexString)
            {
                for (int i = 0; i < dataSize && i * 2 <= hexString.Value.Length - 2; i++)
                {
                    buffer[i] = Convert.ToByte(hexString.Value.Substring(i * 2, 2), 16);
                }
            }
            return buffer;
        }

        private static object ParseHexString(string content)
        {
            return new HexString(content);
        }
    }
}
