using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartBots.Application.Interfaces;
using SmartBots.Infrastructure.Common;
using SmartBots.Infrastructure.Data;
using SmartBots.Infrastructure.Interceptors;
using SmartBots.Infrastructure.Repositories;

namespace SmartBots.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepositories();

            return services;
        }
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<AuditableEntityInterceptor>();

            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());

            });
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient(typeof(IUserOwnedEntityRepository<>), typeof(UserOwnedEntityRepository<>))
                .AddTransient<ITodoRepository, TodoRepository>()
                .AddTransient<IExchangeRepository, ExchangeRepository>()
                .AddTransient<ICurrentUserService, CurrentUserService>();
        }
    }
}
