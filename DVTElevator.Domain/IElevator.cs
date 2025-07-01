using DVTElevator.Domain;

namespace DVTElevator
{
    public interface IElevator
    {
        int Id { get; }
        int CurrentFloor { get; }
        int PassengerFloor { get; }
        int DestinationFloor { get; }

        int Capacity { get; }
        int PassengerCount { get; }
        Direction Direction { get; }
        bool IsMoving { get; }
        bool IsDropOff { get; }
       

        void AddPassenger(int count);
        void SetDestinationFloor(int destinationFloor);
        void SetPassengerFloor(int floor);
        void Step(); // Simulates a tick or move step

        bool HasCapacity();
    }
}
