

namespace GGuerra.Cardamatic.CardReader.Interface.Facade
{
    public interface IDesService
    {
        byte[] Encrypt(byte[] data, byte[] key);
        byte[] Decrypt(byte[] data, byte[] key);
        public byte[] InitialVector { get; set; }
    }
}
