using MediatR;
using SmartBots.Domain.Entities;

namespace SmartBots.Application.Features.Exchange
{
    public record AddExchangeCommand : IRequest<ExchangeDto>
    {
        public string Name { get; set; }
        public ExchangeType Type { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public bool IsTest { get; set; }
    }
}
