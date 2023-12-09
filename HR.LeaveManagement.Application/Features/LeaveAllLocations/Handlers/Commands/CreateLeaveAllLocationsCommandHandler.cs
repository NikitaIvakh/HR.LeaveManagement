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
        private readonly ILeaveAllLocationRepository _leaveAllLocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateLeaveAllLocationsCommandHandler(ILeaveAllLocationRepository leaveAllLocationRepository, IMapper mapper, IUserService userService, ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveAllLocationRepository = leaveAllLocationRepository;
            _mapper = mapper;
            _userService = userService;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveAllLocationsCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveAllLocationDtoValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllLocationDto, cancellationToken);

            if (validationResult.IsValid is false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }

            else
            {
                var leaveType = await _leaveTypeRepository.GetAsync(request.LeaveAllLocationDto.LeaveTypeId);
                var employees = await _userService.GetEmployeesAsync();
                var period = DateTime.Now.Year;
                var allLocations = new List<LeaveAllLocation>();

                foreach (var employee in employees)
                {
                    if (await _leaveAllLocationRepository.AllLocationExists(employee.Id, leaveType.Id, period))
                        continue;

                    allLocations.Add(new LeaveAllLocation
                    {
                        EmployeeId = employee.Id,
                        LeaveTypeId = leaveType.Id,
                        NumbersOfDays = leaveType.DefaultDays,
                        Period = period,
                    });
                }

                await _leaveAllLocationRepository.AddLocation(allLocations);
            }

            var leaveAllLocation = _mapper.Map<LeaveAllLocation>(request.LeaveAllLocationDto);
            leaveAllLocation = await _leaveAllLocationRepository.CreateAsync(leaveAllLocation);

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = leaveAllLocation.Id;

            return response;
        }
    }
}