using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllLocationRepository : IGeneticRepository<LeaveAllLocation>
    {
        Task<LeaveAllLocation> GetLeaveAllLocationWithDetails(int id);

        Task<List<LeaveAllLocation>> GetAllLocationsWithDetails();

        Task<List<LeaveAllLocation>> GetAllLocationsWithDetails(string userId);

        Task<bool> AllLocationExists(string userId, int leaveTypeId, int period);

        Task AddLocation(List<LeaveAllLocation> leaveAllLocations);

        Task<LeaveAllLocation> GetUserAllLocationsAsync(string userId, int leaveTypeId);
    }
}