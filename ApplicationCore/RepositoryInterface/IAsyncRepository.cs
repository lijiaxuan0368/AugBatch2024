using System.Linq.Expressions;

namespace ApplicationCore.RepositoryInterface
{
    public interface IAsyncRepository<T> where T : class
    {
        // Async -> Task
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null);

    }
}
