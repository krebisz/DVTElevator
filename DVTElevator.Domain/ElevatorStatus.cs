using DVTElevator.Domain;

namespace DVTElevator
{
    public class ElevatorStatus
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public int PassengerFloor { get; set; }
        public int DestinationFloor { get; set; }
        public bool IsMoving { get; set; }
        public bool IsDropOff { get; set; }
        public int PassengerCount { get; set; }
        public int Capacity { get; set; }
        public Direction Direction { get; set; }

        public ElevatorStatus() 
        { 
        }

        public ElevatorStatus(int id, int currentFloor, int passengerFloor, int destinationFloor,  bool isMoving, bool isDropOff, int passengerCount, int capacity, Direction direction)
        {
            Id = id;
            CurrentFloor = currentFloor;
            PassengerFloor = passengerFloor;
            DestinationFloor = destinationFloor;
            IsMoving = isMoving;
            IsDropOff = isDropOff;
            PassengerCount = passengerCount;
            Capacity = capacity;
            Direction = direction;
        }

        public string ToConsoleString()
        {
            string TripType = "NA";

            if (IsMoving) 
            {
                if (IsDropOff)
                {
                    TripType = "Drop Off";
                }
                else
                {
                    TripType = "Pick Up";
                }
            }

            string statusString = $"Elevator {Id} | Floor: {CurrentFloor} | " + $"Dir: {Direction} | Moving: {IsMoving}  | TripType: {TripType} | " + $"Passengers: {PassengerCount}/{Capacity}";

            return statusString;
        }
    }
}