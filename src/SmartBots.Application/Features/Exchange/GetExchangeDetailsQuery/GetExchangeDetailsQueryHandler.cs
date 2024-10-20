using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.Exchange
{
    public class GetExchangeDetailsQueryHandler : IRequestHandler<GetExchangeDetailsQuery, ExchangeAccountDto>
    {
        private readonly IExchangeAccountRepository _exchangeRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetExchangeDetailsQueryHandler(IExchangeAccountRepository exchangeRepository, ICurrentUserService currentUserService)
        {
            _exchangeRepository = exchangeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ExchangeAccountDto> Handle(GetExchangeDetailsQuery request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeRepository.GetByIdAsync(request.Id);
            if (exchangeAccount == null)
            {
                return new ExchangeAccountDto(); // Exchange not found
            }

            var currentuserid = _currentUserService.GetUserId();
            exchangeAccount.Authorize(currentuserid);

            return exchangeAccount;
        }
    }
}
