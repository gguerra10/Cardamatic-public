

namespace GGuerra.Cardamatic.CardReader.Common.Enum
{
    /// <summary>
    /// Application crypto, 0b00 = Native 3DES, 0b01 = 3K3DES, 0b10 = AES
    /// </summary>
    public enum DesfireApplicationCrypto
    {
        CryptoNative = 0b00, //(DES)
        Crypto3K3Des = 0b01,
        CryptoAes = 0b10
    }
}
