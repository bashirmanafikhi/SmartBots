using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetOrderQuery
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetOrderQueryHandler(IExchangeRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var exchange = await _exchangeRepository.GetByIdAsync(request.ExchangeId);
            if (exchange == null) return null;

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchange);
            return await exchangeClient.GetOrderAsync(request.Symbol, request.OrderId);
        }
    }

}
