using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllLocation.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Handlers.Commands
{
    public class CreateLeaveAllLocationsCommandHandler : IRequestHandler<CreateLeaveAllLocationsCommand, BaseCommandResponse>
    {
        private readonly ILeaveAllLocationRepository _leaveAllLocationRepository;
        private readonly IMapper _mapper;

        public CreateLeaveAllLocationsCommandHandler(ILeaveAllLocationRepository leaveAllLocationRepository, IMapper mapper)
        {
            _leaveAllLocationRepository = leaveAllLocationRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveAllLocationsCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveAllLocationDtoValidator(_leaveAllLocationRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllLocationDto, cancellationToken);

            if (validationResult.IsValid is false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
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