
using GGuerra.Cardamatic.CardReader.Common.Enum;

namespace GGuerra.Cardamatic.CardReader.Common.Desfire
{
    public class DesfireKey
    {

        /// <summary>
        /// Application (or memory address) to authenticate.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Key number.
        /// </summary>
        public byte KeyNo { get; set; }

        /// <summary>
        /// Key value itself.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Key type (appliation type).
        /// </summary>
        public DesfireApplicationCrypto ApplicationCrypto { get; set; }

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
