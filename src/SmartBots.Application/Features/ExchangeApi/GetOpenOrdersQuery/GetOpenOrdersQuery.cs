using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetOpenOrdersQuery
{
    public record GetOpenOrdersQuery(Guid ExchangeAccountId, string Symbol = null) : IRequest<IEnumerable<Order>>;

}
