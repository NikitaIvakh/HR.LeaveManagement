using HR.LeaveManagement.Presentation.Models;
using HR.LeaveManagement.Presentation.Services.Base;

namespace HR.LeaveManagement.Presentation.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeViewModel>> GetLeaveTypes();

        Task<LeaveTypeViewModel> GetLeaveType(int id);

        Task<BaseResponse<int>> CreateLeaveType(CreateLeaveTypeViewModel leaveType);

        Task<BaseResponse<int>> UpdateLeaveType(int id, LeaveTypeViewModel leaveType);

        Task<BaseResponse<int>> DeleteLeaveType(int id);
    }
}