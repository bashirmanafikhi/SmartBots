using SmartBots.Application.Features.Exchange;
using SmartBots.Domain.Entities;
using System.Linq.Expressions;

namespace SmartBots.Application.Interfaces
{
    public interface IExchangeAccountRepository
    {
        Task<ExchangeAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(ExchangeAccount item, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(ExchangeAccount item, CancellationToken cancellationToken = default);
        Task<IList<ExchangeAccount>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IList<ExchangeAccount>> GetFilteredAsync(
            Expression<Func<ExchangeAccount, bool>> filter,
            CancellationToken cancellationToken = default);
        IQueryable<ExchangeAccount> Query();

        Task<List<ExchangeAccountDto>> GetCurrentUserItemsAsync(CancellationToken cancellationToken = default);
    }
}
