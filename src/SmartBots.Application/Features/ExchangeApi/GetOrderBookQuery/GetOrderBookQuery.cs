using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetOrderBookQuery
{
    public record GetOrderBookQuery(Guid ExchangeId, string Symbol, int Limit = 100) : IRequest<OrderBook>;
}
