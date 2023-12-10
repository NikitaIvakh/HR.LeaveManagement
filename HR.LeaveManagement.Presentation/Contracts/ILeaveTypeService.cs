using HR.LeaveManagement.Presentation.Models.LeaveTypes;
using HR.LeaveManagement.Presentation.Services.Base;

namespace HR.LeaveManagement.Presentation.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeViewModel>> GetLeaveTypesAsync();

        Task<LeaveTypeViewModel> GetLeaveTypeAsync(int id);

        Task<BaseResponse<int>> CreateLeaveTypeAsync(CreateLeaveTypeViewModel leaveType);

        Task<BaseResponse<int>> UpdateLeaveTypeAsync(int id, UpdateLeaveTypeViewModel updateLeaveTypeViewModel);

        Task<BaseResponse<int>> DeleteLeaveTypeAsync(int id, DeleteLeaveTypeViewModel leaveTypeViewModel);
    }
}