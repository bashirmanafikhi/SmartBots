using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetTickerPriceQuery
{
    public class GetTickerPriceQueryHandler : IRequestHandler<GetTickerPriceQuery, TickerPrice>
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetTickerPriceQueryHandler(IExchangeRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<TickerPrice> Handle(GetTickerPriceQuery request, CancellationToken cancellationToken)
        {
            var exchange = await _exchangeRepository.GetByIdAsync(request.ExchangeId);
            if (exchange == null) return null;

            var marketDataClient = _exchangeFactory.CreateMarketDataClient(exchange);
            return await marketDataClient.GetTickerPriceAsync(request.Symbol);
        }
    }

}
