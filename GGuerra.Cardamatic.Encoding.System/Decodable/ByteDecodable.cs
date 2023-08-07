using System;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;


namespace GGuerra.Cardamatic.Encoding.System.Decodable
{
    public class ByteDecodable : IDecodable, IEncodable, IParseable
    {
        public Type Type => typeof(byte);

        public bool TextLengthFixed => false;

        public FieldDecoder GetDecoder()
        {
            return DecodeByte;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeByte;
        }

        public FieldParser GetParser()
        {
            return ParseByte;
        }

        private static object DecodeByte(byte[] content, uint pointer, uint positionBit, uint lengthBytes, uint lengthBits)
        {
            if (lengthBits == 0 && positionBit == 0)
            {
                return (byte)content.GetChar(pointer);
            }
            else
            {
                return (byte)content.GetSubValue(pointer, positionBit, lengthBytes * 8 + lengthBits);
            }
        }

        private static byte[] EncodeByte(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[1];
            if (data is byte mByte)
            {
                buffer[0] = mByte;
            }
            return buffer;
        }
        private static object ParseByte(string content)
        {
            byte.TryParse(content, out byte result);
            return result;
        }
    }
}
