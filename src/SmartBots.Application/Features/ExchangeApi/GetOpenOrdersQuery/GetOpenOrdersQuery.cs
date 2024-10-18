using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetOpenOrdersQuery
{
    public record GetOpenOrdersQuery(Guid ExchangeId, string Symbol = null) : IRequest<IEnumerable<Order>>;

}
