using System.Collections.Generic;

namespace GGuerra.Cardamatic.WinForm.Schemas.Dto
{
    /// <summary>
    /// Card schema tab column for graphic representation.
    /// </summary>
    public class SchemaColumn
    {
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Card schema properties.
        /// </summary>
        public IReadOnlyCollection<SchemaProperty> Properties { get; set; }
    }
}
