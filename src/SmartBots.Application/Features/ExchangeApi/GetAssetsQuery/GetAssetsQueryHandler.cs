using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetAssetsQuery
{
    public class GetAssetsQueryHandler : IRequestHandler<GetAssetsQuery, IEnumerable<Asset>>
    {
        private readonly IExchangeAccountRepository _exchangeAccountRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetAssetsQueryHandler(IExchangeAccountRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeAccountRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<IEnumerable<Asset>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeAccountRepository.GetByIdAsync(request.ExchangeAccountId);
            if (exchangeAccount == null) return Enumerable.Empty<Asset>();

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchangeAccount);
            return await exchangeClient.GetAssetsAsync();
        }
    }

}
