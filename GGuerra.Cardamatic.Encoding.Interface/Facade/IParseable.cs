using System;

namespace GGuerra.Cardamatic.Encoding.Interface.Facade
{


    /// <summary>
    /// This delegate is responsible for the transformation of string text to the corresponding format.
    /// </summary>
    /// <param name="content">String data input.</param>
    /// <returns>Data object</returns>
    public delegate object FieldParser(string content);

    public interface IParseable
    {
        /// <summary>
        /// Type declaration of the class.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Get 'FieldParser' delegate.
        /// </summary>
        /// <returns></returns>
        FieldParser GetParser();
    }
}
