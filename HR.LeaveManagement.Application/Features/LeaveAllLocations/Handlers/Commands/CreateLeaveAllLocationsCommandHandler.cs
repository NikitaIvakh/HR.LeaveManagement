using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllLocation.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Handlers.Commands
{
    public class CreateLeaveAllLocationsCommandHandler : IRequestHandler<CreateLeaveAllLocationsCommand, int>
    {
        private readonly ILeaveAllLocationRepository _leaveAllLocationRepository;
        private readonly IMapper _mapper;

        public CreateLeaveAllLocationsCommandHandler(ILeaveAllLocationRepository leaveAllLocationRepository, IMapper mapper)
        {
            _leaveAllLocationRepository = leaveAllLocationRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveAllLocationsCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllLocationDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveAllLocationDto, cancellationToken);

            if (validationResult.IsValid is false)
                throw new Exception();

            var leaveAllLocation = _mapper.Map<LeaveAllLocation>(request.LeaveAllLocationDto);
            leaveAllLocation = await _leaveAllLocationRepository.CreateAsync(leaveAllLocation);

            return leaveAllLocation.Id;
        }
    }
}