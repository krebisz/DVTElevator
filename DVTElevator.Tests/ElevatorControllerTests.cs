using DVTElevator.Domain;
using DVTElevator.Infrastructure;
using FluentAssertions;
using Moq;

namespace DVTElevator.Tests
{
    /// <summary>
    /// Elevator Controller Method Tests within ElevatorController.cs
    /// </summary>
    public class ElevatorControllerTests
    {
        [Fact]
        public void Request_UnavailableElevator_Return_Success()
        {
            //Arrange
            var elevatorMock = new Mock<IElevator>();
            elevatorMock.SetupGet(e => e.IsMoving).Returns(false);
            elevatorMock.SetupGet(e => e.PassengerCount).Returns(0);
            elevatorMock.SetupGet(e => e.Capacity).Returns(5);
            elevatorMock.SetupGet(e => e.CurrentFloor).Returns(2);
            elevatorMock.SetupGet(e => e.Id).Returns(0);

            var elevators = new List<IElevator> { elevatorMock.Object };
            var controller = new ElevatorController(elevators);

            //Act
            var result = controller.RequestElevator(passengerFloor: 3, destinationFloor: 7, passengers: 2);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.ElevatorId.Should().Be(elevatorMock.Object.Id);
            elevatorMock.Verify(e => e.AddPassenger(2), Times.Once);
            elevatorMock.Verify(e => e.SetDestinationFloor(7), Times.Once);
            elevatorMock.Verify(e => e.SetPassengerFloor(3), Times.Once);
        }

        [Fact]
        public void Request_UnavailableElevator_Return_Failure()
        {
            //Arrange
            var elevatorMock = new Mock<IElevator>();
            elevatorMock.SetupGet(e => e.IsMoving).Returns(true);   //Busy
            elevatorMock.SetupGet(e => e.PassengerCount).Returns(5);
            elevatorMock.SetupGet(e => e.Capacity).Returns(5);      //Full
            elevatorMock.SetupGet(e => e.CurrentFloor).Returns(1);

            var elevators = new List<IElevator> { elevatorMock.Object };
            var controller = new ElevatorController(elevators);

            //Act
            var result = controller.RequestElevator(1, 5, 1);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Failed: No Available Elevators.");
        }

        [Fact]
        public void GetElevatorStatuses_Return_UpdatedStatus()
        {
            //Arrange
            var elevatorMock = new Mock<IElevator>();
            var id = Guid.NewGuid();

            elevatorMock.SetupGet(e => e.Id).Returns(0);
            elevatorMock.SetupGet(e => e.CurrentFloor).Returns(1);
            elevatorMock.SetupGet(e => e.PassengerFloor).Returns(2);
            elevatorMock.SetupGet(e => e.DestinationFloor).Returns(5);
            elevatorMock.SetupGet(e => e.IsMoving).Returns(true);
            elevatorMock.SetupGet(e => e.IsDropOff).Returns(false);
            elevatorMock.SetupGet(e => e.PassengerCount).Returns(3);
            elevatorMock.SetupGet(e => e.Capacity).Returns(5);
            elevatorMock.SetupGet(e => e.Direction).Returns(Direction.Up);

            var elevators = new List<IElevator> { elevatorMock.Object };
            var controller = new ElevatorController(elevators);

            //Act
            var statuses = controller.GetElevatorStatuses();

            //Assert
            statuses.Should().HaveCount(1);
            var status = statuses[0];
            status.Id.Should().Be(0);
            status.CurrentFloor.Should().Be(1);
            status.DestinationFloor.Should().Be(5);
            status.PassengerCount.Should().Be(3);
            status.Capacity.Should().Be(5);

            elevatorMock.Verify(e => e.Step(), Times.Once);
        }
    }
}
