using DVTElevator.Application;

namespace DVTElevator
{
    public class ElevatorDispatchResult : IElevatorDispatchResult
    {
        public bool IsSuccess { get; }
        public int? ElevatorId { get; }
        public string Message { get; }

        private ElevatorDispatchResult(bool success, int? id, string message)
        {
            IsSuccess = success;
            ElevatorId = id;
            Message = message;
        }

        public static ElevatorDispatchResult Success(int id) => new ElevatorDispatchResult(true, id, $"Elevator {id} is on the way.");
        public static ElevatorDispatchResult Failure(string msg) => new ElevatorDispatchResult(false, null, $"Failed: {msg}");
    }
}
