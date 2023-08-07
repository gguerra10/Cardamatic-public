using GGuerra.Cardamatic.CardReader.Interface.Facade;
using GGuerra.Cardamatic.CardReader.Pcsc.Configuration;
using GGuerra.Cardamatic.CardReader.Pcsc.Facade.Impl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PCSC;
using PCSC.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GGuerra.Cardamatic.CardReader.Pcsc.HostedService
{
    public class CardReaderHostedService : IHostedService, ICardReaderManager
    {

        private List<ICardReaderDevice> _cardReaderDevices;
        private bool _readerAttached;
        private readonly ILogger<CardReaderHostedService> _logger;
        private readonly PcscCardReaderConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly IContextFactory _contextFactory;
        private readonly IMonitorFactory _monitorFactory;
        private readonly IDeviceMonitor _deviceMonitor;

        public CardReaderHostedService(
            ILogger<CardReaderHostedService> logger,
            IOptions<PcscCardReaderConfiguration> options,
            IServiceProvider serviceProvider,
            IContextFactory contextFactory,
            IMonitorFactory monitorFactory,
            IDeviceMonitorFactory deviceMonitorFactory
            )
        {
            _logger = logger;
            _configuration = options.Value;
            _serviceProvider = serviceProvider;
            _contextFactory = contextFactory;
            _monitorFactory = monitorFactory;

            _deviceMonitor = deviceMonitorFactory.Create(SCardScope.System);

            _cardReaderDevices = new List<ICardReaderDevice>();
        }

        public IEnumerable<ICardReaderDevice> CardReaders => _cardReaderDevices;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _deviceMonitor.Initialized += DeviceMonitor_Initialized;
            _deviceMonitor.MonitorException += DeviceMonitor_MonitorException;
            _deviceMonitor.StatusChanged += DeviceMonitor_StatusChanged;

            _readerAttached = InitializeCardReaderDevices();
            _deviceMonitor.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _deviceMonitor.Initialized -= DeviceMonitor_Initialized;
            _deviceMonitor.MonitorException -= DeviceMonitor_MonitorException;
            _deviceMonitor.StatusChanged -= DeviceMonitor_StatusChanged;

            _deviceMonitor.Cancel();
            return Task.CompletedTask;
        }

        private IEnumerable<string> GetCardReaderDeviceNames()
        {
            var result = new List<string>();
            try
            {
                using (var context = _contextFactory.Establish(SCardScope.System))
                {
                    result = context.GetReaders().ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot list readers. {ex.Message}");
            }
            return result;
        }

        private void DeviceMonitor_MonitorException(object sender, DeviceMonitorExceptionEventArgs args)
        {
            _logger.LogError($"Pcsc DeviceMonitor Exception: {args.Exception}");
        }

        private void DeviceMonitor_Initialized(object sender, DeviceChangeEventArgs e)
        {
            _logger.LogDebug("Pcsc DeviceMonitor initialized.");
            foreach (var attachedReaders in e.AttachedReaders)
            {
                _logger.LogInformation($"Attached pcsc reader: {attachedReaders}");
            }
            foreach (var detachedReaders in e.DetachedReaders)
            {
                _logger.LogInformation($"Detached pcsc reader: {detachedReaders}");
            }
        }

        private void DeviceMonitor_StatusChanged(object sender, DeviceChangeEventArgs e)
        {
            foreach (var attachedReaders in e.AttachedReaders)
            {
                _logger.LogInformation($"Attached pcsc reader: {attachedReaders}");
            }
            foreach (var detachedReaders in e.DetachedReaders)
            {
                _logger.LogInformation($"Detached pcsc reader: {detachedReaders}");
            }
            CheckSmartCardDevices();
        }

        private bool InitializeCardReaderDevices()
        {
            foreach (var device in _configuration.Devices)
            {
                if (!_cardReaderDevices.Any(c => c.DeviceName == device.Key))
                {
                    var cardReader = new PcscCardReaderDevice(
                        _serviceProvider,
                        _serviceProvider.GetRequiredService<ILogger<PcscCardReaderDevice>>(),
                        device.Key,
                        _configuration,
                        _contextFactory,
                        _monitorFactory
                        );

                    _cardReaderDevices.Add(cardReader);
                }
            }

            return _cardReaderDevices.Any();
        }

        private void CheckSmartCardDevices()
        {
            if (!GetCardReaderDeviceNames().Any())
            {
                if (_readerAttached)
                {
                    _readerAttached = false;
                    _logger.LogError("No Pcsc smart card device found.");
                }
            }
            else
            {
                if (!_readerAttached)
                {
                    _readerAttached = InitializeCardReaderDevices();
                }
                else
                {
                    _logger.LogDebug("Pcsc smart card device attached.");
                }
            }
        }
    }
}
