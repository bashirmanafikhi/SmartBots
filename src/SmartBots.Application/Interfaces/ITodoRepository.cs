using SmartBots.Data.Models;
using System.Linq.Expressions;

namespace SmartBots.Application.Interfaces
{
    public interface ITodoRepository
    {
        Task<Todo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(Todo item, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Todo item, CancellationToken cancellationToken = default);
        Task<IList<Todo>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IList<Todo>> GetFilteredAsync(
            Expression<Func<Todo, bool>> filter,
            CancellationToken cancellationToken = default);
        IQueryable<Todo> Query();
    }
}
