﻿using MediatR;
using SmartBots.Application.Common;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.TradingBots.alaa;
public sealed class SearchTradingBotsQueryHandler : IRequestHandler<SearchTradingBotsQuery, PaginatedList<TradingBotDto>>
{
    private readonly ITradingBotRepository _tradingBotRepository;

    public SearchTradingBotsQueryHandler(ITradingBotRepository repository)
    {
        _tradingBotRepository = repository;
    }

    public async Task<PaginatedList<TradingBotDto>> Handle(SearchTradingBotsQuery query, CancellationToken cancellationToken)
    {
        var predicate = query.Criteria.GetPredicateAsExpression();
        var paging = query.Paging;

        return await _tradingBotRepository.GetCurrentUserItemsWithPaginationAsync(
                predicate,
                paging,
                null,
                cancellationToken);
    }
}
