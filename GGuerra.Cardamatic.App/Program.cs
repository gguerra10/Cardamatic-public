using System.Threading.Tasks;
using GGuerra.Cardamatic.App.Startup;


namespace GGuerra.Cardamatic.App
{
    static class Program
    {
        public async static Task Main(string [] args)
        {
            // Roll the ball.
            await AppStartup.Start(args);
        }
    }
}
