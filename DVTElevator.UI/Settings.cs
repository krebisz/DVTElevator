using DVTElevator.Application;

namespace DVTElevator.UI
{
    /// <summary>
    /// This sets the boundaries on the input parameters that are accepted from the user.
    /// </summary>
    public class Settings : ISettings
    {
        public int MinFloor { get; set; } = 1;
        public int MaxFloor { get; set; } = 10;
        public int MinMenuChoice { get; set; } = 1;
        public int MaxMenuChoice { get; set; } = 3;
    }
}
