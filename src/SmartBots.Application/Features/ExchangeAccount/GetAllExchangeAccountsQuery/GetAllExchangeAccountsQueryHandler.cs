using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.Exchange
{
    public class GetAllExchangeAccountsQueryHandler : IRequestHandler<GetAllExchangeAccountsQuery, List<ExchangeAccountDto>>
    {
        private readonly IExchangeAccountRepository _exchangeAccountRepository;

        public GetAllExchangeAccountsQueryHandler(IExchangeAccountRepository exchangeRepository)
        {
            _exchangeAccountRepository = exchangeRepository;
        }

        public async Task<List<ExchangeAccountDto>> Handle(GetAllExchangeAccountsQuery query, CancellationToken cancellationToken)
        {
            return await _exchangeAccountRepository.GetCurrentUserItemsAsync(cancellationToken);
        }
    }
}
