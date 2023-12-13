using HR.LeaveManagement.Presentation.Models.LeaveTypes;
using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.Presentation.Models.LeaveAllLocations
{
    public class UpdateLeaveAllocationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Number Of Days")]
        [Range(1, 50, ErrorMessage = "Enter Valid Number")]
        public int NumberOfDays { get; set; }

        public LeaveTypeViewModel LeaveType { get; set; }
    }
}