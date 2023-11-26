using HR.LeaveManagement.Application.Persistence.Contracts;
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
    }
}