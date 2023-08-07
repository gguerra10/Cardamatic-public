using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GGuerra.Cardamatic.Base.Facade;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Encoding.System.Decodable;
using GGuerra.Cardamatic.Encoding.System.Startup;


[assembly: HostingStartup(typeof(EncodingSystemStartup))]
namespace GGuerra.Cardamatic.Encoding.System.Startup
{
    public class EncodingSystemStartup : BaseHostingStartup
    {
        public override void ConfigureFacadeServices(IServiceCollection services)
        {
            services.AddTransient<IDecodable, BooleanDecodable>();
            services.AddTransient<IEncodable, BooleanDecodable>();
            services.AddTransient<IParseable, BooleanDecodable>();
            services.AddTransient<IDecodable, CharDecodable>();
            services.AddTransient<IEncodable, CharDecodable>();
            services.AddTransient<IParseable, CharDecodable>();
            services.AddTransient<IDecodable, ByteDecodable>();
            services.AddTransient<IEncodable, ByteDecodable>();
            services.AddTransient<IParseable, ByteDecodable>();

            services.AddTransient<IDecodable, UnsignedShortDecodable>();
            services.AddTransient<IEncodable, UnsignedShortDecodable>();
            services.AddTransient<IParseable, UnsignedShortDecodable>();
            services.AddTransient<IDecodable, ShortDecodable>();
            services.AddTransient<IEncodable, ShortDecodable>();
            services.AddTransient<IParseable, ShortDecodable>();

            services.AddTransient<IDecodable, UnsignedIntegerDecodable>();
            services.AddTransient<IEncodable, UnsignedIntegerDecodable>();
            services.AddTransient<IParseable, UnsignedIntegerDecodable>(); 
            services.AddTransient<IDecodable, IntegerDecodable>();
            services.AddTransient<IEncodable, IntegerDecodable>();
            services.AddTransient<IParseable, IntegerDecodable>();

            services.AddTransient<IDecodable, LongDecodable>();
            services.AddTransient<IEncodable, LongDecodable>();
            services.AddTransient<IParseable, LongDecodable>();

            services.AddTransient<IDecodable, StringDecodable>();
            services.AddTransient<IEncodable, StringDecodable>();
            services.AddTransient<IParseable, StringDecodable>();
            services.AddTransient<IDecodable, ByteArrayDecodable>();
            services.AddTransient<IEncodable, ByteArrayDecodable>();
            services.AddTransient<IParseable, ByteArrayDecodable>();
        }
    }
}
