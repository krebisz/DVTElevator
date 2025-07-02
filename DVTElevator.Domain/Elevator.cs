using DVTElevator.Domain;

namespace DVTElevator
{
    /// <summary>
    /// The Main Elevator Object.
    /// </summary>
    public class Elevator : IElevator
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public Direction Direction { get; set; }
        public ElevatorStatus Status { get; set; }
        public bool IsDropOff { get; set; }
        public int Capacity { get; set; }
        public int PassengerCount { get; set; }
        public int PassengerFloor { get; set; }
        public int DestinationFloor { get; set; }
        public bool IsMoving => PassengerCount > 0;

        public Elevator() { }

        public Elevator(int id, int currentFloor, int capacity, Direction direction)
        {
            Id = id;
            CurrentFloor = currentFloor;
            Capacity = capacity;
            PassengerCount = 0;
            Direction = direction;
            this.DestinationFloor = currentFloor;
        }

        public bool HasCapacity()
        {
            return Capacity - PassengerCount > 0;
        }
  
        public void AddPassenger(int count)
        {
            if (PassengerCount + count > Capacity)
            {
                throw new InvalidOperationException("Elevator Full.");
            }

            PassengerCount += count;
        }

        public void SetDestinationFloor(int destinationFloor)
        {
            DestinationFloor = destinationFloor;
        }

        public void SetPassengerFloor(int floor)
        {
            IsDropOff = false;
            Direction = floor > CurrentFloor ? Direction.Up : floor < CurrentFloor ? Direction.Down : Direction.Idle;
            PassengerFloor = floor;
        }


        /// <summary>
        /// The Main Step Function for the Elevator. It simulates a Tick or a singular Movement between Floors for an Elevator in motion.
        /// </summary>
        public void Step()
        {
            if (IsMoving)
            {
                switch (Direction)
                {
                    case Direction.Down:
                    {
                        CurrentFloor--;
                        break;
                    }
                    case Direction Up:
                    {
                        CurrentFloor++;
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }        

                if (CurrentFloor == PassengerFloor && !IsDropOff)
                {

                    Direction = DestinationFloor > CurrentFloor ? Direction.Up : DestinationFloor < CurrentFloor ? Direction.Down : Direction.Idle;
                    IsDropOff = !IsDropOff;
                }

                if (CurrentFloor == DestinationFloor && IsDropOff)
                {
                    Direction = Direction.Idle;
                    PassengerCount = 0;
                    IsDropOff = !IsDropOff;
                }
            }
        }
    }
}
