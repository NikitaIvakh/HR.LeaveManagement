using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequess.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequess.Handlers.Commands
{
    public class UpdateLeaveRequessCommandHandler : IRequestHandler<UpdateLeaveRequessCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveRequessCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequessCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetAsync(request.Id);

            if (request.LeaveRequest is not null)
            {
                _mapper.Map(request.LeaveRequest, leaveRequest);
                await _leaveRequestRepository.UpdateAsync(leaveRequest);
            }

            else if (request.ChangeLeaveRequestApploval is not null)
            {
                await _leaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApploval.Approved);
            }

            return Unit.Value;
        }
    }
}