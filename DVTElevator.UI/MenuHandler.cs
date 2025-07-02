using DVTElevator.Application;
using Microsoft.Extensions.Logging;

namespace DVTElevator.UI
{
    /// <summary>
    /// Class MenuHandler is the main entry point for the application. The class is responsible for displaying the main menu and handling user input, and runs in a loop until the user exits the application. 
    /// From here, the user can choose to call an elevator, view the status of elevators, or exit the application via the called upon Elevator Controller and the Console Service.
    /// </summary>
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

                    //Case 4: Can be used to simulate the movement of an elevator in real-time by creating a loop that repeatedly calls the ShowElevatorStatus method, until all elevators go idle.
                    //        A separate, event-driven thread could be used to receive user input from the console from the simulatede movement of the elevators in order to handle both simultaneously, in real-time.
                    //        Due to time constraints, this was not implemented. Rather a call to either status to move the elevators, or make a different menu option provides step-by-step movement of the elevators.
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
                            _consoleService.ShowElevatorStatuses();
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
                    _logger.LogError(ex, "Error in MenuHandler.cs");
                    Console.WriteLine($"An Error occurred running the menu: {ex.Message}");
                }
            }
        }
    }
}
