using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Common;
using SmartBots.Infrastructure.Data;
using System.Linq.Expressions;

namespace SmartBots.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task UpdateAsync(T entity)
        {
            T exist = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<TDestination>> ProjectAllToAsync<TDestination>(IConfigurationProvider configurationProvider, CancellationToken cancellationToken)
        {
            return await _dbContext
                .Set<T>()
                .ProjectTo<TDestination>(configurationProvider)
                .ToListAsync(cancellationToken);
        }

        public Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbContext
                .Set<T>()
                .Where(predicate)
                .ToListAsync();
        }
    }
}
