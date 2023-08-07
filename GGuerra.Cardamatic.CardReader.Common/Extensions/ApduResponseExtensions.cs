

using GGuerra.Cardamatic.CardReader.Common.Apdu;

namespace GGuerra.Cardamatic.CardReader.Common.Extensions
{
    public static class ApduResponseExtensions
    {
        public static bool IsSuccess(this ApduResponse apduResponse)
        {
            return (apduResponse != null) && ((apduResponse.SW1 == 0x90 || apduResponse.SW1 == 0x91) && (apduResponse.SW2 == 0x00));
        }

        public static bool MoreDataExpected(this ApduResponse apduResponse)
        {
            return (apduResponse != null) && ((apduResponse.SW1 == 0x90 || apduResponse.SW1 == 0x91) && (apduResponse.SW2 == 0xAF));
        }
    }
}
