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
using System.IdentityModel.Tokens.Jwt;
using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Features.LeaveRequess.Handlers.Commands
{
    public class CreateLeaveRequessCommandHandler : IRequestHandler<CreateLeaveRequessCommand, BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILeaveAllLocationRepository _leaveAllLocationRepository;

        public CreateLeaveRequessCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IEmailSender emailSender,
            IHttpContextAccessor httpContextAccessor, ILeaveAllLocationRepository leaveAllLocationRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _leaveAllLocationRepository = leaveAllLocationRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequessCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidator(_leaveRequestRepository);
            var validatorResult = await validator.ValidateAsync(request.LeaveRequestDto, cancellationToken);
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(key => key.Type == "uid")?.Value;

            var leaveAllLocation = await _leaveAllLocationRepository.GetUserAllLocationsAsync(userId, request.LeaveRequestDto.LeaveTypeId);
            int daysRequest = (int)(request.LeaveRequestDto.EndDate - request.LeaveRequestDto.StartDate).TotalDays;

            if (daysRequest > leaveAllLocation.NumbersOfDays)
            {
                validatorResult.Errors.Add(new ValidationFailure(nameof(request.LeaveRequestDto.EndDate), "You do not have enough days for this request"));
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

                var emailAddress = _httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Email).Value;

                var email = new Email
                {
                    To = emailAddress,
                    Body = $"Your leave request for {request.LeaveRequestDto.StartDate:D} to {request.LeaveRequestDto.EndDate:D} " +
                    $"has been submited successfully.",
                    Subject = "Leave request submited",
                };

                try
                {
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