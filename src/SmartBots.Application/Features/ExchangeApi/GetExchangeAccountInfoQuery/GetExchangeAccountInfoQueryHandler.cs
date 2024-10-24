﻿using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi
{
    public class GetExchangeAccountInfoQueryHandler : IRequestHandler<GetExchangeAccountInfoQuery, ExchangeAccountInfo>
    {
        private readonly IExchangeAccountRepository _exchangeAccountRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public GetExchangeAccountInfoQueryHandler(
            IExchangeAccountRepository exchangeRepository,
            IExchangeFactory exchangeFactory)
        {
            _exchangeAccountRepository = exchangeRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<ExchangeAccountInfo> Handle(GetExchangeAccountInfoQuery request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeAccountRepository.GetByIdAsync(request.ExchangeAccountId);
            if (exchangeAccount == null)
            {
                return new ExchangeAccountInfo();
            }

            var exchangeClient = _exchangeFactory.CreateExchangeClient(exchangeAccount);

            var accountInfo = await exchangeClient.GetAccountInfoAsync();

            return accountInfo;
        }
    }
}
