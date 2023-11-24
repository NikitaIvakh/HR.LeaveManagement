using HR.LeaveManagement.Application.DTOs.LeaveAllLocation;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Commands
{
    public class CreateLeaveAllLocationsCommand : IRequest<int>
    {
        public CreateLeaveAllLocationDto LeaveAllLocationDto { get; set; }
    }
}