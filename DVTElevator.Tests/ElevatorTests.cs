using DVTElevator.Domain;
using FluentAssertions;

namespace DVTElevator.Tests
{
    /// <summary>
    /// Elevator Method Tests within Elevator.cs
    /// </summary>
    public class ElevatorTests
    {
        [Fact]
        public void AddPassenger_IncreasePassengerCount()
        {
            var elevator = new Elevator(id: 1, currentFloor: 0, capacity: 5, direction: Direction.Idle);
            elevator.AddPassenger(3);
            elevator.PassengerCount.Should().Be(3);
        }

        [Fact]
        public void AddPassenger_ExceedingCapacity()
        {
            var elevator = new Elevator(id: 1, currentFloor: 0, capacity: 4, direction: Direction.Idle);
            elevator.AddPassenger(4);

            Action act = () => elevator.AddPassenger(1);
            act.Should().Throw<InvalidOperationException>().WithMessage("Elevator Full.");
        }

        [Theory]
        [InlineData(0, 5, Direction.Up)]
        [InlineData(5, 0, Direction.Down)]
        [InlineData(3, 3, Direction.Idle)]
        public void SetPassengerFloor_And_Direction(int currentFloor, int passengerFloor, Direction expected)
        {
            var elevator = new Elevator(id: 1, currentFloor: currentFloor, capacity: 5, direction: Direction.Idle);
            elevator.SetPassengerFloor(passengerFloor);

            elevator.PassengerFloor.Should().Be(passengerFloor);
            elevator.Direction.Should().Be(expected);
        }

        [Fact]
        public void Step_MoveElevator()
        {
            var elevator = new Elevator(id: 1, currentFloor: 0, capacity: 5, direction: Direction.Idle);
            elevator.AddPassenger(1);
            elevator.SetPassengerFloor(2);
            elevator.SetDestinationFloor(5);

            elevator.Step(); //Move to 1
            elevator.CurrentFloor.Should().Be(1);
            elevator.IsDropOff.Should().BeFalse();

            elevator.Step(); //Move to 2 (Passenger Floor)
            elevator.CurrentFloor.Should().Be(2);
            elevator.IsDropOff.Should().BeTrue();

            elevator.Step(); //Move to 3
            elevator.Step(); //Move to 4
            elevator.Step(); //Move to 5 (Destination Floor)
            elevator.CurrentFloor.Should().Be(5);
            elevator.IsDropOff.Should().BeFalse();
            elevator.PassengerCount.Should().Be(0);
            elevator.Direction.Should().Be(Direction.Idle);
        }

        [Fact]
        public void HasCapacity_Return_True()
        {
            var elevator = new Elevator(id: 1, currentFloor: 0, capacity: 3, direction: Direction.Idle);

            elevator.AddPassenger(2);
            elevator.HasCapacity().Should().BeTrue();
        }

        [Fact]
        public void HasCapacity_Return_False()
        {
            var elevator = new Elevator(id: 1, currentFloor: 0, capacity: 2, direction: Direction.Idle);

            elevator.AddPassenger(2);
            elevator.HasCapacity().Should().BeFalse();
        }
    }
}
