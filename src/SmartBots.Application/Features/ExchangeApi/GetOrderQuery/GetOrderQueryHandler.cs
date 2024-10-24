using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetOrderQuery
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
    {
        private readonly IExchangeAccountRepository _exchangeAccountRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetOrderQueryHandler(IExchangeAccountRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeAccountRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeAccountRepository.GetByIdAsync(request.ExchangeAccountId);
            if (exchangeAccount == null) return null;

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchangeAccount);
            return await exchangeClient.GetOrderAsync(request.Symbol, request.OrderId);
        }
    }

}
