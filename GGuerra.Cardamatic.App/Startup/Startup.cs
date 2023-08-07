using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace GGuerra.Cardamatic.App.Startup
{
    internal class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provider => Configuration as IConfigurationRoot);
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
