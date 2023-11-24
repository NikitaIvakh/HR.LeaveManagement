using HR.LeaveManagement.Application.DTOs.LeaveAllLocation;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Commands
{
    public class UpdateLeaveAllLocationsCommand : IRequest<Unit>
    {
        public UpdateLeaveAllLocationDto LeaveAllLocationDto { get; set; }
    }
}