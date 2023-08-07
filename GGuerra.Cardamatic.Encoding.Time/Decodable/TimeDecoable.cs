using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;
using System;


namespace GGuerra.Cardamatic.Encoding.Time.Decodable
{
    public class TimeDecodable : IDecodable, IEncodable, IParseable
    {
        public Type Type => typeof(Time);

        public bool TextLengthFixed => true;

        public FieldDecoder GetDecoder()
        {
            return DecodeTime;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeTime;
        }

        public FieldParser GetParser()
        {
            return ParseTime;
        }

        private static object DecodeTime(byte[] content, uint pointer, uint positionBits, uint lengthBytes, uint lengthBits)
        {
            int hour = content.GetSubValue(pointer, 0, 5);
            int minute = content.GetSubValue(pointer, 5, 6);
            int second = content.GetSubValue(pointer, 11, 5) * 2;
            if (hour == 0 && minute == 0 && second == 0)
            {
                return new Time(new TimeSpan());
            }

            return new Time(new TimeSpan(hour, minute, second));
        }

        private static byte[] EncodeTime(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[2];
            if (data is Time time)
            {
                if (time.Value != new TimeSpan())
                {
                    var dato = (ushort)(((time.Value.Hours & 0x1F) << 11) |
                            ((time.Value.Minutes & 0x3F) << 5) |
                            ((time.Value.Seconds / 2) & 0x1F));
                    buffer[0] = (byte)(dato >> 8);
                    buffer[1] = (byte)(dato);
                }
            }
            return buffer;
        }

        private static object ParseTime(string content)
        {
            TimeSpan.TryParse(content, out TimeSpan result);
            return new Time(result);
        }
    }
}
