using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly HRLeaveManagementDbContext _context;

        public LeaveTypeRepository(HRLeaveManagementDbContext context) : base(context)
        {
            _context = context;
        }
    }
}