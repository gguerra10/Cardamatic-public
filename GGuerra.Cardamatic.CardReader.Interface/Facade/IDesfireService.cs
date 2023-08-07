using GGuerra.Cardamatic.CardReader.Common.Desfire;
using GGuerra.Cardamatic.CardReader.Common.Enum;
using System.Collections.Generic;


namespace GGuerra.Cardamatic.CardReader.Interface.Facade
{
    public interface IDesfireService
    {
        /// <summary>
        /// Set card reader that will be used to sending further commands.
        /// </summary>
        /// <param name="cardReader"></param>
        void SetCardReader(ICardReader cardReader);

        /// <summary>
        /// Set sam service that will be used in key diversification.
        /// </summary>
        /// <param name="cardReader"></param>
        void SetSamService(ISamService samService);

        DesfireCardVersion GetCardVersion();

        bool FormatCard();

        IEnumerable<string> GetApplicationIds();

        IEnumerable<DesfireApplication> GetApplications();

        /// <summary>
        /// Select application in card.
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        bool SelectApplication(string applicationId);

        bool CreateApplication(DesfireApplication application, DesfireApplicationSetting applicationSetting);

        bool CreateApplication(string applicationId, DesfireApplicationSetting applicationSetting);

        bool DeleteApplication(string applicationId);

        DesfireApplicationSetting GetKeySetting();

        bool SetKeySetting(DesfireKeySetting keySetting);

        /// <summary>
        /// Change key to new fixed key.
        /// </summary>
        /// <param name="keyNo"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ChangeKey(byte keyNo, string key);

        /// <summary>
        /// Change key to new diversified key.
        /// </summary>
        /// <param name="keyNo"></param>
        /// <param name="key"></param>
        /// <param name="cardUid"></param>
        /// <returns></returns>
        bool ChangeKey(byte keyNo, string key, string cardUid);

        IDictionary<byte, byte> GetKeyVersions();

        IEnumerable<byte> GetFileIds();

        IEnumerable<DesfireFile> GetFiles();

        DesfireFile GetFileSettings(byte fileId);

        void ResetAuthentication();

        /// <summary>
        /// Authenticate with fixed keys
        /// </summary>
        /// <param name="keyNo"></param>
        /// <param name="key"></param>
        /// <param name="desfireAuthenticationType"></param>
        /// <returns></returns>
        bool Authenticate(byte keyNo, string key, DesfireAuthenticationType desfireAuthenticationType);

        /// <summary>
        /// Authenticate with cardUid diversified keys.
        /// </summary>
        /// <param name="keyNo"></param>
        /// <param name="key"></param>
        /// <param name="desfireAuthenticationType"></param>
        /// <param name="cardUid"></param>
        /// <returns></returns>
        bool Authenticate(byte keyNo, string key, DesfireAuthenticationType desfireAuthenticationType, string cardUid);


        /// <summary>
        /// Read file content from card.
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="fileType"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        string ReadFile(byte fileId, DesfireFileType fileType, int offset, int length, DesfireCommunicationMode mode);

        /// <summary>
        /// Write content file in card.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileId"></param>
        /// <param name="fileType"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool WriteFile(string data, byte fileId, DesfireFileType fileType, int offset, int length, DesfireCommunicationMode mode);

        bool CommitTransaction();

    }
}
