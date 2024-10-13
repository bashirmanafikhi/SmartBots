using Microsoft.EntityFrameworkCore;
using SmartBots.Application.Features.Todos;
using SmartBots.Application.Interfaces;
using SmartBots.Data.Models;
using System.Linq.Expressions;

namespace SmartBots.Infrastructure.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IUserOwnedEntityRepository<Todo> _repository;

        public TodoRepository(IUserOwnedEntityRepository<Todo> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Todo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> AddAsync(Todo item, CancellationToken cancellationToken = default)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await _repository.AddAsync(item, cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAsync(Todo item, CancellationToken cancellationToken = default)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await _repository.DeleteAsync(item, cancellationToken);
            return true;
        }

        public async Task<IList<Todo>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<IList<Todo>> GetFilteredAsync(
            Expression<Func<Todo, bool>> filter,
            CancellationToken cancellationToken = default)
        {
            return await _repository.GetFilteredAsync(filter, cancellationToken);
        }

        public IQueryable<Todo> Query()
        {
            return _repository.Query();
        }

        public async Task<List<TodoDto>> GetCurrentUserItems(
            CancellationToken cancellationToken = default)
        {
            return await _repository.GetCurrentUserItemsAsync<TodoDto>(cancellationToken);
        }

    }
}
