using HR.LeaveManagement.Presentation.Services.Base;

namespace HR.LeaveManagement.Presentation.Contracts
{
    public interface ILeaveAllLocationService
    {
        Task<BaseResponse<int>> CreateLeaveAllLocationAsync(int leaveTypeId);
    }
}