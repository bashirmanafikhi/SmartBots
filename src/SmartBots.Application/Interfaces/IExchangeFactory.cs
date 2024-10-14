using SmartBots.Domain.Entities;

namespace SmartBots.Application.Interfaces
{
    public interface IExchangeFactory
    {
        IExchangeClient CreateExchangeClient(Exchange exchange);
    }
}
