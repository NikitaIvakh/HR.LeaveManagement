namespace HR.LeaveManagement.Presentation.Models.LeaveRequests
{
    public class EmployeeLeaveRequestViewVM
    {
        public List<LeaveAllocationViewModelM> LeaveAllocations { get; set; }

        public List<LeaveRequestViewModel> LeaveRequests { get; set; }
    }
}
