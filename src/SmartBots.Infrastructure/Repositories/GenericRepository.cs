using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Common;
using SmartBots.Infrastructure.Data;
using System.Linq.Expressions;

namespace SmartBots.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<GenericRepository<T>> _logger;
        private DbSet<T> _dbSet => _dbContext.Set<T>();  // Lazy initialization

        public GenericRepository(ApplicationDbContext dbContext, ILogger<GenericRepository<T>> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _logger.LogDebug("Adding entity of type {EntityType}.", typeof(T).Name);
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _logger.LogDebug("Updating entity of type {EntityType}.", typeof(T).Name);
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _logger.LogDebug("Deleting entity of type {EntityType}.", typeof(T).Name);
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
            if (entity == null)
                _logger.LogWarning("Entity with ID {EntityId} not found.", id);

            return entity;
        }

        public async Task<List<TDestination>> ProjectAllToAsync<TDestination>(
            IConfigurationProvider configurationProvider,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .ProjectTo<TDestination>(configurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<T>> GetFilteredAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>();
        }
    }
}
