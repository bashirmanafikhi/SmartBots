using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.Exchange
{
    public class GetExchangeAccountDetailsQueryHandler : IRequestHandler<GetExchangeAccountDetailsQuery, ExchangeAccountDto>
    {
        private readonly IExchangeAccountRepository _exchangeRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetExchangeAccountDetailsQueryHandler(IExchangeAccountRepository exchangeRepository, ICurrentUserService currentUserService)
        {
            _exchangeRepository = exchangeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ExchangeAccountDto> Handle(GetExchangeAccountDetailsQuery request, CancellationToken cancellationToken)
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
