using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetOrderQuery
{
    public record GetOrderQuery(Guid ExchangeId, string Symbol, long OrderId) : IRequest<Order>;

}
