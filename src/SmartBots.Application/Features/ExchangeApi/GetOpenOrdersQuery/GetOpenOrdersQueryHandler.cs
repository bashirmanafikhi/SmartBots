using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetOpenOrdersQuery
{
    public class GetOpenOrdersQueryHandler : IRequestHandler<GetOpenOrdersQuery, IEnumerable<Order>>
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetOpenOrdersQueryHandler(IExchangeRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<IEnumerable<Order>> Handle(GetOpenOrdersQuery request, CancellationToken cancellationToken)
        {
            var exchange = await _exchangeRepository.GetByIdAsync(request.ExchangeId);
            if (exchange == null) return Enumerable.Empty<Order>();

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchange);
            return await exchangeClient.GetOpenOrdersAsync(request.Symbol);
        }
    }

}
