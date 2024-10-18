using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetTickerPriceQuery
{
    public record GetTickerPriceQuery(Guid ExchangeId, string Symbol) : IRequest<TickerPrice>;
}
