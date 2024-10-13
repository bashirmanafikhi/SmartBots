using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartBots.Application.Common.Mappings;
using SmartBots.Application.Features.Todos;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Common;
using SmartBots.Domain.Interfaces;
using SmartBots.Infrastructure.Data;

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

    public async Task<List<TDestination>> GetCurrentUserItemsAsync<TDestination>(CancellationToken cancellationToken = default) where TDestination : IMapFrom<T>
    {
        var cuurentUserId = _currentUserService.GetUserId().ToString().ToUpper();

        return await GetFilteredAndProjectToAsync<TDestination>(x => 
            string.Equals(x.ApplicationUserId.ToUpper(), cuurentUserId.ToString()),
            cancellationToken);
    }
}
