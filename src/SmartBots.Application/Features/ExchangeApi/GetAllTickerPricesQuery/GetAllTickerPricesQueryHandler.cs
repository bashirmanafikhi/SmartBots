using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetAllTickerPricesQuery
{
    public class GetAllTickerPricesQueryHandler : IRequestHandler<GetAllTickerPricesQuery, IEnumerable<TickerPrice>>
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetAllTickerPricesQueryHandler(IExchangeRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<IEnumerable<TickerPrice>> Handle(GetAllTickerPricesQuery request, CancellationToken cancellationToken)
        {
            var exchange = await _exchangeRepository.GetByIdAsync(request.ExchangeId);
            if (exchange == null) return Enumerable.Empty<TickerPrice>();

            var marketDataClient = _exchangeFactory.CreateMarketDataClient(exchange);
            return await marketDataClient.GetAllTickerPricesAsync();
        }
    }

}
