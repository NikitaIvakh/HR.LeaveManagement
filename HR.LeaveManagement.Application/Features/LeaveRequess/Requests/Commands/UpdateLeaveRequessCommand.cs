using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequess.Requests.Commands
{
    public class UpdateLeaveRequessCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public UpdateLeaveRequestDto LeaveRequest { get; set; }

        public ChangeLeaveRequestApplovalDto ChangeLeaveRequestApploval { get; set; }
    }
}