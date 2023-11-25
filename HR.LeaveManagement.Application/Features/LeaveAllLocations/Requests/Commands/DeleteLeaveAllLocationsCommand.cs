using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Commands
{
    public class DeleteLeaveAllLocationsCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}