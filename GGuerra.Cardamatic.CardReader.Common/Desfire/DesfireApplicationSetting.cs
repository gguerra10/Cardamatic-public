using GGuerra.Cardamatic.CardReader.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace GGuerra.Cardamatic.CardReader.Common.Desfire
{
    public class DesfireApplicationSetting
    {

        public DesfireKeySetting KeySetting { get; set; }

        /// <summary>
        /// Number of keys in the application (per key set – max 0x0D (14))
        /// </summary>
        public byte KeyCount { get; set; }

        public DesfireApplicationCrypto ApplicationCrypto { get; set; }

        public bool Iso { get; set; }

        public bool KeySett3Present { get; set; }

        public DesfireApplicationSetting()
        {
            KeySetting = new DesfireKeySetting();
        }

#pragma warning disable S109 // Assign this magic number to a well-named (variable|constant) and use (variable|constant). Desfire msg format
        public byte[] Raw
        {
            get
            {
                var result = new byte[2];
                result[0] = KeySetting.Raw;
                result[1] = (byte)(
                                   (Convert.ToByte(ApplicationCrypto) << 6) |
                                   (Convert.ToByte(Iso) << 5) |
                                   (Convert.ToByte(KeySett3Present) << 4) |
                                   (KeyCount & 0x0F)
                                  );

                return result;
            }
            set
            {
                if (value.Length > 1)
                {
                    KeySetting.Raw = value[0];

                    ApplicationCrypto = (DesfireApplicationCrypto)((value[1] & 0xC0) >> 6);
                    KeyCount = (byte)(value[1] & 0x0F);
                }
            }
        }
#pragma warning restore S109 // Assign this magic number to a well-named (variable|constant) and use (variable|constant).
    }
}
