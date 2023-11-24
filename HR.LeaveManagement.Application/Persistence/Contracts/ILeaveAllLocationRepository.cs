using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Persistence.Contracts
{
    public interface ILeaveAllLocationRepository : IGeneticRepository<LeaveAllLocation>
    {
        Task<LeaveAllLocation> GetLeaveAllLocationWithDetails(int id);

        Task<List<LeaveAllLocation>> GetAllLocationsWithDetails();
    }
}