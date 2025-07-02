using DVTElevator.Application;
using DVTElevator.Domain;
using DVTElevator.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DVTElevator.Tests
{
    /// <summary>
    /// Simulates the Application staart up, and the main menu options selection.
    /// </summary>
    public class SimulationIntegrationTests
    {
        private readonly ServiceProvider _provider;

        public SimulationIntegrationTests()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "SimulationSettings:ElevatorCount", "2" },
                    { "SimulationSettings:MaxFloor", "10" },
                    { "SimulationSettings:MinFloor", "0" },
                    { "SimulationSettings:MinMenuChoice", "1" },
                    { "SimulationSettings:MaxMenuChoice", "3" }
                })
                .Build();

            var services = new ServiceCollection();

            // Register settings, console service, controller, etc.
            Startup.ConfigureServices(services, config);


            services.AddSingleton<List<IElevator>>(sp => new List<IElevator>
            {
                new Elevator(id: 1, currentFloor: 0, capacity: 5, direction: Direction.Idle),
                new Elevator(id: 2, currentFloor: 3, capacity: 5, direction: Direction.Idle)
            });

            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public void SimulationTest_RunSimulation()
        {
            var controller = _provider.GetRequiredService<IElevatorController>();

            var result = controller.RequestElevator(passengerFloor: 0, destinationFloor: 5, passengers: 1);
            Assert.True(result.IsSuccess);

            var statuses = controller.GetElevatorStatuses();
            Assert.NotEmpty(statuses);
            var elevator = statuses.Find(e => e.Id == result.ElevatorId);

            Assert.NotNull(elevator);
            Assert.True(elevator.PassengerCount > 0);
            Assert.Equal(5, elevator.DestinationFloor);
        }
    }
}