using GGuerra.Cardamatic.CardReader.Common.Enum;
using System.Collections.Generic;

namespace GGuerra.Cardamatic.CardReader.Common.Cards.Base
{
    public class ContactlessRaw
    {
        /// <summary>
        /// Contactless card technology
        /// </summary>
        public virtual ContactlessTechnology ContactlessTechnology { get; }

        /// <summary>
        /// Card unique identifier
        /// </summary>
        public virtual string Uid { get; set; }

        /// <summary>
        /// Card data 
        /// </summary>
        public virtual Dictionary<string, string> Addresses { get; set; } = new Dictionary<string, string>();

    }
}
