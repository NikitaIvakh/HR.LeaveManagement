using AutoMapper;
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
            var leaveAllLocation = await _leaveAllLocationRepository.GetAsync(request.LeaveAllLocationDto.Id);
            _mapper.Map(request.LeaveAllLocationDto, leaveAllLocation);
            await _leaveAllLocationRepository.UpdateAsync(leaveAllLocation);

            return Unit.Value;
        }
    }
}