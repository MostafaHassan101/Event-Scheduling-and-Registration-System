using EventSystem.Domain.Entities.Base;
using System.Linq.Expressions;

namespace EventSystem.Domain.Repositories.Base
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
