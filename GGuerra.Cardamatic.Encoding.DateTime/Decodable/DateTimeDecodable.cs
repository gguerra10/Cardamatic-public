using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;
using System;
using SystemDateTime = System.DateTime;

namespace GGuerra.Cardamatic.Encoding.DateTime.Decodable
{
    public class DateTimeDecodable : IDecodable, IEncodable, IParseable
    {
        private const int YearReference = 2000;

        public Type Type => typeof(SystemDateTime);

        public bool TextLengthFixed => true;

        public FieldDecoder GetDecoder()
        {
            return DecodeDateTime;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeDateTime;
        }

        public FieldParser GetParser()
        {
            return ParseDateTime;
        }

        private static object DecodeDateTime(byte[] content, uint pointer, uint positionBits, uint lengthBytes, uint lengthBits)
        {
            int year = content.GetSubValue(pointer, 0, 6);
            int month = content.GetSubValue(pointer, 6, 4);
            int day = content.GetSubValue(pointer, 10, 5);
            int hour = content.GetSubValue(pointer, 15, 5);
            int minute = content.GetSubValue(pointer, 20, 6);
            int second = content.GetSubValue(pointer, 26, 6);
            if (year == 0 || month == 0 || day == 0)
            {
                return SystemDateTime.MinValue;
            }

            return new SystemDateTime(YearReference + year, month, day, hour, minute, second);
        }

        private static byte[] EncodeDateTime(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[4];
            if (data is SystemDateTime dateTime)
            {
                if (dateTime != SystemDateTime.MinValue)
                {
                    var dato = (uint)((((dateTime.Year - YearReference) & 0x3F) << 26) |
                                ((dateTime.Month & 0x0F) << 22) |
                                ((dateTime.Day & 0x1F) << 17) |
                                ((dateTime.Hour & 0x1F) << 12) |
                                ((dateTime.Minute & 0x3F) << 6) |
                                (dateTime.Second & 0x3F));
                    buffer[0] = (byte)(dato >> 24);
                    buffer[1] = (byte)(dato >> 16);
                    buffer[2] = (byte)(dato >> 8);
                    buffer[3] = (byte)(dato);
                }
            }
            return buffer;
        }

        private static object ParseDateTime(string content)
        {
            SystemDateTime.TryParse(content, out SystemDateTime result);
            return result;
        }
    }
}
