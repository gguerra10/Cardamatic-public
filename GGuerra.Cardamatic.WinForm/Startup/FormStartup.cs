using GGuerra.Cardamatic.Base.Facade;
using GGuerra.Cardamatic.WinForm.Application;
using GGuerra.Cardamatic.WinForm.HostedService;
using GGuerra.Cardamatic.WinForm.KeySets;
using GGuerra.Cardamatic.WinForm.KeySets.Impl;
using GGuerra.Cardamatic.WinForm.Schemas;
using GGuerra.Cardamatic.WinForm.Schemas.Impl;
using GGuerra.Cardamatic.WinForm.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(FormStartup))]
namespace GGuerra.Cardamatic.WinForm.Startup
{
    public class FormStartup : BaseHostingStartup
    {
        public override void ConfigureFacadeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISchemaFactory, SchemaFactory>();
            services.AddTransient<IKeySetFactory, KeySetFactory>();
            services.AddTransient<CardamaticApplication>();
            services.AddHostedService<CardamaticHostedService>();
        }
    }
}
