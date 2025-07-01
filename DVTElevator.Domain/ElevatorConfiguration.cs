namespace DVTElevator.Domain
{
    public class ElevatorConfiguration
    {
        public ElevatorConfiguration() { }

        public List<Elevator> Elevators { get; set; } = new();
    }
}
