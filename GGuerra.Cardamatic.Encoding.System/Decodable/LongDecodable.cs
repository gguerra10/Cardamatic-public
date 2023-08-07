using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;
using System;


namespace GGuerra.Cardamatic.Encoding.System.Decodable
{
    public class LongDecodable : IDecodable, IEncodable, IParseable
    {
        public Type Type => typeof(long);

        public bool TextLengthFixed => false;

        public FieldDecoder GetDecoder()
        {
            return DecodeLong;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeLong;
        }

        public FieldParser GetParser()
        {
            return ParseLong;
        }

        private static object DecodeLong(byte[] content, uint pointer, uint positionBit, uint lengthBytes, uint lengthBits)
        {
            return content.GetLongSubValue(pointer, positionBit, lengthBytes * 8 + lengthBits);
        }

        private static byte[] EncodeLong(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[dataSize];
            if (data is long mLong)
            {
                for (int i = 0; i < dataSize; i++)
                {
                    buffer[i] = (byte)(mLong >> ((dataSize - i - 1) * 8));
                }
            }
            return buffer;
        }

        private static object ParseLong(string content)
        {
            ulong.TryParse(content, out ulong result);
            return result;
        }
    }
}
