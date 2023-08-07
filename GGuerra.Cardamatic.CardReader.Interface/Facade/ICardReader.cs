using System.Collections.Generic;
using GGuerra.Cardamatic.CardReader.Common.Apdu;

namespace GGuerra.Cardamatic.CardReader.Interface.Facade
{
    public interface ICardReader
    {
        string ReaderName { get; }
        ApduResponse Transmit(ApduCommand apdu);
        IEnumerable<ApduResponse> Transmit(IEnumerable<ApduCommand> apduList);
        IEnumerable<ApduResponse> Transmit(ApduCommand apdu, bool multiFrame);
    }
}
