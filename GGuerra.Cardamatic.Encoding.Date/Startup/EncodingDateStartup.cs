using GGuerra.Cardamatic.Base.Facade;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Encoding.Date.Decodable;
using GGuerra.Cardamatic.Encoding.Date.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EncodingDateStartup))]
namespace GGuerra.Cardamatic.Encoding.Date.Startup
{
    public class EncodingDateStartup : BaseHostingStartup
    {
        public override void ConfigureFacadeServices(IServiceCollection services)
        {
            services.AddTransient<IDecodable, DateDecodable>();
            services.AddTransient<IEncodable, DateDecodable>();
            services.AddTransient<IParseable, DateDecodable>();
        }
    }
}
