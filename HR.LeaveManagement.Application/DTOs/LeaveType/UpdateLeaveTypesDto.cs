using HR.LeaveManagement.Application.DTOs.Common;

namespace HR.LeaveManagement.Application.DTOs.LeaveType
{
    public class UpdateLeaveTypesDto : BaseDto, ILeaveTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DefaultDays { get; set; }
    }
}