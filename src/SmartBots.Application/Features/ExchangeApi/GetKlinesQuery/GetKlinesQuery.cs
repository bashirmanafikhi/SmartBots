﻿using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetKlinesQuery
{
    public record GetKlinesQuery(
        Guid ExchangeId,
        string Symbol,
        KlineInterval Interval,
        DateTime? StartTime = null,
        DateTime? EndTime = null
    ) : IRequest<IEnumerable<Kline>>;
}