using DVTElevator.Application;
using Microsoft.Extensions.Logging;

namespace DVTElevator.UI
{
    public class MenuHandler
    {
        private readonly IConsoleService _consoleService;
        private readonly IElevatorController _elevatorController;
        private readonly ISettings _settings;
        private readonly ILogger<MenuHandler> _logger;

        public MenuHandler(IElevatorController elevatorController, IConsoleService consoleService, ISettings settings, ILogger<MenuHandler> logger)
        {
            _elevatorController = elevatorController;
            _consoleService = consoleService;
            _settings = settings;
            _logger = logger;
        }

        public async Task RunAsync()
        {
            bool exit = false;

            while (!exit)
            {
                try
                {
                    _consoleService.RunMenu();
                    int choice = _consoleService.ReadChoice(_settings.MinMenuChoice, _settings.MaxMenuChoice);

                    switch (choice)
                    {
                        case 1:
                            int count = _consoleService.ReadPassengerCount();
                            int passengerFloor = _consoleService.ReadFloorNumber(_settings.MinFloor, _settings.MaxFloor, isDropOff: false);
                            int destinationFloor = _consoleService.ReadFloorNumber(_settings.MinFloor, _settings.MaxFloor, isDropOff: true);

                            var result = _elevatorController.RequestElevator(passengerFloor, destinationFloor, count);
                            Console.WriteLine(result.Message);
                            break;
                        case 2:
                            _consoleService.ShowElevatorStatus();
                            break;
                        case 3:
                            exit = true;
                            break;
                        default:
                            {
                                break;
                            }
                    }

                    await Task.Delay(1000);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while processing elevator request.");
                    Console.WriteLine("An error occurred. Please try again.");
                }
            }
        }
    }
}