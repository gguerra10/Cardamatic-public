using GGuerra.Cardamatic.WinForm.Schemas.Dto;


namespace GGuerra.Cardamatic.WinForm.Schemas
{
    public interface ISchemaFactory
    {
        /// <summary>
        /// Retrieve schema from file. Throws exception if file is not valid.
        /// </summary>
        /// <param name="schemaPath"></param>
        /// <returns></returns>
        Schema GetSchema(string schemaPath);
    }
}
