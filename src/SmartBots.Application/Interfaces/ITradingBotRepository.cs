using SmartBots.Application.Common;
using SmartBots.Application.Features.TradingBots;
using SmartBots.Domain.Entities;
using System.Linq.Expressions;

namespace SmartBots.Application.Interfaces;
public interface ITradingBotRepository
{
    Task<bool> AddAsync(
        TradingBot item,
        CancellationToken cancellationToken = default);

    Task<PaginatedList<TradingBotDto>> GetCurrentUserItemsWithPaginationAsync(
           Expression<Func<TradingBot, bool>> predicate,
           Paging? paging,
           Expression<Func<TradingBot, object>> orderBy,
           CancellationToken cancellationToken = default);

    Task<List<TradingBotDto>> GetCurrentUserItemsAsync(
        Expression<Func<TradingBot, bool>> predicate, 
        CancellationToken cancellationToken = default);
}
