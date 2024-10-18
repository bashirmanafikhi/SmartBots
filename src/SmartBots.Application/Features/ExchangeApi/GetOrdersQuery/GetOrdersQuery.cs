using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetOrdersQuery
{
    public record GetOrdersQuery(Guid ExchangeId, string Symbol = null, DateTime? StartTime = null, DateTime? EndTime = null) : IRequest<IEnumerable<Order>>;

}
