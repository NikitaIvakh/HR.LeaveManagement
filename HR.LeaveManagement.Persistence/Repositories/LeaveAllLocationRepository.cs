using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllLocationRepository : GenericRepository<LeaveAllLocation>, ILeaveAllLocationRepository
    {
        private readonly HRLeaveManagementDbContext _context;

        public LeaveAllLocationRepository(HRLeaveManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<LeaveAllLocation>> GetAllLocationsWithDetails()
        {
            var leaveAllLocations = await _context.LeaveAllLocations.Include(key => key.LeaveType).ToListAsync();
            return leaveAllLocations;
        }

        public async Task<LeaveAllLocation> GetLeaveAllLocationWithDetails(int id)
        {
            var leaveAllLocation = await _context.LeaveAllLocations.Include(key => key.LeaveType).FirstAsync(key => key.Id == id);
            return leaveAllLocation;
        }

        public async Task<bool> AllLocationExists(string userId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllLocations.AnyAsync(key => key.EmployeeId == userId
                                   && key.LeaveTypeId == leaveTypeId
                                   && key.Period == period);
        }

        public async Task AddLocation(List<LeaveAllLocation> leaveAllLocations)
        {
            await _context.AddRangeAsync(leaveAllLocations);
            await _context.SaveChangesAsync();
        }
    }
}