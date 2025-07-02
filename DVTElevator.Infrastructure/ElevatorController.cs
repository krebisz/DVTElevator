using DVTElevator.Application;

namespace DVTElevator.Infrastructure
{
    /// <summary>
    /// Handles the Main Cases made in the Menu from the UI Layer, involving Elevator Operations like Elevator dispatching and Status displays.
    /// </summary>
    public class ElevatorController : IElevatorController
    {
        private List<IElevator> _elevators;
        private List<ElevatorStatus> _elevatorStatuses;

        public ElevatorController(List<IElevator> elevators)
        {
            _elevators = elevators;
            _elevatorStatuses = new List<ElevatorStatus>();
        }

        public ElevatorDispatchResult RequestElevator(int passengerFloor, int destinationFloor, int passengers)
        {
            var elevator = _elevators.Where(e => !e.IsMoving && e.PassengerCount + passengers <= e.Capacity).OrderBy(e => Math.Abs(e.CurrentFloor - passengerFloor)).FirstOrDefault();

            if (elevator == null)
            {
                return ElevatorDispatchResult.Failure("No Available Elevators.");
            }

            elevator.AddPassenger(passengers);
            elevator.SetDestinationFloor(destinationFloor);
            elevator.SetPassengerFloor(passengerFloor);

            return ElevatorDispatchResult.Success(elevator.Id);
        }

        public List<ElevatorStatus> GetElevatorStatuses()
        {
            _elevatorStatuses.Clear();

            foreach (var elevator in _elevators)
            {
                elevator.Step();
                ElevatorStatus elevatorStatus = new ElevatorStatus(elevator.Id, elevator.CurrentFloor, elevator.PassengerFloor, elevator.DestinationFloor, elevator.IsMoving, elevator.IsDropOff, elevator.PassengerCount, elevator.Capacity, elevator.Direction);
                _elevatorStatuses.Add(elevatorStatus);
            }

            return _elevatorStatuses;
        }
    }
}