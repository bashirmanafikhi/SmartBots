using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetAvailableSymbolsQuery
{
    public class GetAvailableSymbolsQueryHandler : IRequestHandler<GetAvailableSymbolsQuery, IEnumerable<Symbol>>
    {
        private readonly IExchangeAccountRepository _exchangeRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetAvailableSymbolsQueryHandler(IExchangeAccountRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<IEnumerable<Symbol>> Handle(GetAvailableSymbolsQuery request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeRepository.GetByIdAsync(request.ExchangeAccountId);
            if (exchangeAccount == null) return Enumerable.Empty<Symbol>();

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchangeAccount);
            return await exchangeClient.GetAvailableSymbolsAsync(request.IsSpotTradingAllowed);
        }
    }

}
