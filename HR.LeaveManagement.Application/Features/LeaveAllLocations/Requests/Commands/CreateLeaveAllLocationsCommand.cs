using HR.LeaveManagement.Application.DTOs.LeaveAllLocation;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Commands
{
    public class CreateLeaveAllLocationsCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveAllLocationDto LeaveAllLocationDto { get; set; }
    }
}