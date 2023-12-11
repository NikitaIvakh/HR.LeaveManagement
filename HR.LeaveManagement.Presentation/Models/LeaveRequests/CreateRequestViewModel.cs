using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.Presentation.Models.LeaveRequests
{
    public class CreateRequestViewModel
    {
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public SelectList? LeaveTypes { get; set; }

        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }

        [MaxLength(300)]
        [Display(Name = "Comments")]
        public string RequestComments { get; set; }
    }
}