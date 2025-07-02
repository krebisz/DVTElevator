using DVTElevator.Application;
using DVTElevator.Domain;
using DVTElevator.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DVTElevator.UI
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ILoggerFactory, LoggerFactory>();

            services.AddLogging(builder =>
            {
                builder.AddConsole(); 
                builder.SetMinimumLevel(LogLevel.Information); 
            });

            services.AddSingleton<ISettings, Settings>();
            services.AddSingleton<IConsoleService, ConsoleService>();
            services.AddSingleton<IElevatorController, ElevatorController>();
            services.AddSingleton<MenuHandler>();

            //A Config Section from the appsettings.json file is read and mapped to ElevatorConfiguration. It containes the customizable elevator configuration details.
            services.Configure<ElevatorConfiguration>(configuration.GetSection("ElevatorConfiguration"));

            services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<IOptions<ElevatorConfiguration>>().Value;
                return config.Elevators.Cast<IElevator>().ToList();
            });
        }
    }
}
