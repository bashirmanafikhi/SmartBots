﻿using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetAvailableSymbolsQuery
{
    public record GetAvailableSymbolsQuery(Guid ExchangeId, bool IsSpotTradingAllowed) : IRequest<IEnumerable<Symbol>>;
}