using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;
using System;


namespace GGuerra.Cardamatic.Encoding.System.Decodable
{
    public class StringDecodable : IDecodable, IEncodable, IParseable
    {
        public Type Type => typeof(string);

        public bool TextLengthFixed => false;

        public FieldDecoder GetDecoder()
        {
            return DecodeString;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeString;
        }

        public FieldParser GetParser()
        {
            return ParseString;
        }

        private static object DecodeString(byte[] content, uint pointer, uint positionBit, uint lengthBytes, uint lengthBits)
        {
            return content.GetStringSubValue(pointer, lengthBytes);
        }

        private static byte[] EncodeString(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[dataSize];
            if (data is string mString)
            {
                for (int i = 0; i < dataSize && i < mString.Length; i++)
                {
                    buffer[i] = (byte)mString[i];
                }
            }
            return buffer;
        }

        private static object ParseString(string content)
        {
            return content;
        }
    }
}
