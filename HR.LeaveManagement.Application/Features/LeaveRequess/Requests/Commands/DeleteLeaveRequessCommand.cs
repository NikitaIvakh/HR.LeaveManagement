using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequess.Requests.Commands
{
    public class DeleteLeaveRequessCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}