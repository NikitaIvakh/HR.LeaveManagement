using AutoMapper;
using HR.LeaveManagement.Application.DTOs;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Queries;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Handlers.Queries
{
    public class GetLeaveAllLocationListRequestHandler : IRequestHandler<GetLeaveAllLocationListRequest, List<LeaveAllLocationDto>>
    {
        private readonly ILeaveAllLocationRepository _leaveAllLocationRepository;
        private readonly IMapper _mapper;

        public GetLeaveAllLocationListRequestHandler(ILeaveAllLocationRepository leaveAllLocationRepository, IMapper mapper)
        {
            _leaveAllLocationRepository = leaveAllLocationRepository;
            _mapper = mapper;
        }

        public async Task<List<LeaveAllLocationDto>> Handle(GetLeaveAllLocationListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllLocations = await _leaveAllLocationRepository.GetAllAsync();
            return _mapper.Map<List<LeaveAllLocationDto>>(leaveAllLocations);
        }
    }
}