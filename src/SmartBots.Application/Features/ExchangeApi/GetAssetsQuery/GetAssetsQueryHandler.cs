using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetAssetsQuery
{
    public class GetAssetsQueryHandler : IRequestHandler<GetAssetsQuery, IEnumerable<Asset>>
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetAssetsQueryHandler(IExchangeRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<IEnumerable<Asset>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
        {
            var exchange = await _exchangeRepository.GetByIdAsync(request.ExchangeId);
            if (exchange == null) return Enumerable.Empty<Asset>();

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchange);
            return await exchangeClient.GetAssetsAsync();
        }
    }

}
