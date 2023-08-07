using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;
using System;


namespace GGuerra.Cardamatic.Encoding.System.Decodable
{
    public class UnsignedIntegerDecodable : IDecodable, IEncodable, IParseable
    {
        public Type Type => typeof(uint);

        public bool TextLengthFixed => false;

        public FieldDecoder GetDecoder()
        {
            return DecodeUnsignedInteger;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeUnsignedInteger;
        }

        public FieldParser GetParser()
        {
            return ParseUnsignedInteger;
        }

        private static object DecodeUnsignedInteger(byte[] content, uint pointer, uint positionBit, uint lengthBytes, uint lengthBits)
        {
            return content.GetSubValueUnsignedInteger(pointer, positionBit, lengthBytes * 8 + lengthBits);
        }


        private static byte[] EncodeUnsignedInteger(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[dataSize];
            if (data is uint unsignedShort)
            {
                for (int i = 0; i < dataSize; i++)
                {
                    buffer[i] = (byte)(unsignedShort >> ((dataSize - i - 1) * 8));
                }
            }
            return buffer;
        }

        private static object ParseUnsignedInteger(string content)
        {
            uint.TryParse(content, out uint result);
            return result;
        }
    }
}
