using HR.LeaveManagement.Presentation.Models.LeaveTypes;
using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.Presentation.Models.LeaveRequests
{
    public class LeaveRequestViewModel : CreateRequestViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Date Requested")]
        public DateTime DateRequested { get; set; }

        [Display(Name = "Date Actioned")]
        public DateTime DateActioned { get; set; }

        [Display(Name = "Approval State")]
        public bool? Approved { get; set; }

        public bool Cancelled { get; set; }

        public LeaveTypeViewModel LeaveType { get; set; }

        public EmployeeViewModel Employee { get; set; }
    }
}