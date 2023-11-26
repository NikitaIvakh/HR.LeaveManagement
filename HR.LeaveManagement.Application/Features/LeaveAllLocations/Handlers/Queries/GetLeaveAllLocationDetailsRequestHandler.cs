using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllLocation;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Handlers.Queries
{
    public class GetLeaveAllLocationDetailsRequestHandler : IRequestHandler<GetLeaveAllLocationDetailsRequest, LeaveAllLocationDto>
    {
        private readonly ILeaveAllLocationRepository _leaveAllLocationRepository;
        private readonly IMapper _mapper;

        public GetLeaveAllLocationDetailsRequestHandler(ILeaveAllLocationRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveAllLocationRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<LeaveAllLocationDto> Handle(GetLeaveAllLocationDetailsRequest request, CancellationToken cancellationToken)
        {
            var leaveAllLocations = await _leaveAllLocationRepository.GetLeaveAllLocationWithDetails(request.Id);
            return _mapper.Map<LeaveAllLocationDto>(leaveAllLocations);
        }
    }
}