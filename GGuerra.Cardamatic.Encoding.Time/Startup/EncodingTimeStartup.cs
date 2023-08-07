using GGuerra.Cardamatic.Base.Facade;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Encoding.Time.Decodable;
using GGuerra.Cardamatic.Encoding.Time.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EncodingTimeStartup))]
namespace GGuerra.Cardamatic.Encoding.Time.Startup
{
    public class EncodingTimeStartup : BaseHostingStartup
    {
        public override void ConfigureFacadeServices(IServiceCollection services)
        {
            services.AddTransient<IDecodable, TimeDecodable>();
            services.AddTransient<IEncodable, TimeDecodable>();
            services.AddTransient<IParseable, TimeDecodable>();
        }
    }
}
