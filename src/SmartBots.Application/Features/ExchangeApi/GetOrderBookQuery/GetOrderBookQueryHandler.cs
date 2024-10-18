using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetOrderBookQuery
{
    public class GetOrderBookQueryHandler : IRequestHandler<GetOrderBookQuery, OrderBook>
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetOrderBookQueryHandler(IExchangeRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<OrderBook> Handle(GetOrderBookQuery request, CancellationToken cancellationToken)
        {
            var exchange = await _exchangeRepository.GetByIdAsync(request.ExchangeId);
            if (exchange == null) return null;

            var marketDataClient = _exchangeFactory.CreateMarketDataClient(exchange);
            return await marketDataClient.GetOrderBookAsync(request.Symbol, request.Limit);
        }
    }

}
