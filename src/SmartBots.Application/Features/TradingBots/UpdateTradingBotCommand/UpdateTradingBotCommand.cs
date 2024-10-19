using MediatR;

namespace SmartBots.Application.Features.TradingBots;
public class UpdateTradingBotCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public TradingBotDto Model { get; set; }
}
