using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetKlinesQuery
{
    public class GetKlinesQueryHandler : IRequestHandler<GetKlinesQuery, IEnumerable<Kline>>
    {
        private readonly IExchangeAccountRepository _exchangeAccountRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetKlinesQueryHandler(IExchangeAccountRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeAccountRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<IEnumerable<Kline>> Handle(GetKlinesQuery request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeAccountRepository.GetByIdAsync(request.ExchangeAccountId);
            if (exchangeAccount == null) return Enumerable.Empty<Kline>();

            var marketDataClient = _exchangeFactory.CreateMarketDataClient(exchangeAccount);
            return await marketDataClient.GetKlinesAsync(request.Symbol, request.Interval, request.StartTime, request.EndTime);
        }
    }

}
