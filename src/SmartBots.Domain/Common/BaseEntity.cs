using SmartBots.Domain.Interfaces;

namespace SmartBots.Domain.Common
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}
