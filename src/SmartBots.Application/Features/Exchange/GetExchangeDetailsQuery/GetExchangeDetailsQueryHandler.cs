using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.Exchange
{
    public class GetExchangeDetailsQueryHandler : IRequestHandler<GetExchangeDetailsQuery, ExchangeDto>
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetExchangeDetailsQueryHandler(IExchangeRepository exchangeRepository, ICurrentUserService currentUserService)
        {
            _exchangeRepository = exchangeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ExchangeDto> Handle(GetExchangeDetailsQuery request, CancellationToken cancellationToken)
        {
            var exchange = await _exchangeRepository.GetByIdAsync(request.Id);
            if (exchange == null)
            {
                return new ExchangeDto(); // Exchange not found
            }

            var currentuserid = _currentUserService.GetUserId();
            exchange.Authorize(currentuserid);

            return exchange;
        }
    }
}
