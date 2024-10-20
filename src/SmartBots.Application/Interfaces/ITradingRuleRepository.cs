using SmartBots.Domain.Entities;

namespace SmartBots.Application.Interfaces;
public interface ITradingRuleRepository
{
    Task<TradingRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(TradingRule item, CancellationToken cancellationToken = default);
}
