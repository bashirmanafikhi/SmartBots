using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Enums;

namespace SmartBots.Application.Features.ExchangeApi.SubscribeToTickerUpdatesRequest
{
    public class SubscribeToTickerUpdatesHandler : IRequestHandler<SubscribeToTickerUpdatesRequest, bool>
    {
        private readonly IExchangeFactory _exchangeFactory;

        public SubscribeToTickerUpdatesHandler(IExchangeFactory exchangeFactory)
        {
            _exchangeFactory = exchangeFactory;
        }

        public async Task<bool> Handle(SubscribeToTickerUpdatesRequest request, CancellationToken cancellationToken)
        {
            var webSocketClient = _exchangeFactory.CreateWebSocketClient(request.ExchangeType);

            return await webSocketClient.SubscribeToTickerUpdatesAsync(
                request.Symbol, request.OnUpdate, cancellationToken);
        }
    }
}
