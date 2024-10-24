﻿using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetTickerPriceQuery
{
    public class GetTickerPriceQueryHandler : IRequestHandler<GetTickerPriceQuery, TickerPrice>
    {
        private readonly IExchangeAccountRepository _exchangeAccountRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetTickerPriceQueryHandler(IExchangeAccountRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeAccountRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<TickerPrice> Handle(GetTickerPriceQuery request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeAccountRepository.GetByIdAsync(request.ExchangeAccountId);
            if (exchangeAccount == null) return null;

            var marketDataClient = _exchangeFactory.CreateMarketDataClient(exchangeAccount);
            return await marketDataClient.GetTickerPriceAsync(request.Symbol);
        }
    }

}
