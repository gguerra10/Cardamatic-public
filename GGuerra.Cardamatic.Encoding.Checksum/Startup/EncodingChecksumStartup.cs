using GGuerra.Cardamatic.Base.Facade;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Encoding.Checksum.Decodable;
using GGuerra.Cardamatic.Encoding.Checksum.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EncodingChecksumStartup))]
namespace GGuerra.Cardamatic.Encoding.Checksum.Startup
{
    public class EncodingChecksumStartup : BaseHostingStartup
    {
        public override void ConfigureFacadeServices(IServiceCollection services)
        {
            services.AddTransient<IDecodable, ChecksumDecodable>();
            services.AddTransient<IEncodable, ChecksumDecodable>();
        }
    }
}
