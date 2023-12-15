using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllLocation;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Handlers.Queries
{
    public class GetLeaveAllLocationDetailsRequestHandler : IRequestHandler<GetLeaveAllLocationDetailsRequest, LeaveAllLocationDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLeaveAllLocationDetailsRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<LeaveAllLocationDto> Handle(GetLeaveAllLocationDetailsRequest request, CancellationToken cancellationToken)
        {
            var leaveAllLocations = await _unitOfWork.LeaveAllLocationRepository.GetLeaveAllLocationWithDetails(request.Id);
            return _mapper.Map<LeaveAllLocationDto>(leaveAllLocations);
        }
    }
}