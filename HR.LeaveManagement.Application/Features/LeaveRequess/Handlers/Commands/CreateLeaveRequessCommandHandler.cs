using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Features.LeaveRequess.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using FluentValidation.Results;
using System.Security.Claims;
using HR.LeaveManagement.Application.Constants;

namespace HR.LeaveManagement.Application.Features.LeaveRequess.Handlers.Commands
{
    public class CreateLeaveRequessCommandHandler : IRequestHandler<CreateLeaveRequessCommand, BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILeaveAllLocationRepository _leaveAllLocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveRequessCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IEmailSender emailSender,
            IHttpContextAccessor httpContextAccessor, ILeaveAllLocationRepository leaveAllLocationRepository, ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _leaveAllLocationRepository = leaveAllLocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequessCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
            var validatorResult = await validator.ValidateAsync(request.LeaveRequestDto, cancellationToken);
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(key => key.Type == CustomClaimTypes.Uid)?.Value;

            var leaveAllLocation = await _leaveAllLocationRepository.GetUserAllLocationsAsync(userId, request.LeaveRequestDto.LeaveTypeId);

            if (leaveAllLocation is null)
                validatorResult.Errors.Add(new ValidationFailure(nameof(request.LeaveRequestDto.LeaveTypeId), "You do not have any allocations for this leave type"));

            else
            {
                int daysRequest = (int)(request.LeaveRequestDto.EndDate - request.LeaveRequestDto.StartDate).TotalDays;

                if (daysRequest > leaveAllLocation.NumbersOfDays)
                {
                    validatorResult.Errors.Add(new ValidationFailure(nameof(request.LeaveRequestDto.EndDate), "You do not have enough days for this request"));
                }
            }

            if (validatorResult.IsValid is false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
            }

            else
            {
                var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
                leaveRequest.RequestingEmployeeId = userId;
                leaveRequest = await _leaveRequestRepository.CreateAsync(leaveRequest);

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = leaveRequest.Id;

                try
                {
                    var emailAddress = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                    var email = new Email
                    {
                        To = emailAddress,
                        Body = $"Your leave request for {request.LeaveRequestDto.StartDate:D} to {request.LeaveRequestDto.EndDate:D} " +
                        $"has been submited successfully.",
                        Subject = "Leave request submited",
                    };

                    await _emailSender.SendEmailAsync(email);
                }

                catch (Exception exception)
                {
                    throw new BadRequestException(exception.Message);
                }
            }

            return response;
        }
    }
}