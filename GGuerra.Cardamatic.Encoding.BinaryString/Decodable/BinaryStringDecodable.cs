using System;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;


namespace GGuerra.Cardamatic.Encoding.BinaryString.Decodable
{
    public class BinaryStringDecodable : IDecodable, IEncodable, IParseable
    {
        public Type Type => typeof(BinaryString);

        public bool TextLengthFixed => true;

        public FieldDecoder GetDecoder()
        {
            return DecodeBinaryString;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeBinaryString;
        }

        public FieldParser GetParser()
        {
            return ParseBinaryString;
        }

        private static object DecodeBinaryString(byte[] content, uint pointer, uint positionBit, uint lengthBytes, uint lengthBits)
        {
            var ret = Convert.ToString(content.GetSubValueUnsignedInteger(pointer, positionBit, lengthBytes * 8 + lengthBits), 2);
            ret = ret.PadLeft((int)(lengthBytes * 8 + lengthBits), '0');
            return new BinaryString(ret);
        }

        private static byte[] EncodeBinaryString(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[dataSize];
            if (data is BinaryString binaryString)
            {
                for (int i = 0; i < dataSize; i++)
                {
                    if (binaryString.Value.Length < i * 8)
                    {
                        buffer[i] = Convert.ToByte(binaryString.Value.Substring(i * 8, 8), 2);
                    }
                    else
                    {
                        buffer[i] = Convert.ToByte(binaryString.Value.Substring(i * 8), 2);
                    }
                }
            }
            return buffer;
        }

        private static object ParseBinaryString(string content)
        {
            return new BinaryString(content);
        }
    }
}
