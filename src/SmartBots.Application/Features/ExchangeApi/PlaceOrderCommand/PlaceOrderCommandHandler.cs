using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.PlaceOrderCommand
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, Order>
    {
        private readonly IExchangeAccountRepository _exchangeRepository;
        private readonly IExchangeAccountFactory _exchangeFactory;

        public PlaceOrderCommandHandler(IExchangeAccountRepository exchangeRepository, IExchangeAccountFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<Order> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeRepository.GetByIdAsync(request.ExchangeAccountId);
            if (exchangeAccount == null) return null;

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchangeAccount);
            return await exchangeClient.PlaceOrderAsync(request.OrderRequest);
        }
    }

}
