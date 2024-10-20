using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi
{
    public class GetExchangeAccountInfoQueryHandler : IRequestHandler<GetExchangeAccountInfoQuery, ExchangeAccountInfo>
    {
        private readonly IExchangeAccountRepository _exchangeRepository;
        private readonly IExchangeAccountFactory _exchangeFactory;

        public GetExchangeAccountInfoQueryHandler(
            IExchangeAccountRepository exchangeRepository,
            IExchangeAccountFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<ExchangeAccountInfo> Handle(GetExchangeAccountInfoQuery request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeRepository.GetByIdAsync(request.ExchangeAccountId);
            if (exchangeAccount == null)
            {
                return new ExchangeAccountInfo();
            }

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchangeAccount);

            var accountInfo = await exchangeClient.GetAccountInfoAsync();

            return accountInfo;
        }
    }
}
