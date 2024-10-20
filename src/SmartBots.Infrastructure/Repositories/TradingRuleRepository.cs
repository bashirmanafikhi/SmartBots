using SmartBots.Application.Interfaces;
using SmartBots.Domain.Entities;

namespace SmartBots.Infrastructure.Repositories;
public class TradingRuleRepository : ITradingRuleRepository
{
    private readonly IGenericRepository<TradingRule> _repository;

    public TradingRuleRepository(IGenericRepository<TradingRule> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<TradingRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(TradingRule item, CancellationToken cancellationToken = default)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        await _repository.DeleteAsync(item, cancellationToken);
        return true;
    }
}
