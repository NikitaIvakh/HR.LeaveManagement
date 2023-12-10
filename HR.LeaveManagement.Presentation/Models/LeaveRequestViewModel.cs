using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.Presentation.Models
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

    public class CreateRequestViewModel
    {
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public SelectList LeaveTypes { get; set; }

        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }

        [MaxLength(300)]
        [Display(Name = "Comments")]
        public string RequestComments { get; set; }
    }
}