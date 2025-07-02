using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DVTElevator.UI
{
    /// <summary>
    /// Services are registered in Startup.cs and injected here. The Menu Handler is the entry point to the application.
    /// </summary>
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
                await menuHandler.RunAsync();

                logger.LogInformation("DVT Elevator Application Exiting...");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Unhandled Exception occurred in Program.cs");
                Console.WriteLine($"Fatal Error. Check logs for details: {ex.Message}");
            }
        }
    }
}