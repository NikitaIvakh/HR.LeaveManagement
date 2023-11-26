namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface IGeneticRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task<T> CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> Exists(int id);
    }
}