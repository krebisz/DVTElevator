namespace DVTElevator.Application
{
    public interface IConsoleService
    {
        void ShowElevatorStatuses(List<ElevatorStatus> statuses = null);

        void RunMenu();

        int ReadPassengerCount();

        int ReadFloorNumber(int min, int max, bool isDropOff);

        int ReadChoice(int min, int max);
    }
}