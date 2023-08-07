using System.Collections.Generic;
using GGuerra.Cardamatic.CardReader.Interface.Event;


namespace GGuerra.Cardamatic.CardReader.Interface.Facade
{
    public interface ICardReaderDevice
    {

        /// <summary>
        /// Card reader device name.
        /// </summary>
        string DeviceName { get; }
        IEnumerable<ICardReader> CardReaders { get; }
        ICardReader GetContactlessCardReader();
        string GetCardUid();
        void StartDetection();
        void StopDetection();

        event CardDetected CardDetected;

        event CardRemoved CardRemoved;
    }
}
