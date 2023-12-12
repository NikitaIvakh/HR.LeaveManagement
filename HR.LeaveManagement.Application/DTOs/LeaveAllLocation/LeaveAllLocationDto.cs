using HR.LeaveManagement.Application.DTOs.Common;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Models.Identity;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllLocation
{
    public class LeaveAllLocationDto : BaseDto
    {
        public int NumbersOfDays { get; set; }

        public LeaveTypeDto LeaveType { get; set; }

        public Employee Employee { get; set; }

        public string EmployeeId { get; set; }

        public int Period { get; set; }
    }
}