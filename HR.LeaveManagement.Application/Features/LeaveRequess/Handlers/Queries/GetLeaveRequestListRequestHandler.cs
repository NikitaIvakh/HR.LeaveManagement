using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequess.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Application.Constants;

namespace HR.LeaveManagement.Application.Features.LeaveRequess.Handlers.Queries
{
    public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;

        public GetLeaveRequestListRequestHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserService userService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _contextAccessor = httpContextAccessor;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequests = new List<LeaveRequest>();
            var requests = new List<LeaveRequestListDto>();

            if (request.IsLoggedInUser)
            {
                var userId = _contextAccessor.HttpContext.User.FindFirst(key => key.Type == CustomClaimTypes.Uid)?.Value;
                leaveRequests = await _unitOfWork.LeaveRequestRepository.GetLeaveRequestsWithDetails(userId);

                var employee = await _userService.GetEmployeeAsync(userId);
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

                foreach (var req in requests)
                {
                    req.Employee = employee;
                }
            }

            else
            {
                leaveRequests = await _unitOfWork.LeaveRequestRepository.GetLeaveRequestsWithDetails();
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

                foreach (var req in requests)
                {
                    req.Employee = await _userService.GetEmployeeAsync(req.RequestingEmployeeId);
                }
            }

            return requests;
        }
    }
}