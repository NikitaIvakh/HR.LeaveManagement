using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using MediatR;
using HR.LeaveManagement.Application.Features.LeaveRequess.Requests.Commands;

namespace HR.LeaveManagement.Application.Features.LeaveRequess.Handlers.Commands
{
    public class DeleteLeaveRequessCommandHandler : IRequestHandler<DeleteLeaveRequessCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveRequessCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteLeaveRequessCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _unitOfWork.LeaveRequestRepository.GetAsync(request.Id)
                ?? throw new NotFoundException(nameof(LeaveRequest), request.Id);

            await _unitOfWork.LeaveRequestRepository.DeleteAsync(leaveRequest);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}