using SmartBots.Data.Models;

namespace SmartBots.Application
{
    public interface ITodoRepository
    {
        Task<IList<Todo>> GetAllAsync();
        Task AddAsync(Todo item);
        Task DeleteAsync(Todo item);
        Task CompleteAsync(Todo item);
        Task UncompleteAsync(Todo item);
    }
}
