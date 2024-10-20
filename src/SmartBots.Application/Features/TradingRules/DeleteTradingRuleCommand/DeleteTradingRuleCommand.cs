using MediatR;

namespace SmartBots.Application.Features.TradingRules;
public sealed record DeleteTradingRuleCommand(Guid Id) : IRequest<bool> { }
