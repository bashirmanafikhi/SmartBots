using SmartBots.Application;
using SmartBots.Application.Interfaces;
using SmartBots.Data.Models;

namespace SmartBots.Infrastructure.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IGenericRepository<Todo> _repository;

        public TodoRepository(IGenericRepository<Todo> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Todo item)
        {
            await _repository.AddAsync(item);
        }

        public async Task DeleteAsync(Todo item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<IList<Todo>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CompleteAsync(Todo item)
        {
            item.Complete();
            await _repository.UpdateAsync(item);
        }

        public async Task UncompleteAsync(Todo item)
        {
            item.Uncomplete();
            await _repository.UpdateAsync(item);
        }
    }
}
