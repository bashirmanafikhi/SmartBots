using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetAllTickerPricesQuery
{
    public class GetAllTickerPricesQueryHandler : IRequestHandler<GetAllTickerPricesQuery, IEnumerable<TickerPrice>>
    {
        private readonly IExchangeAccountRepository _exchangeAccountRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetAllTickerPricesQueryHandler(IExchangeAccountRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeAccountRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<IEnumerable<TickerPrice>> Handle(GetAllTickerPricesQuery request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeAccountRepository.GetByIdAsync(request.ExchangeAccountId);
            if (exchangeAccount == null) return Enumerable.Empty<TickerPrice>();

            var marketDataClient = _exchangeFactory.CreateMarketDataClient(exchangeAccount);
            return await marketDataClient.GetAllTickerPricesAsync();
        }
    }

}
