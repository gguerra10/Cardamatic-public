

namespace GGuerra.Cardamatic.Encoding.Interface.Facade
{


    public interface IDecoderManager
    {
        /// <summary>
        /// Get the proper 'IDecodable' from type name.
        /// </summary>
        /// <param name="typeName">Full class name.</param>
        /// <returns></returns>
        IDecodable GetDecodable(string typeName);


        /// <summary>
        /// Get the proper 'IEncodable' from type name.
        /// </summary>
        /// <param name="typeName">Full class name.</param>
        /// <returns></returns>
        IEncodable GetEncodable(string typeName);

        /// <summary>
        /// Get the proper 'IParseable' from type name.
        /// </summary>
        /// <param name="typeName">Full class name.</param>
        /// <returns></returns>
        IParseable GetParseable(string typeName);
    }
}
