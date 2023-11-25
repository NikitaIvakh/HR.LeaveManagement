namespace HR.LeaveManagement.Application.DTOs.LeaveAllLocation
{
    public interface ILeaveAllLocationDto
    {
        public int NumbersOfDays { get; set; }

        public int LeaveTypeId { get; set; }

        public int Period { get; set; }
    }
}