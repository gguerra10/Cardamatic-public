using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GGuerra.Cardamatic.Base.Facade
{
    // Plugin startup abstract class
    public abstract class BaseHostingStartup : IHostingStartup
    {
        protected IConfiguration Configuration { get; private set; }

        public virtual void ConfigureFacadeServices(IServiceCollection services)
        {
            // Add services.
        }

        public virtual void ConfigureFacadeServices(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureFacadeServices(services);
        }

        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                Configuration = context.Configuration;
                ConfigureFacadeServices(services, Configuration);
            });
        }
    }
}
