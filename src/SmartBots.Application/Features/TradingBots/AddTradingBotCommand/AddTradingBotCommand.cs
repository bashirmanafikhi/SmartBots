using MediatR;

namespace SmartBots.Application.Features.TradingBots;
public sealed class AddTradingBotCommand : IRequest<Guid>
{
    public TradingBotDto Model { get; set; }
}
