using GGuerra.Cardamatic.Base.Facade;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Encoding.BinaryString.Decodable;
using GGuerra.Cardamatic.Encoding.BinaryString.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EncodingBinaryStringStartup))]
namespace GGuerra.Cardamatic.Encoding.BinaryString.Startup
{
    public class EncodingBinaryStringStartup : BaseHostingStartup
    {
        public override void ConfigureFacadeServices(IServiceCollection services)
        {
            services.AddTransient<IDecodable, BinaryStringDecodable>();
            services.AddTransient<IEncodable, BinaryStringDecodable>();
            services.AddTransient<IParseable, BinaryStringDecodable>();
        }
    }
}
