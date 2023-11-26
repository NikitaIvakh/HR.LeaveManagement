using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequess.Requests.Commands
{
    public class CreateLeaveRequessCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveRequestDto LeaveRequestDto { get; set; }
    }
}