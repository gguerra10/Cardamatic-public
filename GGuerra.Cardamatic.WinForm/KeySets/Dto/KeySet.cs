using System.Collections.Generic;
using GGuerra.Cardamatic.CardReader.Common.Desfire;
using GGuerra.Cardamatic.CardReader.Common.Mifare;


namespace GGuerra.Cardamatic.WinForm.KeySets.Dto
{
    /// <summary>
    /// Key set
    /// </summary>
    public class KeySet
    {
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Mifare card keys
        /// </summary>
        public IReadOnlyCollection<MifareClassicKey> MifareClassicKeys { get; set; }


        /// <summary>
        /// Desfire card keys 
        /// </summary>
        public IReadOnlyCollection<DesfireKey> DesfireKeys { get; set; }

        /// <summary>
        /// Sam Service name.
        /// </summary>
        public string SamService { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
