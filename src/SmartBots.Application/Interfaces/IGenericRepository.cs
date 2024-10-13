using SmartBots.Data.Models;
using SmartBots.Domain.Interfaces;
using System.Linq.Expressions;

namespace SmartBots.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

        Task<List<T>> GetFilteredAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default
        );

        // Optional: Move to another interface if needed.
        Task<List<TDestination>> ProjectAllToAsync<TDestination>(
            AutoMapper.IConfigurationProvider configurationProvider,
            CancellationToken cancellationToken = default
        );

        IQueryable<T> Query();
    }
}
