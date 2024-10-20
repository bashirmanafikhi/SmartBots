using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetOrdersQuery
{
    public record GetOrdersQuery(Guid ExchangeAccountId, string Symbol = null, DateTime? StartTime = null, DateTime? EndTime = null) : IRequest<IEnumerable<Order>>;

}
