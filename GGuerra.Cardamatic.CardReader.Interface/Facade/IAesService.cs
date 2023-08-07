

namespace GGuerra.Cardamatic.CardReader.Interface.Facade
{

    public interface IAesService
    {
        byte[] Encrypt(byte[] data, byte[] key);
        byte[] Decrypt(byte[] data, byte[] key);
        byte[] InitialVector { get; set; }
    }
}
