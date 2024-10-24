using MediatR;
using SmartBots.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBots.Application.Features.ExchangeApi.SubscribeToUserDataUpdatesRequest
{
    public class SubscribeToUserDataUpdatesHandler : IRequestHandler<SubscribeToUserDataUpdatesRequest, bool>
    {
        private readonly IExchangeAccountRepository _exchangeAccountRepository;
        private readonly IExchangeFactory _exchangeFactory;

        public SubscribeToUserDataUpdatesHandler(IExchangeAccountRepository exchangeAccountRepository, IExchangeFactory exchangeFactory)
        {
            _exchangeAccountRepository = exchangeAccountRepository;
            _exchangeFactory = exchangeFactory;
        }

        public async Task<bool> Handle(SubscribeToUserDataUpdatesRequest request, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeAccountRepository.GetByIdAsync(request.ExchangeAccountId);
            if (exchangeAccount == null) return false;

            var webSocketClient = _exchangeFactory.CreateWebSocketClient(exchangeAccount);

            return await webSocketClient.SubscribeToUserDataUpdatesAsync(
                request.OnOrderUpdate,
                request.OnOcoOrderUpdate,
                request.OnAccountPositionUpdate,
                request.OnBalanceUpdate,
                request.OnListenKeyExpired,
                cancellationToken);
        }
    }
}
