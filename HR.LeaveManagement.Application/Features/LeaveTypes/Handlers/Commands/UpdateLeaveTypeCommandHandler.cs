﻿using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validator;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.LeaveTypeDto, cancellationToken);

            if (validatorResult.IsValid is false)
                throw new ValidationException(validatorResult);

            var leaveType = await _leaveTypeRepository.GetAsync(request.LeaveTypeDto.Id) 
                ?? throw new NotFoundException(nameof(LeaveType), request.LeaveTypeDto.Id);

            _mapper.Map(request.LeaveTypeDto, leaveType);
            await _leaveTypeRepository.UpdateAsync(leaveType);

            return Unit.Value;
        }
    }
}