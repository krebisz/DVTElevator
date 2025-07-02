namespace DVTElevator.Domain
{
    /// <summary>
    /// Container for Elevators that are read from the configuration file into a formatted object (List).
    /// </summary>
    public class ElevatorConfiguration
    {
        public ElevatorConfiguration() { }

        public List<Elevator> Elevators { get; set; } = new();
    }
}
