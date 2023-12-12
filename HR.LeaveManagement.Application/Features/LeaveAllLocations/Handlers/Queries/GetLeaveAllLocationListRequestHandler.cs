using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllLocation;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using HR.LeaveManagement.Domain;
using Microsoft.AspNetCore.Http;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Constants;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Handlers.Queries
{
    public class GetLeaveAllLocationListRequestHandler : IRequestHandler<GetLeaveAllLocationListRequest, List<LeaveAllLocationDto>>
    {
        private readonly ILeaveAllLocationRepository _leaveAllLocationRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;

        public GetLeaveAllLocationListRequestHandler(ILeaveAllLocationRepository leaveAllLocationRepository, IMapper mapper,
            IHttpContextAccessor contextAccessor, IUserService userService)
        {
            _leaveAllLocationRepository = leaveAllLocationRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userService = userService;
        }

        public async Task<List<LeaveAllLocationDto>> Handle(GetLeaveAllLocationListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllLocations = new List<LeaveAllLocation>();
            var allLocations = new List<LeaveAllLocationDto>();

            if (request.IsLoggedInUser)
            {
                var userId = _contextAccessor.HttpContext.User.FindFirst(key => key.Type == CustomClaimTypes.Uid)?.Value;
                leaveAllLocations = await _leaveAllLocationRepository.GetAllLocationsWithDetails(userId);

                var employee = await _userService.GetEmployeeAsync(userId);
                allLocations = _mapper.Map<List<LeaveAllLocationDto>>(leaveAllLocations);

                foreach (var location in allLocations)
                {
                    location.Employee = employee;
                }
            }

            else
            {
                leaveAllLocations = await _leaveAllLocationRepository.GetAllLocationsWithDetails();
                allLocations = _mapper.Map<List<LeaveAllLocationDto>>(leaveAllLocations);

                foreach (var location in allLocations)
                {
                    location.Employee = await _userService.GetEmployeeAsync(location.EmployeeId);
                }
            }

            return allLocations;
        }
    }
}