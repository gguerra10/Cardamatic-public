using GGuerra.Cardamatic.Base.Facade;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Encoding.Balance.Decodable;
using GGuerra.Cardamatic.Encoding.Balance.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EncodingBalanceStartup))]
namespace GGuerra.Cardamatic.Encoding.Balance.Startup
{
    public class EncodingBalanceStartup : BaseHostingStartup
    {
        public override void ConfigureFacadeServices(IServiceCollection services)
        {
            services.AddTransient<IDecodable, BalanceDecodable>();
            services.AddTransient<IEncodable, BalanceDecodable>();
            services.AddTransient<IParseable, BalanceDecodable>();
        }
    }
}
