using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GGuerra.Cardamatic.CardReader.Interface.Facade;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.WinForm.Schemas;
using GGuerra.Cardamatic.WinForm.KeySets;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;

namespace GGuerra.Cardamatic.WinForm.Application
{

    public class CardamaticApplication
    {
        private readonly ILogger<CardamaticApplication> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISchemaFactory _schemaFactory;
        private readonly IKeySetFactory _keySetFactory;
        private readonly IDecoderManager _decoderManager;
        private readonly ICardReaderManager _cardReaderManager;
        private readonly IMifareClassicService _mifareClassicService;
        private readonly IDesfireService _desfireService;
        private readonly IEnumerable<ISamService> _samServices;

        public CardamaticApplication(
            ILogger<CardamaticApplication> logger,
            IServiceProvider serviceProvider,
            ISchemaFactory schemaFactory,
            IKeySetFactory keySetFactory,
            IDecoderManager decoderManager,
            ICardReaderManager cardReaderManager,
            IMifareClassicService mifareClassicService,
            IDesfireService desfireService,
            IEnumerable<ISamService> samServices
            )
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _schemaFactory = schemaFactory;
            _keySetFactory = keySetFactory;
            _decoderManager = decoderManager;
            _cardReaderManager = cardReaderManager;
            _mifareClassicService = mifareClassicService;
            _desfireService = desfireService;
            _samServices = samServices;
        }

        public void Run()
        {
            _logger.LogInformation("Running CardamaticApplication...");
            System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.SystemAware);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            var formMain = new FormMain(
                    _serviceProvider.GetRequiredService<ILogger<FormMain>>(),
                    _schemaFactory,
                    _keySetFactory,
                    _decoderManager,
                    _cardReaderManager,
                    _mifareClassicService,
                    _desfireService,
                    _samServices
                    );
            formMain.FormClosed += FormMain_FormClosed;
            System.Windows.Forms.Application.Run(formMain);
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _logger.LogInformation("Stopping CardamaticApplication...");
            var appLifeTime = _serviceProvider.GetRequiredService<IHostApplicationLifetime>();
            appLifeTime.StopApplication();
        }
    }
}
