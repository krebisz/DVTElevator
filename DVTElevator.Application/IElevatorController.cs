namespace DVTElevator.Application
{
    public interface IElevatorController
    {
        List<ElevatorStatus> GetElevatorStatuses();

        ElevatorDispatchResult RequestElevator(int passengerFloor, int destinationFloor, int passengers);
    }
}