using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartBots.Application.Interfaces;
using SmartBots.Infrastructure.Data;
using SmartBots.Infrastructure.Interceptors;
using SmartBots.Infrastructure.Repositories;
using SmartBots.Infrastructure.Services;

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
                .AddTransient<ITradingRuleRepository, TradingRuleRepository>()
                .AddTransient<ITodoRepository, TodoRepository>()
                .AddTransient<IExchangeAccountRepository, ExchangeAccountRepository>()
                .AddTransient<ITradingBotRepository, TradingBotRepository>()
                .AddTransient<ICurrentUserService, CurrentUserService>()
                .AddTransient<IExchangeFactory, ExchangeFactory>()
                .AddTransient<ITradingBotManager, TradingBotManager>()
                .AddTransient<IRealTimeDataManager, RealTimeDataManager>()
                .AddTransient<ITechnicalAnalysisService, TechnicalAnalysisService>()
                .AddTransient<ITradingRuleManager, TradingRuleManager>();

        }
    }
}
