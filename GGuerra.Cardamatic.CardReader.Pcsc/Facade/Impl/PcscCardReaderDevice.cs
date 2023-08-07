using GGuerra.Cardamatic.CardReader.Common.Apdu;
using GGuerra.Cardamatic.CardReader.Common.Enum;
using GGuerra.Cardamatic.CardReader.Common.Extensions;
using GGuerra.Cardamatic.CardReader.Interface.Event;
using GGuerra.Cardamatic.CardReader.Pcsc.Configuration;
using GGuerra.Cardamatic.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PCSC;
using PCSC.Iso7816;
using PCSC.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GGuerra.Cardamatic.CardReader.Pcsc.Facade.Impl
{

    public class PcscCardReaderDevice : Interface.Facade.ICardReaderDevice, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private ILogger<PcscCardReaderDevice> _logger;
        private string _deviceName;
        private PcscCardReaderConfiguration _configuration;
        private List<Interface.Facade.ICardReader> _cardReaders;

        private IContextFactory _contextFactory;
        private IMonitorFactory _monitorFactory;
        private readonly ISCardMonitor _monitor;


        public PcscCardReaderDevice(
            IServiceProvider serviceProvider,
            ILogger<PcscCardReaderDevice> logger,
            string deviceName, 
            PcscCardReaderConfiguration configuration,
            IContextFactory contextFactory,
            IMonitorFactory monitorFactory)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _deviceName = deviceName;
            _configuration = configuration;
            _contextFactory = contextFactory;
            _monitorFactory = monitorFactory;

            _cardReaders = GetCardReaders();

            _monitor = _monitorFactory.Create(SCardScope.System);

            _monitor.CardInserted += Monitor_CardInserted;
            _monitor.CardRemoved += Monitor_CardRemoved;
        }

        public string DeviceName => _deviceName;

        public IEnumerable<Interface.Facade.ICardReader> CardReaders => _cardReaders;

        public Interface.Facade.ICardReader GetContactlessCardReader()
        {
            return _cardReaders.FirstOrDefault(r => r.ReaderName == _deviceName);
        }

        protected virtual void OnCardDetected(string uid)
        {
            CardDetected?.Invoke(this, uid);
        }
        public event CardDetected CardDetected;

        protected virtual void OnCardRemoved()
        {
            CardRemoved?.Invoke(this);
        }
        public event CardRemoved CardRemoved;

        public void StartDetection()
        {
            var uid = GetCardUid();
            if (!string.IsNullOrEmpty(uid))
            {
                OnCardDetected(uid);
            }

            _monitor.Start(_deviceName);
        }

        public void StopDetection()
        {
            _monitor.Cancel();
        }

        public string GetCardUid()
        {
            var cardUid = string.Empty;
            var apduCommand = new ApduCommand((byte)InstructionClass.Custom, (byte)InstructionCode.GetData, 0x00, 0x00, null, 0x00); // Get Card UID command
            var apduResponse = GetContactlessCardReader()?.Transmit(apduCommand);
            if (apduResponse.IsSuccess())
            {
                cardUid = apduResponse.Data.ToHexString();
            }
            return cardUid;
        }

        public void Dispose()
        {
            _monitor.CardInserted -= Monitor_CardInserted;
            _monitor.CardRemoved -= Monitor_CardRemoved;
            _monitorFactory.Release(_monitor);
        }


        private void Monitor_CardRemoved(object sender, CardStatusEventArgs e)
        {
            if (e.ReaderName.Equals(_deviceName))
            {
                OnCardRemoved();
            }
        }

        private void Monitor_CardInserted(object sender, CardStatusEventArgs e)
        {
            if (e.ReaderName.Equals(_deviceName))
            {
                var uid = GetCardUid();
                OnCardDetected(uid);
            }
        }


        private List<Interface.Facade.ICardReader> GetCardReaders()
        {
            var result = new List<Interface.Facade.ICardReader>();
            foreach (var readerName in GetCardReaderNames())
            {
                var cardReader = new PcscCardReader(
                    _serviceProvider.GetRequiredService<ILogger<PcscCardReader>>(),
                    readerName,
                    _contextFactory
                    );
                result.Add(cardReader);
            }

            return result;
        }

        private IEnumerable<string> GetCardReaderNames()
        {
            var result = new List<string>();
            try
            {
                using (var context = _contextFactory.Establish(SCardScope.System))
                {
                    foreach (var reader in context.GetReaders())
                    {
                        if (_configuration.Devices[_deviceName].Contains(reader))
                        {
                            result.Add(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot list readers. {ex.Message}");
            }
            return result;
        }
    }
}
