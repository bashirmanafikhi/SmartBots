using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Enums;

namespace SmartBots.Application.Features.ExchangeApi.SubscribeToKlineUpdatesRequest
{
    public class SubscribeToKlineUpdatesHandler : IRequestHandler<SubscribeToKlineUpdatesRequest, bool>
    {
        private readonly IExchangeFactory _exchangeFactory;

        public SubscribeToKlineUpdatesHandler(IExchangeFactory exchangeFactory)
        {
            _exchangeFactory = exchangeFactory;
        }

        public async Task<bool> Handle(SubscribeToKlineUpdatesRequest request, CancellationToken cancellationToken)
        {
            var webSocketClient = _exchangeFactory.CreateWebSocketClient(request.ExchangeType);

            return await webSocketClient.SubscribeToKlineUpdatesAsync(
                request.Symbol, request.Interval, request.OnUpdate, cancellationToken);
        }
    }
}
