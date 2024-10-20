using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetAllTickerPricesQuery
{
    public record GetAllTickerPricesQuery(Guid ExchangeAccountId) : IRequest<IEnumerable<TickerPrice>>;
}
