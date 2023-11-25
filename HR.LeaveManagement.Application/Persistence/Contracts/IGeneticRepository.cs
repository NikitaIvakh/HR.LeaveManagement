namespace HR.LeaveManagement.Application.Persistence.Contracts
{
    public interface IGeneticRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);

        Task<bool> Exists(int id);
    }
}