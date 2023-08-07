using GGuerra.Cardamatic.CardReader.Common.Enum;


namespace GGuerra.Cardamatic.CardReader.Common.Mifare
{
    public class MifareClassicKey
    {
        /// <summary>
        /// Key version number.
        /// </summary>
        public uint KeyNo { get; set; }

        /// <summary>
        /// Key type.
        /// </summary>
        public MifareClassicKeyType KeyType { get; set; }

        /// <summary>
        /// Sector (memory address) to authenticate.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Key value itself
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Key is valid for reading.
        /// </summary>
        public bool ReadKey { get; set; } = true;

        /// <summary>
        /// Key is valid for writing.
        /// </summary>
        public bool WriteKey { get; set; } = true;
    }
}
