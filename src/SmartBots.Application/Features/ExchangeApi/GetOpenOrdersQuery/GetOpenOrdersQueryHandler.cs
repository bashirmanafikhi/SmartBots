using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetOpenOrdersQuery
{
    public class GetOpenOrdersQueryHandler : IRequestHandler<GetOpenOrdersQuery, IEnumerable<Order>>
    {
        private readonly IExchangeAccountRepository _exchangeAccountRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetOpenOrdersQueryHandler(IExchangeAccountRepository exchangeRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeAccountRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<IEnumerable<Order>> Handle(GetOpenOrdersQuery request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeAccountRepository.GetByIdAsync(request.ExchangeAccountId);
            if (exchangeAccount == null) return Enumerable.Empty<Order>();

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchangeAccount);
            return await exchangeClient.GetOpenOrdersAsync(request.Symbol);
        }
    }

}
