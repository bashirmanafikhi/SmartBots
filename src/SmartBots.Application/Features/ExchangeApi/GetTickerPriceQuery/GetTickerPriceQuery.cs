using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetTickerPriceQuery
{
    public record GetTickerPriceQuery(Guid ExchangeAccountId, string Symbol) : IRequest<TickerPrice>;
}
