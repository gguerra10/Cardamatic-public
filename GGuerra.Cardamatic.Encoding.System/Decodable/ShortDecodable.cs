using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;
using System;


namespace GGuerra.Cardamatic.Encoding.System.Decodable
{
    public class ShortDecodable : IDecodable, IEncodable, IParseable
    {
        public Type Type => typeof(ushort);

        public bool TextLengthFixed => false;

        public FieldDecoder GetDecoder()
        {
            return DecodeShort;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeShort;
        }

        public FieldParser GetParser()
        {
            return ParseShort;
        }

        private static object DecodeShort(byte[] content, uint pointer, uint positionBit, uint lengthBytes, uint lengthBits)
        {
            return content.GetSubValueShort(pointer, positionBit, lengthBytes * 8 + lengthBits);
        }

        private static byte[] EncodeShort(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[dataSize];
            if (data is short shortValue)
            {
                for (int i = 0; i < dataSize; i++)
                {
                    buffer[i] = (byte)(shortValue >> ((dataSize - i - 1) * 8));
                }
            }
            return buffer;
        }

        private static object ParseShort(string content)
        {
            short.TryParse(content, out short result);
            return result;
        }
    }
}
