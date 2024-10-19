using SmartBots.Application.Common;
using SmartBots.Application.Features.TradingBots;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Entities;
using System.Linq.Expressions;

namespace SmartBots.Infrastructure.Repositories;
public class TradingBotRepository : ITradingBotRepository
{
    private readonly IUserOwnedEntityRepository<TradingBot> _repository;

    public TradingBotRepository(IUserOwnedEntityRepository<TradingBot> repository)
    {
        _repository = repository;
    }

    public async Task<TradingBot?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<bool> AddAsync(
        TradingBot item, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item);

        await _repository.AddAsync(item, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(
        TradingBot item, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item);

        await _repository.DeleteAsync(item, cancellationToken);
        return true;
    }

    public async Task<PaginatedList<TradingBotDto>> GetCurrentUserItemsWithPaginationAsync(
           Expression<Func<TradingBot, bool>> predicate,
           Paging? paging,
           Expression<Func<TradingBot, object>> orderBy,
           CancellationToken cancellationToken = default)
    {
        var (result, total) = await _repository.GetCurrentUserItemsAsync<TradingBotDto>(
            predicate,
            paging,
            orderBy,
            cancellationToken);

        return new PaginatedList<TradingBotDto>(result, total);
    }

    public async Task<List<TradingBotDto>> GetCurrentUserItemsAsync(
        Expression<Func<TradingBot, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        (var result, _) = await _repository.GetCurrentUserItemsAsync<TradingBotDto>(
            predicate: predicate,
            cancellationToken: cancellationToken);

        return result;
    }
}
