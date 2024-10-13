using SmartBots.Domain.Entities;
using System.Linq.Expressions;

namespace SmartBots.Application.Interfaces
{
    public interface IExchangeRepository
    {
        Task<Exchange?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(Exchange item, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Exchange item, CancellationToken cancellationToken = default);
        Task<IList<Exchange>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IList<Exchange>> GetFilteredAsync(
            Expression<Func<Exchange, bool>> filter,
            CancellationToken cancellationToken = default);
        IQueryable<Exchange> Query();
    }
}
