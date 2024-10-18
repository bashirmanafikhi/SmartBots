using MediatR;

namespace SmartBots.Application.Features.ExchangeApi.CancelOrderCommand
{
    public record CancelOrderCommand(Guid ExchangeId, string Symbol, long OrderId) : IRequest;

}
