using SmartBots.Domain.Entities;
using SmartBots.Domain.Enums;

namespace SmartBots.Application.Interfaces
{
    public interface IExchangeFactory
    {
        IExchangeClient CreateExchangeClient(ExchangeAccount exchange);
        IExchangeClient CreateExchangeClient(ExchangeType exchangeType);
        IMarketDataClient CreateMarketDataClient(ExchangeAccount exchange);
        IMarketDataClient CreateMarketDataClient(ExchangeType exchangeType);
        IExchangeWebSocketClient CreateWebSocketClient(ExchangeAccount exchange);
        IExchangeWebSocketClient CreateWebSocketClient(ExchangeType exchangeType);
    }
}
