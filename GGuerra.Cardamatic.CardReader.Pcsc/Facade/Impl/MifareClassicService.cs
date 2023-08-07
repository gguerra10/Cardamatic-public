using GGuerra.Cardamatic.CardReader.Common.Apdu;
using GGuerra.Cardamatic.CardReader.Common.Enum;
using GGuerra.Cardamatic.CardReader.Common.Extensions;
using GGuerra.Cardamatic.CardReader.Common.Mifare;
using GGuerra.Cardamatic.CardReader.Interface.Facade;
using GGuerra.Cardamatic.Extensions;
using Microsoft.Extensions.Logging;
using PCSC.Iso7816;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GGuerra.Cardamatic.CardReader.Pcsc.Facade.Impl
{
    public enum KeyStructure : byte
    {
        VolatileMemory1 = 0x00,
        VolatileMemory2 = 0x01
    }

    public class MifareClassicService : IMifareClassicService
    {

        private List<MifareClassicKey> _keys;
        private ILogger<MifareClassicService> _logger;
        private ICardReader _cardReader;

        public MifareClassicService(
            ILogger<MifareClassicService> logger
            )
        {
            _logger = logger;
            _keys = new List<MifareClassicKey>();
        }

        public void SetCardReader(ICardReader cardReader)
        {
            _cardReader = cardReader;
            _keys.Clear();
        }

        public bool LoadKey(uint keyNo, uint sector, MifareClassicKeyType keyType, string key)
        {
            var result = true;

            _keys.Add(new MifareClassicKey()
            {
                KeyNo = keyNo,
                Address = sector.ToString(),
                KeyType = keyType,
                Key = key
            });


            return result;
        }


        public bool Authenticate(uint keyNo, uint sector, MifareClassicKeyType keyType)
        {
            var result = false;
            var key = _keys.FirstOrDefault(k => k.KeyNo == keyNo && k.Address == sector.ToString() && k.KeyType == keyType);
            if (key != null)
            {
                var apduCommands = new List<ApduCommand>
                {
                    new ApduCommand((byte)InstructionClass.Custom, (byte)InstructionCode.ExternalAuthenticate, 0x00, (byte)KeyStructure.VolatileMemory1, key.Key.ToByteArray(), 0xFF) // Send key to reader command
                };
                var authenticationData = new byte[]
                {
                    0x01, 0x00, (byte)GetBlockFromSector(sector), (byte)(keyType == MifareClassicKeyType.TypeA ? 0x60: 0x61), (byte)KeyStructure.VolatileMemory1
                };
                apduCommands.Add(new ApduCommand((byte)InstructionClass.Custom, (byte)InstructionCode.InternalAuthenticate, 0x00, 0x00, authenticationData, 0xFF)); // Authenticate mifare classic command
                var apduResponses = _cardReader.Transmit(apduCommands);
                if (!apduResponses.Any(x => !x.IsSuccess()))
                {
                    result = true;
                }
            }
            return result;
        }

        public IDictionary<uint, string> ReadBlocks(IEnumerable<uint> blocks)
        {
            var result = new Dictionary<uint, string>();

            var apduCommands = new List<ApduCommand>();
            for (int order = 0; order < blocks.Count(); order++)
            {
                apduCommands.Add(new ApduCommand((byte)InstructionClass.Custom, (byte)InstructionCode.ReadBinary, 0x00, (byte)blocks.ElementAt(order), null, 0x10));
            }
            var apduResponses = _cardReader.Transmit(apduCommands);
            if (apduResponses.Count() == apduCommands.Count())
            {
                for (int order = 0; order < blocks.Count(); order++)
                {
                    var apduResponse = apduResponses.ElementAt(order);
                    if (apduResponse.IsSuccess())
                    {
                        result.Add(blocks.ElementAt(order), apduResponse.Data.ToHexString());
                    }
                }
            }

            return result;
        }

        public IEnumerable<bool> WriteBlocks(IDictionary<uint, string> dataBlocks)
        {
            var result = new List<bool>();

            var apduCommands = new List<ApduCommand>();
            for (int order = 0; order < dataBlocks.Count(); order++)
            {
                apduCommands.Add(new ApduCommand((byte)InstructionClass.Custom, (byte)InstructionCode.UpdateBinary, 0x00, (byte)dataBlocks.Keys.ElementAt(order), dataBlocks.Values.ElementAt(order).ToByteArray(), 0xFF));
            }
            var apduResponses = _cardReader.Transmit(apduCommands);
            if (apduResponses.Count() == apduCommands.Count())
            {
                for (int order = 0; order < dataBlocks.Count(); order++)
                {
                    var apduResponse = apduResponses.ElementAt(order);
                    result.Add(apduResponse.IsSuccess());
                }
            }
            return result;
        }

        private static uint GetBlockFromSector(uint sector)
        {
            return ((sector) < (32) ? ((sector) * 4) : (128 + (((sector) - 32) * 16)));
        }

        private static uint GetSectorFromBlock(uint block)
        {
            return ((block) < (128) ? ((block) / 4) : (32 + (((block) - 128) / 16)));
        }
    }
}
