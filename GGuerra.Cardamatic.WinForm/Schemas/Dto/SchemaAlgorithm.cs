

namespace GGuerra.Cardamatic.WinForm.Schemas.Dto
{
    public class SchemaAlgorithm
    {
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
        public uint LenghtBits { get; set; }
    }
}
