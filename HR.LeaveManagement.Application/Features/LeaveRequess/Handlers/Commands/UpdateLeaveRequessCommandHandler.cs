﻿using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequess.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequess.Handlers.Commands
{
    public class UpdateLeaveRequessCommandHandler : IRequestHandler<UpdateLeaveRequessCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllLocationRepository _leaveAllLocationRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveRequessCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository, ILeaveAllLocationRepository leaveAllLocationRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveAllLocationRepository = leaveAllLocationRepository;
        }

        public async Task<Unit> Handle(UpdateLeaveRequessCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetAsync(request.Id);

            if (request.LeaveRequest is not null)
            {
                var validator = new UpdateLeaveRequestDtoValidator(_leaveTypeRepository);
                var validatorResult = await validator.ValidateAsync(request.LeaveRequest, cancellationToken);

                if (validatorResult.IsValid is false)
                {
                    throw new ValidationException(validatorResult);
                }

                _mapper.Map(request.LeaveRequest, leaveRequest);
                await _leaveRequestRepository.UpdateAsync(leaveRequest);
            }

            else if (request.ChangeLeaveRequestApploval is not null)
            {
                await _leaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApploval.Approved);

                if (request.ChangeLeaveRequestApploval.Approved)
                {
                    var allLocation = await _leaveAllLocationRepository.GetUserAllLocationsAsync(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                    int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;

                    allLocation.NumbersOfDays -= daysRequested;
                    await _leaveAllLocationRepository.UpdateAsync(allLocation);
                }
            }

            return Unit.Value;
        }
    }
}