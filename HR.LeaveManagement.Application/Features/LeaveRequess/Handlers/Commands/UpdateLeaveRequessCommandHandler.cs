using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequess.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequess.Handlers.Commands
{
    public class UpdateLeaveRequessCommandHandler : IRequestHandler<UpdateLeaveRequessCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveRequessCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateLeaveRequessCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _unitOfWork.LeaveRequestRepository.GetAsync(request.Id);

            if (request.LeaveRequest is not null)
            {
                var validator = new UpdateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
                var validatorResult = await validator.ValidateAsync(request.LeaveRequest, cancellationToken);

                if (validatorResult.IsValid is false)
                {
                    throw new ValidationException(validatorResult);
                }

                _mapper.Map(request.LeaveRequest, leaveRequest);
                await _unitOfWork.LeaveRequestRepository.UpdateAsync(leaveRequest);
                await _unitOfWork.Save();
            }

            else if (request.ChangeLeaveRequestApploval is not null)
            {
                await _unitOfWork.LeaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApploval.Approved);

                if (request.ChangeLeaveRequestApploval.Approved)
                {
                    var allLocation = await _unitOfWork.LeaveAllLocationRepository.GetUserAllLocationsAsync(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                    int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;

                    allLocation.NumbersOfDays -= daysRequested;
                    await _unitOfWork.LeaveAllLocationRepository.UpdateAsync(allLocation);
                }

                await _unitOfWork.Save();
            }

            return Unit.Value;
        }
    }
}