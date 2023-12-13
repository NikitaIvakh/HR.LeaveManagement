using HR.LeaveManagement.Presentation.Models.LeaveRequests;
using HR.LeaveManagement.Presentation.Services.Base;

namespace HR.LeaveManagement.Presentation.Contracts
{
    public interface ILeaveRequestService
    {
        Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestViewViewModelAsync();

        Task<EmployeeLeaveRequestViewVM> GetEmployeeLeaveRequestViewViewModelAsync();

        Task<BaseResponse<int>> CreateLeaveRequestAsync(CreateRequestViewModel createRequestViewModel);

        Task<LeaveRequestViewModel> GetLeaveRequestAsync(int id);

        Task<BaseResponse<int>> DeleteLeaveRequestAsync(int id);

        Task ApproveLeaveRequestAsync(int id, bool approved);
    }
}