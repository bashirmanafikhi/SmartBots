using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartBots.Application.Common;
using SmartBots.Application.Common.Mappings;
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
        private readonly IMapper _mapper;
        protected DbSet<T> _dbSet => _dbContext.Set<T>();  // Lazy initialization

        public GenericRepository(ApplicationDbContext dbContext, ILogger<GenericRepository<T>> logger, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
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

        public async Task<(List<TDestination> Result, int Total)> GetFilteredAndProjectToAsync<TDestination>(
        Expression<Func<T, bool>> predicate,
        Paging? paging = null,
        Expression<Func<T, object>>? orderBy = null,
        CancellationToken cancellationToken = default) where TDestination : IMapFrom<T>
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(predicate);

            if (orderBy is not null)
                query = query.OrderBy(orderBy);

            else if (paging is not null)
                query = query.OrderBy(e => e.Id);

            int total;
            if (paging is not null)
                (total, query) = await ApplyPagingAsync(query, paging, cancellationToken);

            else
                total = await query.CountAsync(cancellationToken);

            var result = await query
                .ProjectTo<TDestination>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return (result, total);
        }

        private async Task<(int Total, IQueryable<T>)> ApplyPagingAsync(
            IQueryable<T> query,
            Paging paging,
            CancellationToken cancellationToken = default)
        {
            var total = await query.CountAsync(cancellationToken);

            var skip = (paging.PageNumber - 1) * paging.PageSize;
            var take = paging.PageSize;

            var pagingQuery = query.Skip(skip).Take(take);

            return (total, pagingQuery);
        }

        public IQueryable<T> Query()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
