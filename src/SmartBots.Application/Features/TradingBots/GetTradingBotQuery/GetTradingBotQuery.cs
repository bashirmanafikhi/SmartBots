using MediatR;

namespace SmartBots.Application.Features.TradingBots;
public record GetTradingBotQuery(Guid Id) : IRequest<TradingBotDto> { }
