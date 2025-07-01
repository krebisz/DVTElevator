namespace DVTElevator.Application
{
    public interface IElevatorDispatchResult
    {
        int? ElevatorId { get; }
        bool IsSuccess { get; }
        string Message { get; }

        static abstract ElevatorDispatchResult Failure(string msg);
        static abstract ElevatorDispatchResult Success(int id);
    }
}