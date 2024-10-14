using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartBots.Application.Common;
using SmartBots.Application.Common.Extensions;
using SmartBots.Application.Common.Mappings;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Common;
using SmartBots.Domain.Interfaces;
using SmartBots.Infrastructure.Data;
using System.Linq.Expressions;

namespace SmartBots.Infrastructure.Repositories;
public sealed class UserOwnedEntityRepository<T> : GenericRepository<T>, IUserOwnedEntityRepository<T> where T : BaseAuditableEntity, IUserOwnedEntity
{
    private readonly ICurrentUserService _currentUserService;
    public UserOwnedEntityRepository(
        ApplicationDbContext dbContext,
        ILogger<GenericRepository<T>> logger,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(dbContext, logger, mapper)
    {
        _currentUserService = currentUserService;
    }

    public async Task<(List<TDestination> Result, int Total)> GetCurrentUserItemsAsync<TDestination>(
        Expression<Func<T, bool>> predicate = null,
        Paging? paging = null,
        Expression<Func<T, object>>? orderBy = null,
        CancellationToken cancellationToken = default) where TDestination : IMapFrom<T>
    {
        var currentUserId = _currentUserService.GetUserId().ToString()!.ToUpper();

        Expression<Func<T, bool>> userIdPredicate = x => x.ApplicationUserId.ToUpper() == currentUserId;

        predicate = (predicate is null)
                        ? userIdPredicate
                        : predicate.And(userIdPredicate);

        return await GetFilteredAndProjectToAsync<TDestination>(predicate, paging, orderBy, cancellationToken);
    }
}
