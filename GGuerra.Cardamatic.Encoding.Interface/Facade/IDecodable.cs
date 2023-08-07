using System;


namespace GGuerra.Cardamatic.Encoding.Interface.Facade
{

    /// <summary>
    /// This delegate is responsible for the transformation of binary data to the corresponding format.
    /// </summary>
    /// <param name="content">Binary data input.</param>
    /// <param name="pointer">Offset within the binary data in bytes.</param>
    /// <param name="positionBit">Offset within the binary data in bits.</param>
    /// <param name="lengthBytes">Length within the binary data in bytes.</param>
    /// <param name="lengthBits">Length within the binary data in bits.</param>
    /// <returns>Data object</returns>
    public delegate object FieldDecoder(byte[] content, uint pointer, uint positionBit, uint lengthBytes, uint lengthBits);

    public interface IDecodable
    {
        /// <summary>
        /// Type declaration of the class.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Get 'FieldDecoder' delegate.
        /// </summary>
        /// <returns></returns>
        FieldDecoder GetDecoder();

        /// <summary>
        /// Text length fixed
        /// </summary>
        bool TextLengthFixed { get; }

    }
}
