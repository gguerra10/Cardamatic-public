using GGuerra.Cardamatic.CardReader.Common.Enum;
using System.Collections.Generic;


namespace GGuerra.Cardamatic.WinForm.Schemas.Dto
{

    /// <summary>
    /// Card schema for graphic representation.
    /// </summary>
    public class Schema
    {

        /// <summary>
        /// Card schema description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Contactless card technology
        /// </summary>
        public ContactlessTechnology ContactlessTechnology { get; set; }


        /// <summary>
        /// Card schema tabs.
        /// </summary>
        public IReadOnlyCollection<SchemaTab> Tabs { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }   
}
