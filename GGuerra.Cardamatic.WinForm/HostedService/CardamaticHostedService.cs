using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using GGuerra.Cardamatic.WinForm.Application;


namespace GGuerra.Cardamatic.WinForm.HostedService
{
    public class CardamaticHostedService : IHostedService, IDisposable
    {
        private bool _disposed;
        private readonly CardamaticApplication _application;

        public CardamaticHostedService(CardamaticApplication application)
        {
            _application = application;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Run the application.
            _application.Run();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
        }
    }
}
