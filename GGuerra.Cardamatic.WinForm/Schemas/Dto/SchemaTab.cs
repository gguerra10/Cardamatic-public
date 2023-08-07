using System.Collections.Generic;


namespace GGuerra.Cardamatic.WinForm.Schemas.Dto
{

    /// <summary>
    /// Card schema tab for graphic representation.
    /// </summary>
    public class SchemaTab
    {
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Card schema columns.
        /// </summary>
        public IReadOnlyCollection<SchemaColumn> Columns { get; set; }
    }
}
