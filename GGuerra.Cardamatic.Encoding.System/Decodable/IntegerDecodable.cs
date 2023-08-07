using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;
using System;


namespace GGuerra.Cardamatic.Encoding.System.Decodable
{
    public class IntegerDecodable : IDecodable, IEncodable, IParseable
    {
        public Type Type => typeof(int);

        public bool TextLengthFixed => false;

        public FieldDecoder GetDecoder()
        {
            return DecodeInteger;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeInteger;
        }

        public FieldParser GetParser()
        {
            return ParseInteger;
        }

        private static object DecodeInteger(byte[] content, uint pointer, uint positionBit, uint lengthBytes, uint lengthBits)
        {
            return content.GetSubValue(pointer, positionBit, lengthBytes * 8 + lengthBits);
        }

        private static byte[] EncodeInteger(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[dataSize];
            if (data is int integer)
            {
                for (int i = 0; i < dataSize; i++)
                {
                    buffer[i] = (byte)(integer >> ((dataSize - i - 1) * 8));
                }
            }
            return buffer;
        }

        private static object ParseInteger(string content)
        {
            int.TryParse(content, out int result);
            return result;
        }
    }
}
