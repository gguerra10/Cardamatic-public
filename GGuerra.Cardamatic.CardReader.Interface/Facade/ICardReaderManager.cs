using System.Collections.Generic;

namespace GGuerra.Cardamatic.CardReader.Interface.Facade
{
    public interface ICardReaderManager
    {

        /// <summary>
        /// Card reader devices list.
        /// </summary>
        IEnumerable<ICardReaderDevice> CardReaders { get; }
    }
}
