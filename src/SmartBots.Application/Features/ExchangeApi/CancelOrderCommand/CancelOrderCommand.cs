using MediatR;

namespace SmartBots.Application.Features.ExchangeApi.CancelOrderCommand
{
    public record CancelOrderCommand(Guid ExchangeAccountId, string Symbol, long OrderId) : IRequest;

}
