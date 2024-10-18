using MediatR;
using SmartBots.Application.Common;

namespace SmartBots.Application.Features.TradingBots;
public sealed class SearchTradingBotsQuery : IRequest<PaginatedList<TradingBotDto>>
{
    public Guid ExchangeId { get; set; }
    public Paging? Paging { get; set; }
    public TradingBotsSearchCriteria Criteria { get; set; } = new TradingBotsSearchCriteria();
}
