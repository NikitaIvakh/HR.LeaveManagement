using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllLocationRepository : IGeneticRepository<LeaveAllLocation>
    {
        Task<LeaveAllLocation> GetLeaveAllLocationWithDetails(int id);

        Task<List<LeaveAllLocation>> GetAllLocationsWithDetails();
    }
}