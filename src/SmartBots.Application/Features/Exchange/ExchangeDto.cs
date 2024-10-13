using SmartBots.Application.Common.Mappings;
using SmartBots.Domain.Entities;

namespace SmartBots.Application.Features.Exchange
{
    public class ExchangeDto : IMapFrom<Domain.Entities.Exchange>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ExchangeType Type { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public bool IsTest { get; set; }
    }
}
