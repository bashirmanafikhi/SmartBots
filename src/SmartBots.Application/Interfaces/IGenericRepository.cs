using SmartBots.Application.Common;
using SmartBots.Application.Common.Mappings;
using SmartBots.Domain.Interfaces;
using System.Linq.Expressions;

namespace SmartBots.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

        Task<List<T>> GetFilteredAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default
        );

        Task<(List<TDestination> Result, int Total)> GetFilteredAndProjectToAsync<TDestination>(
            Expression<Func<T, bool>> predicate,
            Paging? paging = null,
            Expression<Func<T, object>>? orderBy = null,
            CancellationToken cancellationToken = default) where TDestination : IMapFrom<T>;

        // Optional: Move to another interface if needed.
        Task<List<TDestination>> ProjectAllToAsync<TDestination>(
            AutoMapper.IConfigurationProvider configurationProvider,
            CancellationToken cancellationToken = default
        );

        IQueryable<T> Query();
    }
}
