using GGuerra.Cardamatic.Base.Facade;
using GGuerra.Cardamatic.CardReader.Interface.Facade;
using GGuerra.Cardamatic.CardReader.Pcsc.Configuration;
using GGuerra.Cardamatic.CardReader.Pcsc.Facade;
using GGuerra.Cardamatic.CardReader.Pcsc.Facade.Impl;
using GGuerra.Cardamatic.CardReader.Pcsc.HostedService;
using GGuerra.Cardamatic.CardReader.Pcsc.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PCSC;
using PCSC.Monitoring;

[assembly: HostingStartup(typeof(CardReaderStartup))]
namespace GGuerra.Cardamatic.CardReader.Pcsc.Startup
{
    public class CardReaderStartup : BaseHostingStartup
    {
        public override void ConfigureFacadeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PcscCardReaderConfiguration>(configuration.GetSection(nameof(PcscCardReaderConfiguration)));

            services.AddSingleton<IContextFactory>(ContextFactory.Instance);
            services.AddSingleton<IMonitorFactory>(MonitorFactory.Instance);
            services.AddSingleton<IDeviceMonitorFactory>(DeviceMonitorFactory.Instance);

            services.AddSingleton<CardReaderHostedService>();
            services.AddHostedService(serviceProvider =>
            {
                return serviceProvider.GetRequiredService<CardReaderHostedService>();
            });
            services.AddSingleton<ICardReaderManager>(serviceProvider =>
            {
                return (ICardReaderManager)serviceProvider.GetRequiredService<CardReaderHostedService>();
            });
            services.AddTransient<IMifareClassicService, MifareClassicService>();
        }
    }
}
