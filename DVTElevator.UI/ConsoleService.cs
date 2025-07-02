using DVTElevator.Application;
using Microsoft.Extensions.Logging;

namespace DVTElevator.UI
{
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
                Console.WriteLine("=== DVT Elevator Simulation ===");
                Console.WriteLine("[1] Call Elevator");
                Console.WriteLine("[2] Show Elevator Status");
                Console.WriteLine("[3] Exit");
                Console.WriteLine("===============================");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while displaying the main menu.");
                Console.WriteLine("An unexpected error occurred while displaying the menu.");
            }
        }

        public void ShowElevatorStatus(List<ElevatorStatus>? statuses = null)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Elevator Status ===");

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
                _logger.LogError(ex, "Error while showing elevator status.");
                Console.WriteLine("An error occurred while retrieving elevator statuses.");
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

                Console.WriteLine("Invalid passenger count.");
                return ReadPassengerCount();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while reading passenger count.");
                Console.WriteLine("Error reading input. Try again.");
                return ReadPassengerCount();
            }
        }

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
                _logger.LogError(ex, $"Error reading floor {floorType} number.");
                Console.WriteLine("Error reading input. Try again.");
                return ReadFloorNumber(min, max, isDropOff);
            }
        }

        public int ReadChoice(int min, int max)
        {
            try
            {
                Console.Write("Select Option: ");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice.");
                return ReadChoice(min, max);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while reading menu choice.");
                Console.WriteLine("Error reading input. Try again.");
                return ReadChoice(min, max);
            }
        }
    }
}
