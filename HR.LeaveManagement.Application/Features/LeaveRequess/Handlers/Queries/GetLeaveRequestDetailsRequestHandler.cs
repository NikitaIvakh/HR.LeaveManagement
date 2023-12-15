using MediatR;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequess.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;

namespace HR.LeaveManagement.Application.Features.LeaveRequess.Handlers.Queries
{
    public class GetLeaveRequestDetailsRequestHandler : IRequestHandler<GetLeaveRequestDetailsRequest, LeaveRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetLeaveRequestDetailsRequestHandler(IMapper mapper, IUserService userService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailsRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = _mapper.Map<LeaveRequestDto>(await _unitOfWork.LeaveRequestRepository.GetLeaveRequestWithDetails(request.Id));
            leaveRequest.Employee = await _userService.GetEmployeeAsync(leaveRequest.RequestingEmployeeId);
            return leaveRequest;
        }
    }
}