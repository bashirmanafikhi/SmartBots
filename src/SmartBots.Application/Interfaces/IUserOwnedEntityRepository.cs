using SmartBots.Application.Common;
using SmartBots.Application.Common.Mappings;
using SmartBots.Domain.Common;
using SmartBots.Domain.Interfaces;
using System.Linq.Expressions;

namespace SmartBots.Application.Interfaces;
public interface IUserOwnedEntityRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity, IUserOwnedEntity
{
    Task<(List<TDestination> Result, int Total)> GetCurrentUserItemsAsync<TDestination>(
        Expression<Func<T, bool>> predicate,
        Paging? paging,
        Expression<Func<T, object>>? orderBy,
        CancellationToken cancellationToken = default) where TDestination : IMapFrom<T>;
}

