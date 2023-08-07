using System;


namespace GGuerra.Cardamatic.CardReader.Common.Desfire
{
    public class DesfireKeySetting
    {
        /// <summary>
        /// ChangeKey access right:
        /// 0x00-0x0D - active authentication with specified key
        /// 0x0E = same key with which current authentication is active
        /// 0x0F = no access, key sets are frozen
        /// </summary>
        public byte ChangeKey { get; set; }

        /// <summary>
        /// AppKeySettings changeable: 
        /// False = Keysett1 cannot be changed
        /// True = Keysett1 can be changed after authentication with AppMasterKey
        /// </summary>
        public bool AppKeySettingsPermission { get; set; }

        /// <summary>
        /// File create / delete configuration:
        /// False = App.Master key is required to create/delete file
        /// True = App.Master key is not required to create/delete file
        /// </summary>
        public bool FilePermission { get; set; }

        /// <summary>
        /// File directory access configuration:
        /// False = App.Master key is required to get file Ids, file sett, key sett.; 
        /// True = App.Master key is not required to get file Ids, file sett, key sett.
        /// </summary>
        public bool FileDirectoryPermission { get; set; }

        /// <summary>
        /// AppMaster key changeable:
        /// False = App.Master key cannot be changed
        /// True = App.Master key can be changed after authentication with AppMasterKey
        /// </summary>
        public bool MasterKeyPermission { get; set; }

        public DesfireKeySetting() { }

#pragma warning disable S109 // Assign this magic number to a well-named (variable|constant) and use (variable|constant). Desfire msg format
        public byte Raw
        {
            get
            {
                return (byte)(
                    (ChangeKey << 4) |
                    (Convert.ToByte(AppKeySettingsPermission) << 3) |
                    (Convert.ToByte(FilePermission) << 2) |
                    (Convert.ToByte(FileDirectoryPermission) << 1) |
                    (Convert.ToByte(MasterKeyPermission))
                    );
            }
            set
            {
                ChangeKey = (byte)((value & 0xF0) >> 4);
                AppKeySettingsPermission = Convert.ToBoolean((value & 0x08) >> 3);
                FilePermission = Convert.ToBoolean((value & 0x04) >> 2);
                FileDirectoryPermission = Convert.ToBoolean((value & 0x02) >> 1);
                MasterKeyPermission = Convert.ToBoolean(value & 0x01);
            }
        }
#pragma warning restore S109 // Assign this magic number to a well-named (variable|constant) and use (variable|constant).
    }
}
