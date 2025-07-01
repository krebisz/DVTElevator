namespace DVTElevator.Application
{
    public interface IConsoleService
    {
        void ShowElevatorStatus(List<ElevatorStatus> statuses = null);

        void RunMenu();

        int ReadPassengerCount();

        //int ReadCurrentFloorNumber(int min, int max);

        //int ReadDestinationFloorNumber(int min, int max);

        int ReadFloorNumber(int min, int max, bool isDropOff);

        int ReadChoice(int min, int max);
    }
}