using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
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
            var leaveAllLocation = await _leaveAllLocationRepository.GetAsync(request.Id) 
                ?? throw new NotFoundException(nameof(LeaveAllLocation), request.Id);

            await _leaveAllLocationRepository.DeleteAsync(leaveAllLocation);
            return Unit.Value;
        }
    }
}