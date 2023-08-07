using System;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;


namespace GGuerra.Cardamatic.Encoding.Balance.Decodable
{
    public class BalanceDecodable : IDecodable, IEncodable, IParseable
    {
        public Type Type => typeof(Balance);


        public bool TextLengthFixed => false;

        public FieldDecoder GetDecoder()
        {
            return DecodeBalance;
        }

        public FieldEncoder GetEncoder()
        {
            return EncodeBalance;
        }

        public FieldParser GetParser()
        {
            return ParseBalance;
        }

        private static object DecodeBalance(byte[] content, uint pointer, uint positionBits, uint lengthBytes, uint lengthBits)
        {
            uint balance = content.GetSubValueUnsignedInteger(pointer, 0, 32);
            uint balanceInv = content.GetSubValueUnsignedInteger(pointer + 4, 0, 32);
            uint balanceCopy = content.GetSubValueUnsignedInteger(pointer + 8, 0, 32);
            if (balance != balanceCopy || balance != ~balanceInv)
            {
                return new Balance();
            }
            // Swap bytes
            balance = ((balance & 0xFF) << 24) | ((balance & 0xFF00) << 8) | ((balance & 0xFF0000) >> 8) | ((balance & 0xFF000000) >> 24);

            return new Balance(balance);
        }

        private static byte[] EncodeBalance(object data, int dataSize, uint dataSizeBits)
        {
            var buffer = new byte[16];
            if (data is Balance balance)
            {
                var balanceBytes = BitConverter.GetBytes(balance.Value);
                var balanceBytesInv = BitConverter.GetBytes(~balance.Value);
                var metadataBytes = new byte[] { 0x00, 0xFF, 0x00, 0xFF };

                Array.Copy(balanceBytes, 0, buffer, 0, 4);
                Array.Copy(balanceBytesInv, 0, buffer, 4, 4);
                Array.Copy(balanceBytes, 0, buffer, 8, 4);
                Array.Copy(metadataBytes, 0, buffer, 12, 4);
            }

            return buffer;
        }

        private static object ParseBalance(string content)
        {
            uint.TryParse(content, out uint balance);
            return new Balance(balance);
        }
    }
}
