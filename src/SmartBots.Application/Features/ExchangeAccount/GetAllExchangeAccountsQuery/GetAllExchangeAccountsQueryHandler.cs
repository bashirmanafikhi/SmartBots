using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.Exchange
{
    public class GetAllExchangeAccountsQueryHandler : IRequestHandler<GetAllExchangeAccountsQuery, List<ExchangeAccountDto>>
    {
        private readonly IExchangeAccountRepository _exchangeRepository;

        public GetAllExchangeAccountsQueryHandler(IExchangeAccountRepository exchangeRepository)
        {
            _exchangeRepository = exchangeRepository;
        }

        public async Task<List<ExchangeAccountDto>> Handle(GetAllExchangeAccountsQuery query, CancellationToken cancellationToken)
        {
            return await _exchangeRepository.GetCurrentUserItemsAsync(cancellationToken);
        }
    }
}
