using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.TradingBots;
public class StartAutoTradingCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public KlineInterval Interval { get; set; }
}
