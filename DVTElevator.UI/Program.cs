using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DVTElevator.UI
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            var services = new ServiceCollection();
            Startup.ConfigureServices(services, configuration);
            var provider = services.BuildServiceProvider();

            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            logger.LogInformation("DVT Elevator Application Starting...");

            try
            {
                var menuHandler = provider.GetRequiredService<MenuHandler>();

                var cts = new CancellationTokenSource();
                await menuHandler.RunAsync();

                cts.Cancel(); //Stop on exit

                logger.LogInformation("DVT Elevator Application Exiting...");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Unhandled exception occurred.");
                Console.WriteLine("Fatal error. Check logs for details.");
            }
        }
    }
}