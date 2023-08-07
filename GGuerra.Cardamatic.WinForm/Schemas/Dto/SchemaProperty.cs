

namespace GGuerra.Cardamatic.WinForm.Schemas.Dto
{

    /// <summary>
    /// Card schema property.
    /// </summary>
    public class SchemaProperty
    {
        /// <summary>
        /// Type class fullname.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Address in the card.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Offset within the address in bytes.
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// Length within the address in bytes.
        /// </summary>
        public uint Length { get; set; }

        /// <summary>
        /// Offset within the address in bits.
        /// </summary>
        public uint OffSetBits { get; set; }

        /// <summary>
        /// Length within the address in bits.
        /// </summary>
        public uint LengthBits { get; set; }

        /// <summary>
        /// Algorithmic parameters.
        /// </summary>
        public SchemaAlgorithm Algorithm { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        public object Value { get; set; }
    }


}
