using HR.LeaveManagement.Presentation.Models.LeaveRequests;
using HR.LeaveManagement.Presentation.Services.Base;

namespace HR.LeaveManagement.Presentation.Contracts
{
    public interface ILeaveRequestService
    {
        Task<BaseResponse<int>> CreateLeaveRequestAsync(CreateRequestViewModel createRequestViewModel);

        Task<BaseResponse<int>> DeleteLeaveRequest(int id);
    }
}