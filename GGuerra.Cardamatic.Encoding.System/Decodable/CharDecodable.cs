using System;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;


namespace GGuerra.Cardamatic.Encoding.System.Decodable
{
    public class CharDecodable : IDecodable, IEncodable, IParseable
    {
        public Type Type => typeof(char);

        public bool TextLengthFixed => true;

        public FieldDecoder GetDecoder()
        {
            return DecodeChar;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeChar;
        }

        public FieldParser GetParser()
        {
            return ParseChar;
        }

        private static object DecodeChar(byte[] content, uint pointer, uint positionBit, uint lengthBytes, uint lengthBits)
        {
            return content.GetChar(pointer);
        }

        private static byte[] EncodeChar(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[1];
            if (data is char mChar)
            {
                buffer[0] = (byte)mChar;
            }
            return buffer;
        }

        private static object ParseChar(string content)
        {
            char.TryParse(content, out char result);
            return result;
        }
    }
}
