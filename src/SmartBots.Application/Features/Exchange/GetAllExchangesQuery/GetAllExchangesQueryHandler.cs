using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.Exchange
{
    public class GetAllExchangesQueryHandler : IRequestHandler<GetAllExchangesQuery, List<ExchangeDto>>
    {
        private readonly IExchangeRepository _exchangeRepository;

        public GetAllExchangesQueryHandler(IExchangeRepository exchangeRepository)
        {
            _exchangeRepository = exchangeRepository;
        }

        public async Task<List<ExchangeDto>> Handle(GetAllExchangesQuery query, CancellationToken cancellationToken)
        {
            return await _exchangeRepository.GetCurrentUserItemsAsync(cancellationToken);
        }
    }
}
