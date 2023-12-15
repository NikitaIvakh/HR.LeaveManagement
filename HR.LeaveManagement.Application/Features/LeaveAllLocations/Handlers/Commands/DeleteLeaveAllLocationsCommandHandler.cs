using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Handlers.Commands
{
    public class DeleteLeaveAllLocationsCommandHandler : IRequestHandler<DeleteLeaveAllLocationsCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveAllLocationsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteLeaveAllLocationsCommand request, CancellationToken cancellationToken)
        {
            var leaveAllLocation = await _unitOfWork.LeaveAllLocationRepository.GetAsync(request.Id)
                ?? throw new NotFoundException(nameof(LeaveAllLocation), request.Id);

            await _unitOfWork.LeaveAllLocationRepository.DeleteAsync(leaveAllLocation);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}