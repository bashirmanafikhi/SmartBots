using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi
{
    public class GetExchangeAccountInfoQueryHandler : IRequestHandler<GetExchangeAccountInfoQuery, ExchangeAccountInfo>
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetExchangeAccountInfoQueryHandler(
            IExchangeRepository exchangeRepository,
            IExchangeFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<ExchangeAccountInfo> Handle(GetExchangeAccountInfoQuery request, CancellationToken cancellationToken)
        {
            var exchange = await _exchangeRepository.GetByIdAsync(request.ExchangeId);
            if (exchange == null)
            {
                return new ExchangeAccountInfo();
            }

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchange);

            var accountInfo = await exchangeClient.GetAccountInfoAsync();

            return accountInfo;
        }
    }
}
