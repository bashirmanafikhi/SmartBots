using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetBalancesQuery
{
    public class GetBalancesQueryHandler : IRequestHandler<GetBalancesQuery, IEnumerable<Balance>>
    {
        private readonly IExchangeAccountRepository _exchangeRepository;
        private readonly IExchangeAccountFactory _exchangeFactory;

        public GetBalancesQueryHandler(IExchangeAccountRepository exchangeRepository, IExchangeAccountFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<IEnumerable<Balance>> Handle(GetBalancesQuery request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeRepository.GetByIdAsync(request.ExchangeAccountId);
            if (exchangeAccount == null) return Enumerable.Empty<Balance>();

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchangeAccount);
            return await exchangeClient.GetBalancesAsync();
        }
    }

}
