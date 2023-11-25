using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Handlers.Commands
{
    public class DeleteLeaveAllLocationsCommandHandler : IRequestHandler<DeleteLeaveAllLocationsCommand, Unit>
    {
        private readonly ILeaveAllLocationRepository _leaveAllLocationRepository;

        public DeleteLeaveAllLocationsCommandHandler(ILeaveAllLocationRepository leaveAllLocationRepository)
        {
            _leaveAllLocationRepository = leaveAllLocationRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveAllLocationsCommand request, CancellationToken cancellationToken)
        {
            var leaveAllLocation = await _leaveAllLocationRepository.GetAsync(request.Id);
            await _leaveAllLocationRepository.DeleteAsync(leaveAllLocation);

            return Unit.Value;
        }
    }
}