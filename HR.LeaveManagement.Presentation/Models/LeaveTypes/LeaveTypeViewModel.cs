using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.Presentation.Models.LeaveTypes
{
    public class LeaveTypeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Default number of days")]
        public int DefaultDays { get; set; }
    }
}