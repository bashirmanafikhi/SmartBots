using SmartBots.Domain.Interfaces;
using System.Linq.Expressions;

namespace SmartBots.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate);


        // maybe we can separate these to something like IProjectableRepo 
        Task<List<TDestination>> ProjectAllToAsync<TDestination>(AutoMapper.IConfigurationProvider configurationProvider, CancellationToken cancellationToken);
        //Task<TDestination> ProjectToByIdAsync<TDestination>();
    }
}
