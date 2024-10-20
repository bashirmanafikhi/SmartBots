using SmartBots.Application.Common.Mappings;
using SmartBots.Domain.Enums;

namespace SmartBots.Application.Features.Exchange
{
    public class ExchangeAccountDto : IMapFrom<Domain.Entities.ExchangeAccount>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ExchangeType Type { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public bool IsTest { get; set; }

        // Implicit conversion from Domain Entity to DTO
        public static implicit operator ExchangeAccountDto(Domain.Entities.ExchangeAccount exchange) =>
            new ExchangeAccountDto
            {
                Id = exchange.Id,
                Name = exchange.Name,
                Type = exchange.Type,
                ApiKey = exchange.ApiKey,
                ApiSecret = exchange.ApiSecret,
                IsTest = exchange.IsTest
            };
    }
}
