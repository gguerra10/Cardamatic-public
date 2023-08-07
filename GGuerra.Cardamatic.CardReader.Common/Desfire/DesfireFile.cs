

namespace GGuerra.Cardamatic.CardReader.Common.Desfire
{
    public class DesfireFile
    {
        public byte FileId { get; set; }

        public string IsoFileId { get; set; } = string.Empty;

        public int FileLength { get; set; }

        public ushort Permissions { get; set; }


    }
}
