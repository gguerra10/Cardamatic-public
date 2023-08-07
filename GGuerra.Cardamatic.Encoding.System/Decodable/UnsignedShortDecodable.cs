using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;
using System;


namespace GGuerra.Cardamatic.Encoding.System.Decodable
{
    public class UnsignedShortDecodable : IDecodable, IEncodable, IParseable
    {
        public Type Type => typeof(ushort);

        public bool TextLengthFixed => false;

        public FieldDecoder GetDecoder()
        {
            return DecodeUnsignedShort;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeUnsignedShort;
        }

        public FieldParser GetParser()
        {
            return ParseUnsignedShort;
        }

        private static object DecodeUnsignedShort(byte[] content, uint pointer, uint positionBit, uint lengthBytes, uint lengthBits)
        {
            return content.GetSubValueUnsignedShort(pointer, positionBit, lengthBytes * 8 + lengthBits);
        }

        private static byte[] EncodeUnsignedShort(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[dataSize];
            if (data is ushort unsignedShort)
            {
                for (int i = 0; i < dataSize; i++)
                {
                    buffer[i] = (byte)(unsignedShort >> ((dataSize - i - 1) * 8));
                }
            }
            return buffer;
        }

        private static object ParseUnsignedShort(string content)
        {
            ushort.TryParse(content, out ushort result);
            return result;
        }
    }
}
