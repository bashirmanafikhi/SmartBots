using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetKlinesQuery
{
    public class GetKlinesQueryHandler : IRequestHandler<GetKlinesQuery, IEnumerable<Kline>>
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetKlinesQueryHandler(IExchangeRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<IEnumerable<Kline>> Handle(GetKlinesQuery request, CancellationToken cancellationToken)
        {
            var exchange = await _exchangeRepository.GetByIdAsync(request.ExchangeId);
            if (exchange == null) return Enumerable.Empty<Kline>();

            var marketDataClient = _exchangeFactory.CreateMarketDataClient(exchange);
            return await marketDataClient.GetKlinesAsync(request.Symbol, request.Interval, request.StartTime, request.EndTime);
        }
    }

}
