using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;
using System;
using SystemTextEncoding = System.Text.Encoding;

namespace GGuerra.Cardamatic.Encoding.System.Decodable
{
    public class ByteArrayDecodable : IDecodable, IEncodable, IParseable
    {
        public Type Type => typeof(byte[]);

        public bool TextLengthFixed => true;

        public FieldDecoder GetDecoder()
        {
            return DecodeByteArray;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeByteArray;
        }

        public FieldParser GetParser()
        {
            return ParseByteArray;
        }

        private static object DecodeByteArray(byte[] content, uint pointer, uint positionBit, uint lengthBytes, uint lengthBits)
        {
            var result = new byte[lengthBytes];
            for (uint i = 0; i < lengthBytes; i++)
            {
                result[i] = (byte)content.GetChar(pointer + i);
            }
            return result;
        }

        private static byte[] EncodeByteArray(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[dataSize];
            if (data is byte[] byteArray)
            {
                for (int i = 0; i < dataSize && i < byteArray.Length; i++)
                {
                    buffer[i] = byteArray[i];
                }
            }
            return buffer;
        }

        private static object ParseByteArray(string content)
        {
            return SystemTextEncoding.ASCII.GetBytes(content);
        }
    }
}
