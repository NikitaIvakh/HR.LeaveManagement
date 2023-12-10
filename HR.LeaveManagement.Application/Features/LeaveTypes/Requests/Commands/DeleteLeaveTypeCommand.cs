using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands
{
    public class DeleteLeaveTypeCommand : IRequest<BaseCommandResponse>
    {
        public DeleteLeaveTypeDto DeleteLeaveTypeDto { get; set; }
    }
}