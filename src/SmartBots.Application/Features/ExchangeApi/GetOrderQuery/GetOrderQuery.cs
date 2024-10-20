using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetOrderQuery
{
    public record GetOrderQuery(Guid ExchangeAccountId, string Symbol, long OrderId) : IRequest<Order>;

}
