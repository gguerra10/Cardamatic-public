using System.Collections.Generic;
using GGuerra.Cardamatic.CardReader.Common.Enum;


namespace GGuerra.Cardamatic.CardReader.Interface.Facade
{
    public interface IMifareClassicService
    {

        /// <summary>
        /// Set card reader that will be used to sending further commands.
        /// </summary>
        /// <param name="cardReader"></param>
        void SetCardReader(ICardReader cardReader);

        /// <summary>
        /// Load key in memory.
        /// </summary>
        /// <param name="keyNo"></param>
        /// <param name="sector"></param>
        /// <param name="keyType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        bool LoadKey(uint keyNo, uint sector, MifareClassicKeyType keyType, string key);

        /// <summary>
        /// Authenticate with previous memory loaded key.
        /// </summary>
        /// <param name="keyNo"></param>
        /// <param name="sector"></param>
        /// <param name="keyType"></param>
        /// <returns></returns>
        bool Authenticate(uint keyNo, uint sector, MifareClassicKeyType keyType);

        /// <summary>
        /// Read blocks from MiFare card.
        /// </summary>
        /// <param name="blocks"></param>
        /// <returns></returns>
        IDictionary<uint, string> ReadBlocks(IEnumerable<uint> blocks);

        /// <summary>
        /// Write blocks in MiFare card.
        /// </summary>
        /// <param name="dataBlocks"></param>
        /// <returns></returns>
        IEnumerable<bool> WriteBlocks(IDictionary<uint, string> dataBlocks);
    }
}
