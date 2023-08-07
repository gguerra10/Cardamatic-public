using System;


namespace GGuerra.Cardamatic.Encoding.Interface.Facade
{

    /// <summary>
    /// This delegate is responsible for the transformation of data in corresponding format to binary data.
    /// </summary>
    /// <param name="data">Object data input.</param>
    /// <param name="dataSize">Output data array size in bytes.</param>
    /// <param name="dataSizeBits">Output data array size in bits.</param>
    /// <returns>Binary data array</returns>
    public delegate byte[] FieldEncoder(object data, int dataSize, uint dataSizeBits);

    public interface IEncodable
    {
        /// <summary>
        /// Type declaration of the class.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Get 'FieldEncoder' delegate.
        /// </summary>
        /// <returns></returns>
        FieldEncoder GetEncoder();
    }
}
