using HR.LeaveManagement.Application.DTOs.Common;

namespace HR.LeaveManagement.Application.DTOs
{
    public class LeaveAllLocationDto : BaseDto
    {
        public int NumbersOfDays { get; set; }

        public LeaveTypeDto LeaveType { get; set; }

        public int LeaveTypeId { get; set; }

        public int Period { get; set; }
    }
}