using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetOrdersQuery
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<Order>>
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetOrdersQueryHandler(IExchangeRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<IEnumerable<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var exchange = await _exchangeRepository.GetByIdAsync(request.ExchangeId);
            if (exchange == null) return Enumerable.Empty<Order>();

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchange);
            return await exchangeClient.GetOrdersAsync(request.Symbol, request.StartTime, request.EndTime);
        }
    }

}
