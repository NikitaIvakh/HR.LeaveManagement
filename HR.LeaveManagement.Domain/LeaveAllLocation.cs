namespace HR.LeaveManagement.Domain
{
    public class LeaveAllLocation
    {
        public int Id { get; set; }

        public int NumbersOfDays { get; set; }

        public DateTime DateCreated { get; set; }

        public LeaveType LeaveType { get; set; }

        public int LeaveTypeId { get; set; }

        public int Period { get; set; }
    }
}