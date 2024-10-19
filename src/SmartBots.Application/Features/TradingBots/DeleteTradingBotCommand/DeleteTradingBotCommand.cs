using MediatR;

namespace SmartBots.Application.Features.TradingBots;
public sealed record DeleteTradingBotCommand(Guid Id) : IRequest<bool>
{
}
