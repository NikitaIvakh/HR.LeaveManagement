namespace HR.LeaveManagement.Application.Responses
{
    public class BaseCommandResponse
    {
        public bool Success { get; set; } = true;

        public List<string> Errors { get; set; }

        public string Message { get; set; }
    }
}