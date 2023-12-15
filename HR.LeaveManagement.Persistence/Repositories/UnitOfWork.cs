using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HRLeaveManagementDbContext _context;
        private ILeaveAllLocationRepository _leaveAllLocationRepository;
        private ILeaveRequestRepository _leaveRequestRepository;
        private ILeaveTypeRepository _leaveTypeRepository;

        public UnitOfWork(HRLeaveManagementDbContext context)
        {
            _context = context;
        }

        public ILeaveAllLocationRepository LeaveAllLocationRepository
        {
            get
            {
                return _leaveAllLocationRepository ??= new LeaveAllLocationRepository(_context);
            }
        }

        public ILeaveRequestRepository LeaveRequestRepository
        {
            get
            {
                return _leaveRequestRepository ??= new LeaveRequestRepository(_context);
            }
        }

        public ILeaveTypeRepository LeaveTypeRepository
        {
            get
            {
                return _leaveTypeRepository ??= new LeaveTypeRepository(_context);
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}