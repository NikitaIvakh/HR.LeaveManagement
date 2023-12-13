using HR.LeaveManagement.Presentation.Models.LeaveAllLocations;

namespace HR.LeaveManagement.Presentation.Models.LeaveRequests
{
    public class EmployeeLeaveRequestViewVM
    {
        public List<LeaveAllocationViewModel> LeaveAllocations { get; set; }

        public List<LeaveRequestViewModel> LeaveRequests { get; set; }
    }
}