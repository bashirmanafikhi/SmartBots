using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetBalancesQuery
{
    public class GetBalancesQueryHandler : IRequestHandler<GetBalancesQuery, IEnumerable<Balance>>
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetBalancesQueryHandler(IExchangeRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<IEnumerable<Balance>> Handle(GetBalancesQuery request, CancellationToken cancellationToken)
        {
            var exchange = await _exchangeRepository.GetByIdAsync(request.ExchangeId);
            if (exchange == null) return Enumerable.Empty<Balance>();

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchange);
            return await exchangeClient.GetBalancesAsync();
        }
    }

}
