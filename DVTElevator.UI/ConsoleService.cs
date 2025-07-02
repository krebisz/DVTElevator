using DVTElevator.Application;
using Microsoft.Extensions.Logging;

namespace DVTElevator.UI
{
    /// <summary>
    /// The Console Service is responsole for handling the input and output of the application. Based upong the choices the user makes, the Console Service will call the Elevator Controller, 
    /// and handle the various menu options made in the Menu Handler.
    /// </summary>
    public class ConsoleService : IConsoleService
    {
        private readonly IElevatorController _controller;
        private readonly ILogger<ConsoleService> _logger;

        public ConsoleService(IElevatorController controller, ILogger<ConsoleService> logger)
        {
            _controller = controller;
            _logger = logger;
        }

        public void RunMenu()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== DVT Elevator Application ===");
                Console.WriteLine("[1] Call Elevator");
                Console.WriteLine("[2] Show Elevator Statuses");
                Console.WriteLine("[3] Exit");
                Console.WriteLine("===============================");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error displaying menu in: ConsoleService.RunMenu().");
                Console.WriteLine($"Error displaying the Main Menu: {ex.Message}");
            }
        }

        /// <summary>
        /// Returns the current status of all the elevators after a status update has been made for each of them.
        /// Each call to this method moves along the elevator in motion, and displays their newly updated status, simulating floor to floor movement.
        /// </summary>
        /// <param name="statuses"></param>
        public void ShowElevatorStatuses(List<ElevatorStatus>? statuses = null)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Elevator Status ===");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("*NOTE: Each call to 'Show Elevator Status' [2] in the Menu simulates the next movement an elevator in motion will take.");
                Console.WriteLine("By repeatedly calling this menu option, you can simulate the movement of an elevator in real-time.");
                Console.WriteLine("This is done so a user can call other menu options while the elevator is in motion.*");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                var displayStatuses = statuses ?? _controller.GetElevatorStatuses();

                foreach (ElevatorStatus status in displayStatuses)
                {
                    Console.WriteLine(status.ToConsoleString());
                }

                Console.WriteLine("=========================");
                Console.WriteLine("Press any key to continue.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error showing elevator statuses in: ConsoleService.ShowElevatorStatus(statuses).");
                Console.WriteLine($"Error displaying elevator statuses: {ex.Message}");
            }
        }

        public int ReadPassengerCount()
        {
            try
            {
                Console.Write("Enter number of passengers: ");

                if (int.TryParse(Console.ReadLine(), out int count) && count >= 0)
                {
                    return count;
                }

                Console.WriteLine("Invalid Passenger Count.");
                return ReadPassengerCount();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading Input in: ConsoleService.ReadPassengerCount().");
                Console.WriteLine("Error reading Passenger Count. Please try again.");
                return ReadPassengerCount();
            }
        }

        /// <summary>
        /// Used to set the current Passenger and Destination Floor for an Elevator.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="isDropOff"></param>
        /// <returns></returns>
        public int ReadFloorNumber(int min, int max, bool isDropOff)
        {
            string floorType = isDropOff ? "Destination" : "Passenger";

            try
            {
                Console.Write($"Enter {floorType} floor: ");


                if (int.TryParse(Console.ReadLine(), out int floor) && floor >= min && floor <= max)
                {
                    return floor;
                }

                Console.WriteLine("Invalid floor. Please try again.");
                return ReadFloorNumber(min, max, isDropOff);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading input in: ConsoleService.ReadFloorNumber().");
                Console.WriteLine($"Error reading floor {floorType} number. Please try again.");
                return ReadFloorNumber(min, max, isDropOff);
            }
        }

        /// <summary>
        /// Checks the user Menu Option selected.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int ReadChoice(int min, int max)
        {
            try
            {
                Console.Write("Select Option: ");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }

                Console.WriteLine("Invalid Menu Option. Please try again.");
                return ReadChoice(min, max);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading Menu Option in: ConsoleService.ReadChoice().");
                Console.WriteLine($"Error Menu Option. Please try again: {ex}");
                return ReadChoice(min, max);
            }
        }
    }
}
