using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllLocation.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using HR.LeaveManagement.Application.Contracts.Identity;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Handlers.Commands
{
    public class CreateLeaveAllLocationsCommandHandler : IRequestHandler<CreateLeaveAllLocationsCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public CreateLeaveAllLocationsCommandHandler(IUserService userService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveAllLocationsCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveAllLocationDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllLocationDto, cancellationToken);

            if (validationResult.IsValid is false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }

            else
            {
                var leaveType = await _unitOfWork.LeaveTypeRepository.GetAsync(request.LeaveAllLocationDto.LeaveTypeId);
                var employees = await _userService.GetEmployeesAsync();
                var period = DateTime.Now.Year;
                var allLocations = new List<LeaveAllLocation>();

                foreach (var employee in employees)
                {
                    if (await _unitOfWork.LeaveAllLocationRepository.AllLocationExists(employee.Id, leaveType.Id, period))
                        continue;

                    allLocations.Add(new LeaveAllLocation
                    {
                        EmployeeId = employee.Id,
                        LeaveTypeId = leaveType.Id,
                        NumbersOfDays = leaveType.DefaultDays,
                        Period = period,
                    });

                }

                await _unitOfWork.LeaveAllLocationRepository.AddLocation(allLocations);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Allocations Successful";
            }

            return response;
        }
    }
}