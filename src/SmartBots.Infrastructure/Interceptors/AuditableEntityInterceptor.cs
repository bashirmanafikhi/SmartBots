using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Infrastructure.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly IServiceProvider _serviceProvider;

        public AuditableEntityInterceptor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                UpdateAuditableEntities(eventData.Context);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateAuditableEntities(DbContext context)
        {
            var entities = context.ChangeTracker.Entries<IAuditableEntity>().ToList();

            foreach (EntityEntry<IAuditableEntity> entry in entities)
            {
                if (entry.Entity is IAuditableEntity auditable)
                {
                    var currentUserService = _serviceProvider.GetRequiredService<ICurrentUserService>();

                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedDate = DateTime.UtcNow;
                        auditable.CreatedBy = currentUserService.GetUserId();
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        auditable.UpdatedDate = DateTime.UtcNow;
                        auditable.UpdatedBy = currentUserService.GetUserId();
                    }
                }
            }
        }
    }
}
