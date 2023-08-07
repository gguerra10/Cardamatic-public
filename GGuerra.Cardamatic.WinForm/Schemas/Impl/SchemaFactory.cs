using System;
using System.IO;
using Newtonsoft.Json;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.WinForm.Schemas.Dto;



namespace GGuerra.Cardamatic.WinForm.Schemas.Impl
{

    /// <summary>
    /// Schema factory implementation.
    /// </summary>
    public class SchemaFactory : ISchemaFactory
    {
        private readonly IDecoderManager _decoderManager;

        public SchemaFactory(IDecoderManager decoderManager)
        {
            _decoderManager = decoderManager;
        }

        public Schema GetSchema(string filePath)
        {
            // Read file and deserialize into card schema.
            var schemaContent = File.ReadAllText(filePath);
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            var schema = JsonConvert.DeserializeObject<Schema>(schemaContent);
            // Check schema has tabs
            if (schema != null && schema.Tabs != null)
            {
                foreach(var tab in schema.Tabs)
                {
                    // Check tab has columns
                    if (tab.Columns != null)
                    {
                        foreach(var column in tab.Columns)
                        {
                            // Check column has properties
                            if (column.Properties != null)
                            {
                                foreach(var property in column.Properties)
                                {
                                    try
                                    {
                                        // Check if property has decodeable implementation.
                                        var decoder = _decoderManager.GetDecodable(property.TypeName);
                                        if(decoder == null)
                                        {
                                            throw new Exception($"Property {property.Name} in file {filePath} has no 'IDecodable' implementation.");
                                        }
                                    }
                                    catch (Exception innerException)
                                    {
                                        throw new Exception($"Property {property.Name} in file {filePath} has no 'IDecodable' implementation.", innerException);
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception($"Column {column.Description} in file {filePath} cannot be deserialized as valid column schema.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"Tab {tab.Description} in file {filePath} cannot be deserialized as valid tab schema.");
                    }
                }
            }
            else
            {
                throw new Exception($"File {filePath} cannot be deserialized as valid schema.");
            }

            return schema;
        }
    }
}
