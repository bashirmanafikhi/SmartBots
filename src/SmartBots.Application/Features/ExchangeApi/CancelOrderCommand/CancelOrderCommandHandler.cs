using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.CancelOrderCommand
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IExchangeAccountRepository _exchangeRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public CancelOrderCommandHandler(IExchangeAccountRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }
        public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeRepository.GetByIdAsync(request.ExchangeAccountId);
            if (exchangeAccount == null) return;

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchangeAccount);
            await exchangeClient.CancelOrderAsync(request.Symbol, request.OrderId);
        }
    }

}
