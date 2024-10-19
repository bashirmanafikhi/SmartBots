using SmartBots.Domain.Entities;
using SmartBots.Domain.Enums;

namespace SmartBots.Application.Interfaces
{
    public interface IExchangeFactory
    {
        IExchangeClient CreateExchangeClient(Exchange exchange);
        IMarketDataClient CreateMarketDataClient(Exchange exchange);
        IExchangeWebSocketClient CreateWebSocketClient(ExchangeType exchangeType);
    }
}
