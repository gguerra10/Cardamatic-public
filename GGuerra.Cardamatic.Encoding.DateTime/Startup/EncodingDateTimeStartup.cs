using GGuerra.Cardamatic.Base.Facade;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Encoding.DateTime.Decodable;
using GGuerra.Cardamatic.Encoding.DateTime.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


[assembly: HostingStartup(typeof(EncodingDateTimeStartup))]
namespace GGuerra.Cardamatic.Encoding.DateTime.Startup
{
    public class EncodingDateTimeStartup : BaseHostingStartup
    {
        public override void ConfigureFacadeServices(IServiceCollection services)
        {
            services.AddTransient<IDecodable, DateTimeDecodable>();
            services.AddTransient<IEncodable, DateTimeDecodable>();
            services.AddTransient<IParseable, DateTimeDecodable>();
        }
    }
}
