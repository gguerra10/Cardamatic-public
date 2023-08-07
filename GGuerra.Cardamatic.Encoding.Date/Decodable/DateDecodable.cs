using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;
using System;


namespace GGuerra.Cardamatic.Encoding.Date.Decodable
{
    public sealed class DateDecodable : IDecodable, IEncodable, IParseable
    {
        private const int YearReference = 2000;

        public Type Type => typeof(Date);

        public bool TextLengthFixed => true;

        public FieldDecoder GetDecoder()
        {
            return DecodeDate;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeDate;
        }

        public FieldParser GetParser()
        {
            return ParseDate;
        }

        private static object DecodeDate(byte[] content, uint pointer, uint positionBits, uint lengthBytes, uint lengthBits)
        {
            int year = content.GetSubValue(pointer, 0, 7);
            int month = content.GetSubValue(pointer, 7, 4);
            int day = content.GetSubValue(pointer, 11, 5);
            if (year == 0 && month == 0 && day == 0)
            {
                return new Date(DateTime.MinValue);
            }

            return new Date(new DateTime(YearReference + year, month, day));
        }

        private static byte[] EncodeDate(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[2];
            if (data is Date date)
            {
                if (date.Value != DateTime.MinValue)
                {
                    var dato = (ushort)((((date.Value.Year - YearReference) & 0x7F) << 9) |
                            ((date.Value.Month & 0x0F) << 5) |
                            (date.Value.Day & 0x1F));
                    buffer[0] = (byte)(dato >> 8);
                    buffer[1] = (byte)(dato);
                }
            }
            return buffer;
        }

        private static object ParseDate(string content)
        {
            DateTime.TryParse(content, out DateTime result);
            return new Date(result);
        }
    }
}
