

namespace GGuerra.Cardamatic.CardReader.Interface.Facade
{
    public interface ISamService
    {
        string Name { get; }
        byte[] GetKey(byte[] key, byte[] div);
    }
}
