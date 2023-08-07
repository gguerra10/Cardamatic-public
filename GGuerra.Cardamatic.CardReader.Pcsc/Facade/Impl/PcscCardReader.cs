using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PCSC;
using GGuerra.Cardamatic.CardReader.Common.Apdu;
using GGuerra.Cardamatic.CardReader.Common.Enum;
using GGuerra.Cardamatic.CardReader.Common.Extensions;



namespace GGuerra.Cardamatic.CardReader.Pcsc.Facade.Impl
{
    public class PcscCardReader : Interface.Facade.ICardReader
    {
        private readonly string _readerName;
        private readonly ILogger<PcscCardReader> _logger;
        private readonly IContextFactory _contextFactory;

        public PcscCardReader(
            ILogger<PcscCardReader> logger,
            string readerName,
            IContextFactory contextFactory
            )
        {
            _logger = logger;
            _readerName = readerName;
            _contextFactory = contextFactory;
        }

        public string ReaderName => _readerName;
        public ApduResponse Transmit(ApduCommand apdu)
        {
            ApduResponse apduResponse = null;
            try
            {
                using (var context = _contextFactory.Establish(SCardScope.System))
                {
                    using (var rfidReader = context.ConnectReader(_readerName, SCardShareMode.Shared, SCardProtocol.Any))
                    {
                        using (rfidReader.Transaction(SCardReaderDisposition.Leave))
                        {
                            _logger.LogDebug($"Sending APDU: {apdu}");
                            apduResponse = SendApdu(rfidReader, apdu);
                            _logger.LogDebug($"Received APDU: {apduResponse}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Cannot transmit APDU. {ex.Message}");
            }
            return apduResponse;
        }

        public IEnumerable<ApduResponse> Transmit(IEnumerable<ApduCommand> apduList)
        {
            var result = new List<ApduResponse>();
            try
            {
                using (var context = _contextFactory.Establish(SCardScope.System))
                {
                    using (var rfidReader = context.ConnectReader(_readerName, SCardShareMode.Shared, SCardProtocol.Any))
                    {
                        using (rfidReader.Transaction(SCardReaderDisposition.Leave))
                        {
                            foreach (var apdu in apduList)
                            {
                                _logger.LogDebug($"Sending APDU: {apdu}");
                                var apduResponse = SendApdu(rfidReader, apdu);
                                _logger.LogDebug($"Received APDU: {apduResponse}");
                                result.Add(apduResponse);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Cannot transmit APDU. {ex.Message}");
            }
            return result;
        }

        public IEnumerable<ApduResponse> Transmit(ApduCommand apdu, bool multiFrame)
        {
            var apduResponses = new List<ApduResponse>();
            var apduResponse = Transmit(apdu);
            if (apduResponse != null)
            {
                apduResponses.Add(apduResponse);
            }
            if (multiFrame)
            {
                while (apduResponse.MoreDataExpected())
                {
                    var apduCommand = new ApduCommand((byte)InstructionClass.Wrapped, (byte)WrappedInstruction.AdditionalFrameInstruction, 0x00, 0x00, null, 0x00); // More data command
                    apduResponse = Transmit(apduCommand);
                    apduResponses.Add(apduResponse);
                }
            }
            return apduResponses;
        }

        private ApduResponse SendApdu(PCSC.ICardReader rfidReader, ApduCommand apdu)
        {
            var sendPci = SCardPCI.GetPci(rfidReader.Protocol);
            var receivePci = new SCardPCI(); // IO returned protocol control information.

            var receiveBuffer = new byte[256];
            var command = apdu.ToArray();

            var bytesReceived = rfidReader.Transmit(
               sendPci, // Protocol Control Information (T0, T1 or Raw)
               command, // command APDU
               command.Length,
               receivePci, // returning Protocol Control Information
               receiveBuffer,
               receiveBuffer.Length); // data buffer

            Array.Resize(ref receiveBuffer, bytesReceived);
            var apduResponse = new ApduResponse(receiveBuffer);
            return apduResponse;
        }

    }
}
