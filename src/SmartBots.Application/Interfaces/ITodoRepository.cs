using SmartBots.Domain.Entities;
using SmartBots.Application.Features.Todos;
using System.Linq.Expressions;
using SmartBots.Application.Common;

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
        Task<PaginationResponse<TodoDto>> GetCurrentUserItemsWithPaginationAsync(
            Expression<Func<Todo, bool>> predicate,
            Paging? paging,
            Expression<Func<Todo, object>> orderBy,
            CancellationToken cancellationToken = default);
    }
}
