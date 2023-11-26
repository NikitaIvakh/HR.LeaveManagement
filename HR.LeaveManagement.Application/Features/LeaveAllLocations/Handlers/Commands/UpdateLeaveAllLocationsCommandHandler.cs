using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllLocation.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Handlers.Commands
{
    public class UpdateLeaveAllLocationsCommandHandler : IRequestHandler<UpdateLeaveAllLocationsCommand, Unit>
    {
        private readonly ILeaveAllLocationRepository _leaveAllLocationRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveAllLocationsCommandHandler(ILeaveAllLocationRepository leaveAllLocationRepository, IMapper mapper)
        {
            _leaveAllLocationRepository = leaveAllLocationRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveAllLocationsCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllLocationDtoValidator(_leaveAllLocationRepository);
            var validatorResult = await validator.ValidateAsync(request.LeaveAllLocationDto, cancellationToken);

            if (validatorResult.IsValid is false)
                throw new ValidationException(validatorResult);

            var leaveAllLocation = await _leaveAllLocationRepository.GetAsync(request.LeaveAllLocationDto.Id);

            if (leaveAllLocation is null)
                throw new NotFoundException(nameof(leaveAllLocation), request.LeaveAllLocationDto.Id);

            _mapper.Map(request.LeaveAllLocationDto, leaveAllLocation);
            await _leaveAllLocationRepository.UpdateAsync(leaveAllLocation);

            return Unit.Value;
        }
    }
}