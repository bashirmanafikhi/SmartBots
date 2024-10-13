using SmartBots.Application.Common.Mappings;
using SmartBots.Domain.Common;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Interfaces;
public interface IUserOwnedEntityRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity, IUserOwnedEntity
{
    Task<List<TDestination>> GetCurrentUserItemsAsync<TDestination>(CancellationToken cancellationToken = default) where TDestination : IMapFrom<T>;
}

