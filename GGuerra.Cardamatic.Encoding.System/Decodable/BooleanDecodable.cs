using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;
using System;


namespace GGuerra.Cardamatic.Encoding.System.Decodable
{
    public class BooleanDecodable : IDecodable, IEncodable, IParseable
    {
        public Type Type => typeof(bool);

        public bool TextLengthFixed => false;

        public FieldDecoder GetDecoder()
        {
            return DecodeBoolean;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeBoolean;
        }

        public FieldParser GetParser()
        {
            return ParseBoolean;
        }

        private static object DecodeBoolean(byte[] content, uint pointer, uint positionBit, uint lengthBytes, uint lengthBits)
        {
            return (int)DecodeInteger(content, pointer, positionBit, lengthBytes, lengthBits) == 1;
        }

        private static object DecodeInteger(byte[] content, uint pointer, uint positionBit, uint lengthBytes, uint lengthBits)
        {
            return content.GetSubValue(pointer, positionBit, lengthBytes * 8 + lengthBits);
        }

        private static byte[] EncodeBoolean(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[1];
            if (data is bool b)
            {
                buffer[0] = (byte)(b ? 0x01 : 0x00);
            }
            return buffer;
        }

        private static object ParseBoolean(string content)
        {
            bool.TryParse(content, out bool result);
            return result;
        }
    }
}
