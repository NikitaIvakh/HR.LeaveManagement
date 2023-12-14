using HR.LeaveManagement.Application.DTOs.Common;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest
{
    public class ChangeLeaveRequestApplovalDto : BaseDto
    {
        public bool Approved { get; set; }
    }
}