using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.Presentation.Models
{
    public class LeaveTypeViewModel : CreateLeaveTypeViewModel
    {
        public int Id { get; set; }
    }

    public class CreateLeaveTypeViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Default number of days")]
        public int DefaulyDays { get; set; }
    }
}