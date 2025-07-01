namespace DVTElevator.Application
{
    public interface ISettings
    {
        int MaxFloor { get; set; }
        int MinFloor { get; set; }

        int MinMenuChoice { get; set; }
        int MaxMenuChoice { get; set; }
    }
}