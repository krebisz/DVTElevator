using DVTElevator.Application;

namespace DVTElevator.UI
{
    public class Settings : ISettings
    {
        public int MinFloor { get; set; } = 1;
        public int MaxFloor { get; set; } = 10;
        public int MinMenuChoice { get; set; } = 1;
        public int MaxMenuChoice { get; set; } = 3;
    }
}
