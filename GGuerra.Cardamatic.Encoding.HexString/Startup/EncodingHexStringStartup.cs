using GGuerra.Cardamatic.Base.Facade;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Encoding.HexString.Decodable;
using GGuerra.Cardamatic.Encoding.HexString.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EncodingHexStringStartup))]
namespace GGuerra.Cardamatic.Encoding.HexString.Startup
{
    public class EncodingHexStringStartup : BaseHostingStartup
    {
        public override void ConfigureFacadeServices(IServiceCollection services)
        {
            services.AddTransient<IDecodable, HexStringDecodable>();
            services.AddTransient<IEncodable, HexStringDecodable>();
            services.AddTransient<IParseable, HexStringDecodable>();
        }
    }
}
