

namespace GGuerra.Cardamatic.CardReader.Common.Enum
{
    public enum WrappedInstruction : byte
    {
        GetData = 0xCA,

        AdditionalFrameInstruction = 0xAF,

        GetCardVersion = 0x60,
        FormatCardInstruction = 0xFC,

        CreateApplicationInstruction = 0xCA,
        SelectApplicationInstruction = 0x5A,
        GetApplicationsInstruction = 0x6A,
        GetIsoApplicationsInstruction = 0x6D,
        DeleteApplicationInstruction = 0xDA,

        SetKeySettingsInstruction = 0x54,
        GetKeySettingsInstruction = 0x45,
        ChangeKeyInstruction = 0xC4,
        GetKeyVersionInstruction = 0x64,

        GetFilesInstruction = 0x6F,
        GetIsoFilesInstruction = 0x61,
        GetFileSettingsInstruction = 0xF5,

        CommitTransactionInstruction = 0xC7,
        AbortTransactionInstruction = 0xA7
    }
}
