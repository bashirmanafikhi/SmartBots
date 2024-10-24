using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetOrderBookQuery
{
    public class GetOrderBookQueryHandler : IRequestHandler<GetOrderBookQuery, OrderBook>
    {
        private readonly IExchangeAccountRepository _exchangeAccountRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetOrderBookQueryHandler(IExchangeAccountRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeAccountRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<OrderBook> Handle(GetOrderBookQuery request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeAccountRepository.GetByIdAsync(request.ExchangeAccountId);
            if (exchangeAccount == null) return null;

            var marketDataClient = _exchangeFactory.CreateMarketDataClient(exchangeAccount);
            return await marketDataClient.GetOrderBookAsync(request.Symbol, request.Limit);
        }
    }

}
