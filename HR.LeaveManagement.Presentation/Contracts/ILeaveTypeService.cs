using HR.LeaveManagement.Presentation.Models;
using HR.LeaveManagement.Presentation.Services.Base;

namespace HR.LeaveManagement.Presentation.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeViewModel>> GetLeaveTypesAsync();

        Task<LeaveTypeViewModel> GetLeaveTypeAsync(int id);

        Task<BaseResponse<int>> CreateLeaveTypeAsync(CreateLeaveTypeViewModel leaveType);

        Task<BaseResponse<int>> UpdateLeaveTypeAsync(int id, LeaveTypeViewModel leaveType);

        Task<BaseResponse<int>> DeleteLeaveTypeAsync(int id);
    }
}