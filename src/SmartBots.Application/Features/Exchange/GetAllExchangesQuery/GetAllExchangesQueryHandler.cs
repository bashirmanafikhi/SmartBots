using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.Exchange
{
    public class GetAllExchangesQueryHandler : IRequestHandler<GetAllExchangesQuery, List<ExchangeAccountDto>>
    {
        private readonly IExchangeAccountRepository _exchangeRepository;

        public GetAllExchangesQueryHandler(IExchangeAccountRepository exchangeRepository)
        {
            _exchangeRepository = exchangeRepository;
        }

        public async Task<List<ExchangeAccountDto>> Handle(GetAllExchangesQuery query, CancellationToken cancellationToken)
        {
            return await _exchangeRepository.GetCurrentUserItemsAsync(cancellationToken);
        }
    }
}
