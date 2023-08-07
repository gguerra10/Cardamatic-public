using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;



namespace GGuerra.Cardamatic.App.Startup
{
    public static class AppStartup
    {
        private const string AssembliesSeparatorToken = ";";

        [STAThread]
        public static async Task Start(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var currentPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            var plugins = DependencyContext.Default.GetDefaultAssemblyNames().Select(o => o.FullName)
                .Where(o => AppDomain.CurrentDomain.Load(o)
                .CustomAttributes.Any(attribute => attribute.AttributeType == typeof(HostingStartupAttribute)))
                .ToList();

            var hostBuilder = new HostBuilder()
                .ConfigureWebHostDefaults((webHost) =>
                {
                    webHost
                    .ConfigureAppConfiguration((hostContext, configurationBuilder) =>
                    {
                        hostContext.HostingEnvironment.EnvironmentName = environment;
                        configurationBuilder.Sources.Clear();
                        configurationBuilder
                            .SetBasePath(currentPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables();
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddOptions();
                        services.AddLogging(loggingBuilder =>
                        {
                            loggingBuilder.AddNLog();
                        });
                    })
                    .UseUrls("http://0:0")
                    .UseStartup<Startup>()
                    .UseSetting(WebHostDefaults.HostingStartupAssembliesKey, string.Join(AssembliesSeparatorToken, plugins));
                });


            // Roll the ball!
            await hostBuilder.Build().RunAsync();
        }
    }
}
