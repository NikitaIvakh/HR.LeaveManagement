namespace HR.LeaveManagement.Presentation.Models.LeaveAllLocations
{
    public class ViewLeaveAllocationsViewModel
    {
        public string EmployeeId { get; set; }

        public List<LeaveAllocationViewModel> LeaveAllocations { get; set; }
    }
}