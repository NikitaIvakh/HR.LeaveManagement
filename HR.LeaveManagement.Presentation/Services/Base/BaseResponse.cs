namespace HR.LeaveManagement.Presentation.Services.Base
{
    public class BaseResponse<Type>
    {
        public string Message { get; set; }

        public string ValidationErrors { get; set; }

        public bool Status { get; set; }

        public Type Data { get; set; }
    }
}